# Changelog

All notable changes to this project will be documented in this file. See [standard-version](https://github.com/conventional-changelog/standard-version) for commit guidelines.

## 0.0.1 (2026-05-28)

### Features

* 初始版本，支持远程图片下载与磁盘缓存
* WebGL 平台通过 UnityWebRequestTexture 加载，由浏览器管理缓存
* 非 WebGL 平台通过 IDownloadManager 下载到磁盘，以 URL 的 MD5 哈希作为文件名缓存
