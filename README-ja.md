[English](README.md) | [<u>**Japanese/日本語**</u>](README-ja.md)
# JPフォント修正ツール（JP Font Fixer）


Noto Sans JP を使用して、日本語のシステムフォントをインストールおよび上書きする小さな Windows 用ユーティリティです。


### なぜ必要？
MS Gothic や MS PGothic は正直… *見た目があまり良くありません。*  
これは、[Windows 3.1 向けに設計されたフォント](https://learn.microsoft.com/en-gb/typography/font-list/ms-gothic#:~:text=and%20licensed%20as%20the%20default%20system%20font%20for%20Windows%203.1)であり、*Windows 11 向けではない*ためです。

**以下の例を見てください：**

![img](https://i.ibb.co/LDg3QjRG/Screenshot-2026-04-20-131127.png)

一方で、Noto Sans JP はより見やすく、読みやすいフォントです。

---

## 機能

- Noto Sans JP フォントを Windows にインストール
- 以下の日本語システムフォントに対するレジストリ上書きを適用：
  - MS Gothic / MS ゴシック	
  - MS UI Gothic / MS UI ゴシック
  - MS PGothic / MS Pゴシック
- シンプルなGUIを提供：
  - フォントのインストール
  - 変更を元の MS フォントに戻す

---

## 動作要件

- Windows 10 / 11
- 管理者権限 *（フォントおよびレジストリ変更に必須）*

---

## 実行方法

### 方法1（開発用）
`dotnet run`

### 方法2（ビルド版）

**`FontFixerGUI.exe` を管理者として実行してください**

## 動作の仕組み

このアプリは以下の処理を行います：

Noto Sans JP のフォントファイルを `C:\Windows\Fonts` にコピー
次のレジストリキーにエントリを追加：
`HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts`
*`FontSubstitutes`* を使用してフォントの置き換えを適用
元に戻す方法

レジストリの上書きを削除することで、既定のフォント設定に復元できます。

### 注意事項
* 管理者権限が必要です
* システム全体のフォント動作に影響します
* インストール後、再起動が必要になる場合があります

## ライセンス

Copyright (C) 2026 David Maj/dmx3377 david@dmx3377.uk

本ソフトウェアはフリーかつオープンソースであり、Apache License 2.0 のもとで提供されています。詳細は [LICENSE](LICENSE) ファイル
 をご確認ください。

Noto Sans フォントについては、[LICENSE-Font](LICENSE-Font)
 を参照してください。
