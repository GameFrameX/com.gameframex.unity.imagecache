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
  All-in-One Solution for Indie Game Development · Empowering Indie Developers' Dreams
</p>

<p align="center">
  <a href="https://gameframex.doc.alianblank.com">Documentation</a> ·
  <a href="#quick-start">Quick Start</a> ·
  <a href="https://qm.qq.com/q/3dIpogITg">QQ Group</a> ·
  Language: **English** ·
  <a href="README.zh-CN.md">简体中文</a> ·
  <a href="README.zh-TW.md">繁體中文</a> ·
  <a href="README.ja.md">日本語</a> ·
  <a href="README.ko.md">한국어</a>
</p>

---

## Project Overview

GameFrameX.ImageCache is the image caching component for the GameFrameX framework. It provides remote image downloading and disk caching for Unity projects. Images are cached on disk using MD5-based filenames, with configurable cache path and extensible support for max disk size and expiry.

**Platform Support:**
- Non-WebGL: Downloads images via `IDownloadManager` to disk cache, loads as `Texture2D`
- WebGL: Loads images via `UnityWebRequestTexture`, browser handles caching

## Quick Start

### Installation

Edit your Unity project's `Packages/manifest.json` and add the `scopedRegistries` section:

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

Then add the package to `dependencies`:

```json
{
  "dependencies": {
    "com.gameframex.unity.imagecache": "0.0.1"
  }
}
```

`scopes` controls which packages are resolved through this registry. Only packages whose names start with `com.gameframex` will be fetched from it.

### Usage

```csharp
// Get the ImageCache component
var imageCache = GameEntry.GetComponent<ImageCacheComponent>();

// Load a remote image asynchronously
Texture2D texture = await imageCache.LoadImageAsync("https://example.com/image.png");

// Check if an image is cached
bool cached = imageCache.IsCached("https://example.com/image.png");

// Remove a specific cached image
imageCache.RemoveCache("https://example.com/image.png");

// Clear all cached images
imageCache.ClearCache();
```

## Platform Support

| Platform | Strategy |
|----------|----------|
| iOS / Android / Windows / macOS | Disk cache via `IDownloadManager` |
| WebGL | Browser cache via `UnityWebRequestTexture` |

## Documentation & Resources

- [Official Documentation](https://gameframex.doc.alianblank.com)

## Community & Support

- QQ Group: [Join](https://qm.qq.com/q/3dIpogITg)

## Changelog

See [Releases](https://github.com/GameFrameX/com.gameframex.unity.imagecache/releases) for changelog.

## License

See [LICENSE.md](LICENSE.md) for license information.
