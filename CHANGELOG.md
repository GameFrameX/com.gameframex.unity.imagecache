# 1.0.0 (2026-05-30)


### Features

* **config:** CachePath 默认为空，运行时回退到可读写路径 ([a91f814](https://github.com/gameframex/com.gameframex.unity.imagecache/commit/a91f814c6b635a47554b56ed6968e898f04da407))
* **editor:** 补充 Inspector 缓存配置绘制 ([e33bf18](https://github.com/gameframex/com.gameframex.unity.imagecache/commit/e33bf18ea49daf691e7abbec0c5b11fc47f7330e))
* initial commit ([22312f5](https://github.com/gameframex/com.gameframex.unity.imagecache/commit/22312f54988b05ae72965e5890abf105c251c46c))

# Changelog

All notable changes to this project will be documented in this file. See [standard-version](https://github.com/conventional-changelog/standard-version) for commit guidelines.

## 0.0.1 (2026-05-28)

### Features

* 初始版本，支持远程图片下载与磁盘缓存
* WebGL 平台通过 UnityWebRequestTexture 加载，由浏览器管理缓存
* 非 WebGL 平台通过 IDownloadManager 下载到磁盘，以 URL 的 MD5 哈希作为文件名缓存
