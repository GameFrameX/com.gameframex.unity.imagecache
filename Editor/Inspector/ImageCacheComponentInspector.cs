using GameFrameX.Editor;
using GameFrameX.ImageCache.Runtime;
using UnityEditor;
using UnityEngine;

namespace GameFrameX.ImageCache.Editor
{
    /// <summary>
    /// 图片缓存组件的自定义 Inspector，提供管理器实现类型的下拉选择和缓存配置编辑。
    /// </summary>
    [CustomEditor(typeof(ImageCacheComponent))]
    internal sealed class ImageCacheComponentInspector : ComponentTypeComponentInspector
    {

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            // C9 活视图：编辑态提示 ImageCacheOptions 入口；Play 态展开 Config（CachePath/MaxDiskSize/ExpireDays）实时编辑，经 ImageCacheConfig 公开属性写回。
            GameFrameXOptionsLiveViewUtility.DrawLiveView((ImageCacheComponent)target, typeof(ImageCacheOptions));
        }

        protected override void RefreshTypeNames()
        {
            RefreshComponentTypeNames(typeof(IImageCacheManager));
        }
    }
}
