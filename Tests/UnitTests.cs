using GameFrameX.ImageCache.Runtime;
using GameFrameX.Runtime;
using NUnit.Framework;

namespace GameFrameX.ImageCache.Tests
{
    internal class ImageCacheOptionsTests
    {
        [TearDown]
        public void TearDown()
        {
            GameFrameXOptions.Reset();
        }

        [Test]
        public void ImageCacheOptions_ConfigureAndTryGet_RoundTripsDifferentialValues()
        {
            GameFrameXOptions.Reset();
            GameFrameXOptions.Configure(new ImageCacheOptions
            {
                Config = new ImageCacheConfig { CachePath = "user://images/", MaxDiskSize = 1024, ExpireDays = 7 },
            });

            // 组件 Awake 路径依赖运行时 Host，退化为直接验证注册表消费语义（TryGet 即组件 Awake 的读取入口）
            bool hit = GameFrameXOptions.TryGet<ImageCacheOptions>(out var imageCacheOptions);

            Assert.IsTrue(hit, "Configure 后 TryGet 应命中");
            Assert.That(imageCacheOptions.Config.CachePath, Is.EqualTo("user://images/"));
            Assert.That(imageCacheOptions.Config.MaxDiskSize, Is.EqualTo(1024));
            Assert.That(imageCacheOptions.Config.ExpireDays, Is.EqualTo(7));
        }

        [Test]
        public void ImageCacheOptions_DefaultConfig_MatchesOriginalSerializedDefault()
        {
            GameFrameXOptions.Reset();
            GameFrameXOptions.Configure(new ImageCacheOptions());

            bool hit = GameFrameXOptions.TryGet<ImageCacheOptions>(out var imageCacheOptions);

            Assert.IsTrue(hit);
            Assert.That(imageCacheOptions.Config.CachePath, Is.EqualTo(string.Empty));
            Assert.That(imageCacheOptions.Config.MaxDiskSize, Is.EqualTo(0));
            Assert.That(imageCacheOptions.Config.ExpireDays, Is.EqualTo(0));
        }
    }
}
