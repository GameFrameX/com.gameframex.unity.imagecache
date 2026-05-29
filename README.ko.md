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
  인디 게임 개발자를 위한 올인원 솔루션 · 인디 개발자의 꿈을 실현
</p>

<p align="center">
  <a href="https://gameframex.doc.alianblank.com">문서</a> ·
  <a href="#빠른-시작">빠른 시작</a> ·
  <a href="https://qm.qq.com/q/3dIpogITg">QQ 그룹</a> ·
  언어: <a href="README.md">English</a> ·
  <a href="README.zh-CN.md">简体中文</a> ·
  <a href="README.zh-TW.md">繁體中文</a> ·
  <a href="README.ja.md">日本語</a> ·
  **한국어**
</p>

---

## 프로젝트 개요

GameFrameX.ImageCache는 GameFrameX 프레임워크의 이미지 캐시 컴포넌트입니다. 원격 이미지 다운로드 및 디스크 캐시 기능을 제공하며, MD5 기반 파일명으로 로컬에 캐시합니다. 구성 가능한 캐시 경로를 지원하며, 최대 디스크 용량과 만료 기간 확장 기능을 갖추고 있습니다.

**플랫폼 지원:**
- 비 WebGL: `IDownloadManager`를 통해 이미지를 디스크 캐시에 다운로드하고 `Texture2D`로 로드
- WebGL: `UnityWebRequestTexture`를 통해 이미지를 로드하고 브라우저가 캐시 관리

## 빠른 시작

### 설치

Unity 프로젝트의 `Packages/manifest.json`을 편집하여 `scopedRegistries` 섹션을 추가하세요:

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

`dependencies`에 패키지를 추가하세요:

```json
{
  "dependencies": {
    "com.gameframex.unity.imagecache": "0.0.1"
  }
}
```

`scopes`는 이 레지스트리를 통해 어떤 패키지를 해석할지 제어합니다. `com.gameframex`로 시작하는 패키지만 이 레지스트리에서 가져옵니다.

### 사용 예시

```csharp
// 이미지 캐시 컴포넌트 가져오기
var imageCache = GameEntry.GetComponent<ImageCacheComponent>();

// 원격 이미지 비동기 로드
Texture2D texture = await imageCache.LoadImageAsync("https://example.com/image.png");

// 이미지가 캐시되었는지 확인
bool cached = imageCache.IsCached("https://example.com/image.png");

// 특정 캐시 제거
imageCache.RemoveCache("https://example.com/image.png");

// 모든 캐시 삭제
imageCache.ClearCache();
```

## 플랫폼 지원

| 플랫폼 | 방식 |
|--------|------|
| iOS / Android / Windows / macOS | `IDownloadManager`를 통한 디스크 캐시 |
| WebGL | `UnityWebRequestTexture`를 통한 브라우저 캐시 |

## 문서 및 자료

- [공식 문서](https://gameframex.doc.alianblank.com)

## 커뮤니티 및 지원

- QQ 그룹: [참가](https://qm.qq.com/q/3dIpogITg)

## 변경 로그

변경 로그는 [Releases](https://github.com/GameFrameX/com.gameframex.unity.imagecache/releases)에서 확인하세요.

## 라이선스

본 프로젝트는 Apache License 2.0에 따라 배포됩니다 - 자세한 내용은 [LICENSE](https://github.com/GameFrameX/com.gameframex.unity.imagecache/blob/main/LICENSE) 파일을 참조하세요.
