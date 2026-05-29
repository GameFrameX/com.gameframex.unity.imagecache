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
  獨立遊戲前後端一體化解決方案 · 獨立遊戲開發者的圓夢大使
</p>

<p align="center">
  <a href="https://gameframex.doc.alianblank.com">文檔</a> ·
  <a href="#快速開始">快速開始</a> ·
  <a href="https://qm.qq.com/q/3dIpogITg">QQ群</a> ·
  語言: <a href="README.md">English</a> ·
  <a href="README.zh-CN.md">简体中文</a> ·
  **繁體中文** ·
  <a href="README.ja.md">日本語</a> ·
  <a href="README.ko.md">한국어</a>
</p>

---

## 項目簡介

GameFrameX.ImageCache 是 GameFrameX 框架的圖片快取元件。提供遠端圖片下載與磁碟快取功能，使用基於 MD5 的檔名快取到本地，支援可設定的快取路徑，並預留最大磁碟容量和過期時間的擴充能力。

**平台支援：**
- 非 WebGL：透過 `IDownloadManager` 下載圖片到磁碟快取，載入為 `Texture2D`
- WebGL：透過 `UnityWebRequestTexture` 載入圖片，由瀏覽器管理快取

## 快速開始

### 安裝方式

編輯 Unity 專案的 `Packages/manifest.json`，添加 `scopedRegistries` 部分：

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

然後在 `dependencies` 中添加依賴：

```json
{
  "dependencies": {
    "com.gameframex.unity.imagecache": "0.0.1"
  }
}
```

`scopes` 控制哪些套件透過此註冊表解析。只有以 `com.gameframex` 開頭的套件才會從這個註冊表取得。

### 使用範例

```csharp
// 取得圖片快取元件
var imageCache = GameEntry.GetComponent<ImageCacheComponent>();

// 非同步載入遠端圖片
Texture2D texture = await imageCache.LoadImageAsync("https://example.com/image.png");

// 檢查圖片是否已快取
bool cached = imageCache.IsCached("https://example.com/image.png");

// 移除指定快取
imageCache.RemoveCache("https://example.com/image.png");

// 清空所有快取
imageCache.ClearCache();
```

## 平台支援

| 平台 | 策略 |
|------|------|
| iOS / Android / Windows / macOS | 透過 `IDownloadManager` 磁碟快取 |
| WebGL | 透過 `UnityWebRequestTexture` 瀏覽器快取 |

## 文檔與資源

- [官方文檔](https://gameframex.doc.alianblank.com)

## 社區與支援

- QQ群: [加入](https://qm.qq.com/q/3dIpogITg)

## 更新日誌

查看 [Releases](https://github.com/GameFrameX/com.gameframex.unity.imagecache/releases) 了解更新日誌。

## 開源協議

本專案基於 Apache License 2.0 協議開源 - 詳見 [LICENSE](https://github.com/GameFrameX/com.gameframex.unity.imagecache/blob/main/LICENSE) 檔案。
