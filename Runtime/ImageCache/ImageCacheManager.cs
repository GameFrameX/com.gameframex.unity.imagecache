using System.Threading;
using System.Threading.Tasks;
using GameFrameX.Runtime;
using UnityEngine;

#if UNITY_WEBGL
using UnityEngine.Networking;
#else
using System.Collections.Concurrent;
using System.IO;
using GameFrameX.Download.Runtime;
#endif

namespace GameFrameX.ImageCache.Runtime
{
    /// <summary>
    /// 图片缓存管理器。
    /// 非 WebGL 平台：通过 IDownloadManager 下载远程图片到磁盘，以 URL 的 MD5 哈希作为文件名缓存。
    /// WebGL 平台：通过 UnityWebRequestTexture 加载，由浏览器管理缓存。
    /// </summary>
    [UnityEngine.Scripting.Preserve]
    public sealed class ImageCacheManager : GameFrameworkModule, IImageCacheManager
    {
        private readonly ImageCacheConfig _config;

#if !UNITY_WEBGL
        /// <summary>
        /// 下载管理器实例，用于发起文件下载。
        /// </summary>
        private IDownloadManager _downloadManager;

        /// <summary>
        /// 正在进行中的下载任务，Key 为下载序列号，Value 为对应的 TaskCompletionSource。
        /// </summary>
        private readonly ConcurrentDictionary<int, TaskCompletionSource<bool>> _downloadingTasks = new ConcurrentDictionary<int, TaskCompletionSource<bool>>();
#endif

        public ImageCacheManager()
        {
            _config = new ImageCacheConfig();
        }

        /// <summary>
        /// 获取图片缓存配置。
        /// </summary>
        public ImageCacheConfig Config
        {
            get { return _config; }
        }

#if !UNITY_WEBGL
        /// <summary>
        /// 设置下载管理器并注册下载成功/失败事件回调。
        /// </summary>
        /// <param name="downloadManager">下载管理器实例。</param>
        internal void SetDownloadManager(IDownloadManager downloadManager)
        {
            _downloadManager = downloadManager;
            _downloadManager.DownloadSuccess += OnDownloadSuccess;
            _downloadManager.DownloadFailure += OnDownloadFailure;
        }
#endif

        /// <summary>
        /// 异步加载远程图片。
        /// </summary>
        /// <param name="url">远程图片地址（必须以 http:// 或 https:// 开头）。</param>
        /// <param name="cancellationToken">取消令牌（仅非 WebGL 平台生效）。</param>
        /// <returns>加载成功返回 Texture2D，失败返回 null。</returns>
        public async Task<Texture2D> LoadImageAsync(string url, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(url))
            {
                Log.Warning("ImageCache: url is null or empty.");
                return null;
            }

            if (!url.StartsWith("http://") && !url.StartsWith("https://"))
            {
                Log.Warning("ImageCache: url must start with http:// or https://. url: {0}", url);
                return null;
            }

#if UNITY_WEBGL
            return await LoadImageWebGLAsync(url);
#else
            return await LoadImageDiskCacheAsync(url, cancellationToken);
#endif
        }

#if UNITY_WEBGL
        /// <summary>
        /// WebGL 平台：通过 UnityWebRequestTexture 加载图片，由浏览器管理缓存。
        /// </summary>
        /// <param name="url">远程图片地址。</param>
        /// <returns>加载成功返回 Texture2D，失败返回 null。</returns>
        private async Task<Texture2D> LoadImageWebGLAsync(string url)
        {
            var request = UnityWebRequestTexture.GetTexture(url);
            var op = request.SendWebRequest();
            var tcs = new TaskCompletionSource<bool>();
            op.completed += _ => tcs.TrySetResult(true);
            await tcs.Task;

            if (!string.IsNullOrEmpty(request.error))
            {
                Log.Warning("ImageCache: download failed. url: {0}, error: {1}", url, request.error);
                request.Dispose();
                return null;
            }

            var texture = DownloadHandlerTexture.GetContent(request);
            request.Dispose();
            return texture;
        }
#else
        /// <summary>
        /// 非 WebGL 平台：优先从磁盘缓存读取，未命中时下载到本地后加载为 Texture2D。
        /// </summary>
        /// <param name="url">远程图片地址。</param>
        /// <param name="cancellationToken">取消令牌。</param>
        /// <returns>加载成功返回 Texture2D，失败返回 null。</returns>
        private async Task<Texture2D> LoadImageDiskCacheAsync(string url, CancellationToken cancellationToken)
        {
            var hash = Utility.Hash.MD5.Hash(url);
            var cachePath = PathHelper.Combine(_config.CachePath, hash + Utility.Const.FileNameSuffix.PNG);

            if (!FileHelper.IsExists(cachePath))
            {
                if (!Directory.Exists(_config.CachePath))
                {
                    Directory.CreateDirectory(_config.CachePath);
                }

                var serialId = _downloadManager.AddDownload(cachePath, url);
                var tcs = new TaskCompletionSource<bool>();
                _downloadingTasks.TryAdd(serialId, tcs);

                var downloadSuccess = await tcs.Task;
                if (!downloadSuccess)
                {
                    Log.Warning("ImageCache: download failed. url: {0}", url);
                    return null;
                }

                if (cancellationToken.IsCancellationRequested)
                {
                    return null;
                }
            }

            if (!FileHelper.IsExists(cachePath))
            {
                return null;
            }

            var buffer = FileHelper.ReadAllBytes(cachePath);
            if (buffer == null || buffer.Length == 0)
            {
                return null;
            }

            var texture = new Texture2D(1, 1, TextureFormat.RGBA32, false, false);
            if (!texture.LoadImage(buffer))
            {
                Object.Destroy(texture);
                return null;
            }

            return texture;
        }
#endif

        /// <summary>
        /// 检查指定 URL 的图片是否已缓存。
        /// WebGL 平台始终返回 false。
        /// </summary>
        /// <param name="url">远程图片地址。</param>
        /// <returns>已缓存返回 true，否则返回 false。</returns>
        public bool IsCached(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return false;
            }

#if UNITY_WEBGL
            return false;
#else
            var hash = Utility.Hash.MD5.Hash(url);
            var cachePath = PathHelper.Combine(_config.CachePath, hash + Utility.Const.FileNameSuffix.PNG);
            return FileHelper.IsExists(cachePath);
#endif
        }

        /// <summary>
        /// 移除指定 URL 的本地缓存文件。
        /// WebGL 平台不支持此操作。
        /// </summary>
        /// <param name="url">远程图片地址。</param>
        public void RemoveCache(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return;
            }

#if UNITY_WEBGL
            Log.Warning("ImageCache: RemoveCache is not supported on WebGL.");
#else
            var hash = Utility.Hash.MD5.Hash(url);
            var cachePath = PathHelper.Combine(_config.CachePath, hash + Utility.Const.FileNameSuffix.PNG);
            if (FileHelper.IsExists(cachePath))
            {
                FileHelper.Delete(cachePath);
            }
#endif
        }

        /// <summary>
        /// 清空所有本地缓存文件。
        /// WebGL 平台不支持此操作。
        /// </summary>
        public void ClearCache()
        {
#if UNITY_WEBGL
            Log.Warning("ImageCache: ClearCache is not supported on WebGL.");
#else
            if (!Directory.Exists(_config.CachePath))
            {
                return;
            }

            FileHelper.CleanDirectory(_config.CachePath);
#endif
        }

#if !UNITY_WEBGL
        /// <summary>
        /// 下载成功回调，根据序列号匹配对应的 TaskCompletionSource 并设置结果为 true。
        /// </summary>
        private void OnDownloadSuccess(object sender, DownloadSuccessEventArgs e)
        {
            if (_downloadingTasks.TryRemove(e.SerialId, out var tcs))
            {
                tcs.TrySetResult(true);
            }
        }

        /// <summary>
        /// 下载失败回调，根据序列号匹配对应的 TaskCompletionSource 并设置结果为 false。
        /// </summary>
        private void OnDownloadFailure(object sender, DownloadFailureEventArgs e)
        {
            if (_downloadingTasks.TryRemove(e.SerialId, out var tcs))
            {
                tcs.TrySetResult(false);
            }
        }
#endif

        protected override void Update(float elapseSeconds, float realElapseSeconds)
        {
        }

        /// <summary>
        /// 关闭并清理资源。
        /// </summary>
        protected override void Shutdown()
        {
#if !UNITY_WEBGL
            if (_downloadManager != null)
            {
                _downloadManager.DownloadSuccess -= OnDownloadSuccess;
                _downloadManager.DownloadFailure -= OnDownloadFailure;
                _downloadManager = null;
            }

            _downloadingTasks.Clear();
#endif
        }
    }
}
