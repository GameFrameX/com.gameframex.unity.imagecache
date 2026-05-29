using System.Threading;
using System.Threading.Tasks;
using GameFrameX.Runtime;
using UnityEngine;

#if !UNITY_WEBGL
using GameFrameX.Download.Runtime;
#endif

namespace GameFrameX.ImageCache.Runtime
{
    /// <summary>
    /// 图片缓存组件。
    /// 提供远程图片的异步加载与磁盘缓存能力，可在 Inspector 中配置缓存路径等参数。
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("GameFrameX/Image Cache")]
    [UnityEngine.Scripting.Preserve]
    public sealed class ImageCacheComponent : GameFrameworkComponent
    {
        private IImageCacheManager _imageCacheManager;

        /// <summary>
        /// 图片缓存配置，可在 Inspector 中设置。
        /// </summary>
        [SerializeField]
        private ImageCacheConfig m_Config = new ImageCacheConfig();

        /// <summary>
        /// 获取图片缓存配置。
        /// </summary>
        public ImageCacheConfig Config
        {
            get { return m_Config; }
        }

        protected override void Awake()
        {
            ImplementationComponentType = Utility.Assembly.GetType(componentType);
            InterfaceComponentType = typeof(IImageCacheManager);
            base.Awake();
            _imageCacheManager = GameFrameworkEntry.GetModule<IImageCacheManager>();
            if (_imageCacheManager == null)
            {
                Log.Fatal("ImageCache manager is invalid.");
                return;
            }

            _imageCacheManager.Config.CachePath = m_Config.CachePath;
            _imageCacheManager.Config.MaxDiskSize = m_Config.MaxDiskSize;
            _imageCacheManager.Config.ExpireDays = m_Config.ExpireDays;

#if !UNITY_WEBGL
            var downloadManager = GameFrameworkEntry.GetModule<IDownloadManager>();
            if (downloadManager == null)
            {
                Log.Fatal("Download manager is invalid.");
                return;
            }

            ((ImageCacheManager)_imageCacheManager).SetDownloadManager(downloadManager);
#endif
        }

        /// <summary>
        /// 异步加载远程图片。
        /// </summary>
        /// <param name="url">远程图片地址（必须以 http:// 或 https:// 开头）。</param>
        /// <param name="cancellationToken">取消令牌。</param>
        /// <returns>加载成功返回 Texture2D，失败返回 null。</returns>
        public Task<Texture2D> LoadImageAsync(string url, CancellationToken cancellationToken = default)
        {
            return _imageCacheManager.LoadImageAsync(url, cancellationToken);
        }

        /// <summary>
        /// 检查指定 URL 的图片是否已缓存。
        /// </summary>
        /// <param name="url">远程图片地址。</param>
        /// <returns>已缓存返回 true，否则返回 false。</returns>
        public bool IsCached(string url)
        {
            return _imageCacheManager.IsCached(url);
        }

        /// <summary>
        /// 移除指定 URL 的本地缓存文件。
        /// </summary>
        /// <param name="url">远程图片地址。</param>
        public void RemoveCache(string url)
        {
            _imageCacheManager.RemoveCache(url);
        }

        /// <summary>
        /// 清空所有本地缓存文件。
        /// </summary>
        public void ClearCache()
        {
            _imageCacheManager.ClearCache();
        }
    }
}
