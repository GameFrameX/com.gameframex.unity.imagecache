## [Unreleased]

### ⚠ BREAKING CHANGES

* **module:** 新增 `ImageCacheOptions` 启动配置（`GameFrameXOptions.Configure`，首场景加载前注入），收编原 `ImageCacheConfig` 序列化配置（`ImageCacheOptions.Config`，未配置时与原序列化默认值同源）
* **module:** 组件值参数序列化字段移除，编辑期配置入口收敛到代码 Options；`ImageCacheComponent` 保留为薄入口，模块经 `GetModule<T>()` 直达

**迁移对照：**

| 旧访问方式 | 新访问方式 | 说明 |
|---|---|---|
| 组件上的 `ImageCacheConfig` 序列化配置 | `ImageCacheOptions.Config`（`GameFrameXOptions.Configure`） | 配置整体收编为启动期注入 |

# [2.0.0](https://github.com/gameframex/com.gameframex.unity.imagecache/compare/1.0.0...2.0.0) (2026-10-08)


### ⚠ BREAKING CHANGES

* **module:** 模块契约全接口化：删除 `GameFrameworkModule` 抽象基类，改为 `IGameFrameworkModule` 标记接口 + 可选能力接口

**新契约形态与迁移对照：**

| 旧基类成员 | 新契约 | 迁移方式 |
|---|---|---|
| `: GameFrameworkModule` | `IGameFrameworkModule` | 声明处直接替换（标记接口，可注册进 `GameFrameworkEntry`） |
| `override void Update(float, float)` | `IUpdatableModule` | 真实实现改 `public`；空实现删除（未实现者不再被轮询） |
| `override void Shutdown()` | `IShutdownableModule` | 真实实现改 `public`；空实现删除（未实现者不再被关闭） |
| `override int Priority` | `IPrioritizedModule` | 改 `public int Priority { get; }`；未实现视为 0 |

本包变更：`ImageCacheManager` 实现 `IShutdownableModule`，删除空 `Update` 覆写。

`GameFrameworkEntry.Update` 仅分派 `IUpdatableModule`；`Shutdown` 按优先级反序仅分派 `IShutdownableModule`；tick 与关闭顺序与旧实现完全一致。能力方法仅由宿主入口调用，业务侧继续通过 `GetModule<IXxxManager>()` 访问模块。

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
