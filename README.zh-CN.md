<p align="center">
  <img src="https://download.alianblank.com/gameframex/gameframex_logo_320.png" alt="GameFrameX Logo" width="160" />
</p>

<h1 align="center">GameFrameX Image Cache</h1>

<p align="center">
  <a href="https://github.com/GameFrameX/com.gameframex.unity.imagecache/releases">
    <img src="https://img.shields.io/github/v/release/GameFrameX/com.gameframex.unity.imagecache?style=flat-square" alt="Version" />
  </a>
  <a href="https://github.com/GameFrameX/com.gameframex.unity.imagecache/blob/main/LICENSE">
    <img src="https://img.shields.io/github/license/GameFrameX/com.gameframex.unity.imagecache?style=flat-square" alt="License" />
  </a>
  <a href="https://gameframex.doc.alianblank.com">
    <img src="https://img.shields.io/badge/Documentation-online-blue?style=flat-square" alt="Documentation" />
  </a>
</p>

<p align="center">
  独立游戏前后端一体化解决方案 · 独立游戏开发者的圆梦大使
</p>

<p align="center">
  <a href="https://gameframex.doc.alianblank.com">文档</a> ·
  <a href="#快速开始">快速开始</a> ·
  <a href="https://qm.qq.com/q/3dIpogITg">QQ群</a> ·
  语言: <a href="README.md">English</a> ·
  **简体中文** ·
  <a href="README.zh-TW.md">繁體中文</a> ·
  <a href="README.ja.md">日本語</a> ·
  <a href="README.ko.md">한국어</a>
</p>

---

## 项目简介

GameFrameX.ImageCache 是 GameFrameX 框架的图片缓存组件。提供远程图片下载与磁盘缓存功能，使用基于 MD5 的文件名缓存到本地，支持可配置的缓存路径，并预留最大磁盘容量和过期时间的扩展能力。

**平台支持：**
- 非 WebGL：通过 `IDownloadManager` 下载图片到磁盘缓存，加载为 `Texture2D`
- WebGL：通过 `UnityWebRequestTexture` 加载图片，由浏览器管理缓存

## 快速开始

### 安装方式

编辑 Unity 项目的 `Packages/manifest.json`，添加 `scopedRegistries` 部分：

```json
{
  "scopedRegistries": [
    {
      "name": "GameFrameX",
      "url": "https://gameframex.upm.alianblank.uk",
      "scopes": [
        "com.gameframex"
      ]
    }
  ]
}
```

然后在 `dependencies` 中添加依赖：

```json
{
  "dependencies": {
    "com.gameframex.unity.imagecache": "0.0.1"
  }
}
```

`scopes` 控制哪些包通过此注册表解析。只有以 `com.gameframex` 开头的包才会从这个注册表获取。

### 使用示例

```csharp
// 获取图片缓存组件
var imageCache = GameEntry.GetComponent<ImageCacheComponent>();

// 异步加载远程图片
Texture2D texture = await imageCache.LoadImageAsync("https://example.com/image.png");

// 检查图片是否已缓存
bool cached = imageCache.IsCached("https://example.com/image.png");

// 移除指定缓存
imageCache.RemoveCache("https://example.com/image.png");

// 清空所有缓存
imageCache.ClearCache();
```

## 平台支持

| 平台 | 策略 |
|------|------|
| iOS / Android / Windows / macOS | 通过 `IDownloadManager` 磁盘缓存 |
| WebGL | 通过 `UnityWebRequestTexture` 浏览器缓存 |

## 文档与资源

- [官方文档](https://gameframex.doc.alianblank.com)

## 社区与支持

- QQ群: [加入](https://qm.qq.com/q/3dIpogITg)

## 更新日志

查看 [Releases](https://github.com/GameFrameX/com.gameframex.unity.imagecache/releases) 了解更新日志。

## 开源协议

详见 [LICENSE.md](LICENSE.md) 文件。
