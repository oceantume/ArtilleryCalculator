# ArtilleryCalculator

This is a simple artillery calculator for [Hell Let Loose](https://www.hellletloose.com/) that runs on your desktop. By running on the desktop, this tool is able to use [Windows hooks](https://docs.microsoft.com/en-us/windows/win32/winmsg/hooks) to monitor keyboard input. This allows you to quickly calculate the elevation for a given distance without alt-tabbing out of the game.

## Windows hook?
When the application is running, it will optionally install a hook that receives every keyboard events in the system. This can be used to monitor for numpad keypresses even when the game is focused. This is essentially the main reason why I made this tool, because I did not want to have to alt-tab out of my game or to use an app on my phone when playing on artillery.

## Future plans and contributing
If you find a bug or want to suggest a feature, please use the Github issues system. Here are a few ideas of thing I *may* work on in the future:
 - Showing a list of the last few conversions;
 - Offering an "overlay" mode that is simpler and meant to be displayed on top of the game.
