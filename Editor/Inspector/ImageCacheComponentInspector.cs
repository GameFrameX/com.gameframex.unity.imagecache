using GameFrameX.Editor;
using GameFrameX.ImageCache.Runtime;
using UnityEditor;

namespace GameFrameX.ImageCache.Editor
{
    /// <summary>
    /// 图片缓存组件的自定义 Inspector，提供管理器实现类型的下拉选择。
    /// </summary>
    [CustomEditor(typeof(ImageCacheComponent))]
    internal sealed class ImageCacheComponentInspector : ComponentTypeComponentInspector
    {
        protected override void RefreshTypeNames()
        {
            RefreshComponentTypeNames(typeof(IImageCacheManager));
        }
    }
}
