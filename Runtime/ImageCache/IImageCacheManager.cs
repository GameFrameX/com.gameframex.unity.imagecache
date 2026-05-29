using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace GameFrameX.ImageCache.Runtime
{
    /// <summary>
    /// 图片缓存管理器接口。
    /// </summary>
    [UnityEngine.Scripting.Preserve]
    public interface IImageCacheManager
    {
        /// <summary>
        /// 获取图片缓存配置。
        /// </summary>
        ImageCacheConfig Config { get; }

        /// <summary>
        /// 异步加载远程图片。
        /// 非 WebGL 平台：优先从磁盘缓存读取，未命中时下载到本地后加载。
        /// WebGL 平台：通过 UnityWebRequestTexture 直接加载，由浏览器管理缓存。
        /// </summary>
        /// <param name="url">远程图片地址（必须以 http:// 或 https:// 开头）。</param>
        /// <param name="cancellationToken">取消令牌。</param>
        /// <returns>加载成功返回 Texture2D，失败返回 null。</returns>
        Task<Texture2D> LoadImageAsync(string url, CancellationToken cancellationToken = default);

        /// <summary>
        /// 检查指定 URL 的图片是否已缓存。
        /// WebGL 平台始终返回 false。
        /// </summary>
        /// <param name="url">远程图片地址。</param>
        /// <returns>已缓存返回 true，否则返回 false。</returns>
        bool IsCached(string url);

        /// <summary>
        /// 移除指定 URL 的本地缓存文件。
        /// WebGL 平台不支持此操作。
        /// </summary>
        /// <param name="url">远程图片地址。</param>
        void RemoveCache(string url);

        /// <summary>
        /// 清空所有本地缓存文件。
        /// WebGL 平台不支持此操作。
        /// </summary>
        void ClearCache();
    }
}
