using UnityEngine;
using UnityEngine.Scripting;

namespace GameFrameX.ImageCache.Runtime
{
    /// <summary>
    /// 图片缓存裁剪辅助器，引用程序集中所有公开类型以防止 IL2CPP 代码裁剪。
    /// </summary>
    [Preserve]
    public class GameFrameXImageCacheCroppingHelper : MonoBehaviour
    {
        [Preserve]
        private void Start()
        {
            _ = typeof(ImageCacheComponent);
            _ = typeof(IImageCacheManager);
            _ = typeof(ImageCacheManager);
            _ = typeof(ImageCacheConfig);
        }
    }
}
