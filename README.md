# Discord Tray Icon Fixer

For reasons unknown, the Discord developers insist on including the version number in Discord's folder name, instead of just using "Discord". 

While this seems harmless at first, if you prefer to keep the tray icon visible you will probably have noticed that the tray icon is automatically hidden every single time Discord updates (which is constantly). This happens because of the folder name change and the Discord updater removing and re-creating the tray icon registry key with the new path, defaulting it hidden.

This has been a complaint for years now and while the Discord developers have acknowledged it, they appear to have no interest in fixing it (even though it would be incredibly trivial to do so). This little helper program aims to resolve this issue.

# How it Works

This program utilizes the fact that Discord is launched by a seperate executable in a location that never changes with updates. 

The default shortcuts for Discord do not point to the Discord executable itself, but rather an updater executable: `%LocalAppData%\Discord\Update.exe --processStart Discord.exe`
This updater launches first, checks for and applies updates if available, and then launches Discord (somehow determining the path with the version number).

**We take advantage of this two stage process as follows:**

1. Launch the `%LocalAppData%\Discord\Update.exe` with the `--processStart Discord.exe` argument.
2. Wait for the Update process to complete and launch Discord.
3. Find the Discord tray icon registry key and ensure it is marked as visible.

Waiting for the Update process allows us to be sure that if an update was applied and the registry key was changed, we don't change the key before the updater does.

The project is configured to build a Window application, rather than a Console application. Because there are no windows, the application will run silently.

# How to Use

1. Build or download the exexcutable.
2. Place the executable in `%LocalAppData%\Discord` (it can tehcnically be anywhere, but this is the most logical location).
3. Update your Discord shortcut properties (including the Start Menu shortcut) to point to `%LocalAppData%\Discord\DiscordTrayIconFixer.exe`

If you have Discord set to launch on start up, you will need to disable it and manually add a shortcut to `%LocalAppData%\Discord\DiscordTrayIconFixer.exe` in your startup folder.
