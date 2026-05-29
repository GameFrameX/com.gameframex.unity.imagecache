using System;
using UnityEngine;

namespace GameFrameX.ImageCache.Runtime
{
    /// <summary>
    /// 图片缓存配置。
    /// </summary>
    [Serializable]
    public sealed class ImageCacheConfig
    {
        /// <summary>
        /// 缓存文件存放路径。
        /// </summary>
        [SerializeField]
        private string m_CachePath;

        /// <summary>
        /// 磁盘缓存最大容量（字节）。0 表示不限制。
        /// </summary>
        [SerializeField]
        private long m_MaxDiskSize;

        /// <summary>
        /// 缓存过期天数。0 表示永不过期。
        /// </summary>
        [SerializeField]
        private int m_ExpireDays;

        public ImageCacheConfig()
        {
            m_CachePath = string.Empty;
            m_MaxDiskSize = 0;
            m_ExpireDays = 0;
        }

        /// <summary>
        /// 获取或设置缓存文件存放路径。
        /// </summary>
        public string CachePath
        {
            get { return m_CachePath; }
            set { m_CachePath = value; }
        }

        /// <summary>
        /// 获取或设置磁盘缓存最大容量（字节）。0 表示不限制。
        /// </summary>
        public long MaxDiskSize
        {
            get { return m_MaxDiskSize; }
            set { m_MaxDiskSize = value; }
        }

        /// <summary>
        /// 获取或设置缓存过期天数。0 表示永不过期。
        /// </summary>
        public int ExpireDays
        {
            get { return m_ExpireDays; }
            set { m_ExpireDays = value; }
        }
    }
}
