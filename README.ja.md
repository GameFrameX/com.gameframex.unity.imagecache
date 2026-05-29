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
  インディゲーム開発者向けオールインワンソリューション · インディ開発者の夢を支援
</p>

<p align="center">
  <a href="https://gameframex.doc.alianblank.com">ドキュメント</a> ·
  <a href="#クイックスタート">クイックスタート</a> ·
  <a href="https://qm.qq.com/q/3dIpogITg">QQグループ</a> ·
  言語: <a href="README.md">English</a> ·
  <a href="README.zh-CN.md">简体中文</a> ·
  <a href="README.zh-TW.md">繁體中文</a> ·
  **日本語** ·
  <a href="README.ko.md">한국어</a>
</p>

---

## プロジェクト概要

GameFrameX.ImageCache は GameFrameX フレームワークの画像キャッシュコンポーネントです。リモート画像のダウンロードとディスクキャッシュ機能を提供し、MD5 ベースのファイル名でローカルにキャッシュします。設定可能なキャッシュパスをサポートし、最大ディスク容量と有効期限の拡張機能を備えています。

**プラットフォーム対応：**
- 非 WebGL：`IDownloadManager` 経由で画像をディスクキャッシュにダウンロードし、`Texture2D` として読み込む
- WebGL：`UnityWebRequestTexture` 経由で画像を読み込み、ブラウザがキャッシュを管理

## クイックスタート

### インストール

Unity プロジェクトの `Packages/manifest.json` を編集し、`scopedRegistries` セクションを追加してください：

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

`dependencies` にパッケージを追加してください：

```json
{
  "dependencies": {
    "com.gameframex.unity.imagecache": "0.0.1"
  }
}
```

`scopes` は、どのパッケージをこのレジストリから解決するかを制御します。`com.gameframex` で始まるパッケージのみがこのレジストリから取得されます。

### 使用例

```csharp
// 画像キャッシュコンポーネントを取得
var imageCache = GameEntry.GetComponent<ImageCacheComponent>();

// リモート画像を非同期で読み込む
Texture2D texture = await imageCache.LoadImageAsync("https://example.com/image.png");

// 画像がキャッシュされているか確認
bool cached = imageCache.IsCached("https://example.com/image.png");

// 特定のキャッシュを削除
imageCache.RemoveCache("https://example.com/image.png");

// 全キャッシュをクリア
imageCache.ClearCache();
```

## プラットフォーム対応

| プラットフォーム | 方式 |
|------------------|------|
| iOS / Android / Windows / macOS | `IDownloadManager` 経由のディスクキャッシュ |
| WebGL | `UnityWebRequestTexture` 経由のブラウザキャッシュ |

## ドキュメントとリソース

- [公式ドキュメント](https://gameframex.doc.alianblank.com)

## コミュニティとサポート

- QQグループ: [参加](https://qm.qq.com/q/3dIpogITg)

## 変更履歴

変更履歴は [Releases](https://github.com/GameFrameX/com.gameframex.unity.imagecache/releases) をご確認ください。

## ライセンス

本プロジェクトは Apache License 2.0 のもとで公開されています - 詳細は [LICENSE](https://github.com/GameFrameX/com.gameframex.unity.imagecache/blob/main/LICENSE) ファイルをご参照ください。
