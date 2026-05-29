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
        private SerializedProperty _cachePath;
        private SerializedProperty _maxDiskSize;
        private SerializedProperty _expireDays;

        protected override void Enable()
        {
            var config = serializedObject.FindProperty("m_Config");
            _cachePath = config.FindPropertyRelative("m_CachePath");
            _maxDiskSize = config.FindPropertyRelative("m_MaxDiskSize");
            _expireDays = config.FindPropertyRelative("m_ExpireDays");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            serializedObject.Update();

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Image Cache Config", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_cachePath, new GUIContent("Cache Path"));
            EditorGUILayout.PropertyField(_maxDiskSize, new GUIContent("Max Disk Size (Bytes)"));
            EditorGUILayout.PropertyField(_expireDays, new GUIContent("Expire Days"));

            serializedObject.ApplyModifiedProperties();
        }

        protected override void RefreshTypeNames()
        {
            RefreshComponentTypeNames(typeof(IImageCacheManager));
        }
    }
}
