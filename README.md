# ArtilleryCalculator

This is a simple artillery calculator for [Hell Let Loose](https://www.hellletloose.com/) that runs on your desktop. By running on the desktop, this tool is able to use [Windows hooks](https://docs.microsoft.com/en-us/windows/win32/winmsg/hooks) to monitor keyboard input. This allows you to quickly calculate the elevation for a given distance without alt-tabbing out of the game.

![image](https://user-images.githubusercontent.com/1060971/138877975-0ef6b655-24d6-48bf-bb07-23e47273ba87.png)

More information and screenshots here: https://www.hlltrainingcamp.com/forums/hell-let-loose/artillery-calculator-desktop-application

## Windows hook
When the application is running, it will optionally install a hook that receives every keyboard events in the system. The code for this functionnality can be read in [NumpadListener.cs](/ArtilleryCalculator/NumpadListener.cs). This can be used to monitor for numpad keypresses even when the game is focused. This is essentially the main reason why I made this tool, because I did not want to have to alt-tab out of my game or to use an app on my phone when playing on artillery. This is optional and unchecking the "Listen to numpad" option or closing the application will completely stop this monitoring. 

## Transparent mode

When enabled, the application will have a slightly reduced opacity, a smaller UI and it will be impossible to directly interact with it. When used with the "Stay on top" and "Listen to numpad" features, this makes the application behave more like a small overlay. Because the application will no longer be interactive when this is on, it will no longer be possible to accidentally click on it and lose game focus which was happening for some people (sorry🙃).

This new mode can be toggled via an hard-coded hotkey (Ctrl+Shift+K) or can be disabled by selecting the application window either via Alt+Tab or by clicking on its icon in the taskbar.

![image](https://user-images.githubusercontent.com/1060971/138877994-d1a8f3c6-96ac-4343-b597-2f66bdc34f3a.png)

## Future plans and contributing
If you find a bug or want to suggest a feature, please use the Github issues system. Here are a few ideas of thing I *may* work on in the future:
 - Showing a list of the last few conversions;
 - Offering an "overlay" mode that is simpler and meant to be displayed on top of the game.

## Credits
Application developed by me, Oceantume.

A big thank you to [Hell Let Loose Training Camp](https://www.hlltrainingcamp.com/) for your feedback, for sharing the distance to elevation formula and for being awesome!

The creator of this tool is in no ways affiliated with the creators of Hell Let Loose.
