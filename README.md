# JP Font Fixer

A small Windows utility that installs and overrides Japanese system fonts using Noto Sans JP.


### Why though?
MS Gothic and MS PGothic look... *ugly.*
It's because [they were designed for Windows 3.1](https://learn.microsoft.com/en-gb/typography/font-list/ms-gothic#:~:text=and%20licensed%20as%20the%20default%20system%20font%20for%20Windows%203.1), *not* Windows 11.

**Just look at this example:**

![img](https://i.ibb.co/LDg3QjRG/Screenshot-2026-04-20-131127.png)

Noto Sans JP however looks nicer and is more readable.

---

## What it does:

- Installs Noto Sans JP fonts into Windows
- Applies registry overrides for these Japanese system fonts:
  - MS Gothic
  - MS UI Gothic
  - MS PGothic
- Provides a simple GUI to:
  - Install fonts
  - Revert changes back to the default MS fonts

---

## Requirements

- Windows 10 / 11
- Administrator privileges *(required for font + registry changes)*

---

## How to run

### Option 1 (Development)
```bash
dotnet run
```


### Option 2 (Published build)

**Run `FontFixerGUI.exe` as Administrator**

## How it works

The app:

1. Copies Noto Sans JP font files into C:\Windows\Fonts
2. Adds registry entries under: `HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts`
3. Applies font substitutions via: `FontSubstitutes`

#### Revert option

The app can restore the default font mappings by removing registry overrides.

---

## Notes
* Requires admin rights
* Changes system-wide font behaviour
* Restart may be required after installation


## License
Copyright (C) 2026 David Maj/dmx3377 <david@dmx3377.uk>

This is free and open-source work, and is licensed under the Apache 2.0 License. See the [LICENSE file](LICENSE) for further information.

For the Noto Sans font, please see the [LICENSE-Font](LICENSE-Font) file.