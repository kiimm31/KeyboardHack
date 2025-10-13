// See https://aka.ms/new-console-template for more information

using KeyboardHackerMan.Helper;

WindowsHelper.FocusWindow("MapleStory");
WindowsApiHelper.SendKeyPress(WindowsApiHelper.VirtualKeyCode.SPACE, 10000);
Console.WriteLine("Jumped!");