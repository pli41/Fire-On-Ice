using UnityEngine;
using System.Collections;

//Class to handle mapping of different controllers in Unity over different OS(Windows and Mac Os X primarily). As of now coding for XBox 360 controller
public abstract class ControllerInputWrapper
{

    //Enums to handle all possible inputs on a controller. 3 Broad categories "Buttons", "Triggers" and "Axis". 
    //Linux environment is supported but for Dpad Keys only wired controllers are supported.
    //PS3 stuff yet to come. A,B,X and Y will map to corresponding buttons on PS3
    //A -> Square
    //B -> X
    //X -> Circle
    //Y -> Triangle

    //    private static string LEFT_OSX_TRIGGER = "LeftOSXTrigger";
    //    private static string LEFT_LINUX_TRIGGER = "LeftLinuxTrigger";
    //    private static string LEFT_WIN_TRIGGER = "LeftWinTrigger";
    //    private static string RIGHT_OSX_TRIGGER = "RightOSXTrigger";
    //    private static string RIGHT_LINUX_TRIGGER = "RightLinuxTrigger";
    //    private static string RIGHT_WIN_TRIGGER = "RightWinTrigger";
    //    private static string RIGHT_OSX_STICK_Y = "RightOSXStickY";
    //    private static string RIGHT_WIN_STICK_Y = "RightWinStickY";
    //    private static string RIGHT_OSX_STICK_X = "RightOSXStickX";
    //    private static string RIGHT_WIN_STICK_X = "RightWinStickX";
    //    private static string RIGHT_PS_STICK_X = "RightPSStickX";
    //    private static string RIGHT_PS_STICK_Y = "RightPSStickY";
    //    private static string LEFT_STICK_Y = "Vertical";
    //    private static string LEFT_STICK_X = "Horizontal";
    //    private static string DPAD_WIN_STICK_Y = "DpadWinStickY";
    //    private static string DPAD_WIN_STICK_X = "DpadWinStickX";
    //    private static string DPAD_LINUX_STICK_X = "DpadLinuxStickX";
    //    private static string DPAD_LINUX_STICK_Y = "DpadLinuxStickY";
    //    private static string DPAD_PS_STICK_X = "DpadPSStickX";
    //    private static string DPAD_PS_STICK_Y = "DpadPSStickY";

    public enum Axis { LeftStickY, LeftStickX, RightStickY, RightStickX, DPadY, DPadX};
    public enum Buttons { A, B, X, Y, RightBumper, LeftBumper, Back, Start, LeftStickClick, RightStickClick, Key_1, Key_2, Key_3, Key_4, Key_Fire, Key_Confirm};
    public enum Triggers { RightTrigger, LeftTrigger };

    public abstract bool GetButton(Buttons button, int joyNum, bool isDown = false);

    public abstract bool GetButtonUp(Buttons button, int joyNum);

    public abstract float GetAxis(Axis axis, int joyNum, bool isRaw = false);

    public abstract float GetTrigger(Triggers trigger, int joyNum, bool isRaw = false);

    protected virtual string getButtonName(int joyNum, string winID, string linID, string osxID)
    {
        string buttonName = "j" + joyNum + "_Button";
        switch (ControllerManager.currentOS)
        {
            case ControllerManager.OperatingSystem.Win:
                return buttonName + winID;
            case ControllerManager.OperatingSystem.Linux:
                return buttonName + linID;
            case ControllerManager.OperatingSystem.OSX:
                return buttonName + osxID;
            default:
                return null;
        }

    }

    protected virtual string getAxisName(int joyNum, string winID, string linID, string osxID)
    {
        string axisName = "j" + joyNum + "_Axis";
		//Debug.Log (ControllerManager.currentOS);
        switch (ControllerManager.currentOS)
        {
            case ControllerManager.OperatingSystem.Win:
                return axisName + winID;
            case ControllerManager.OperatingSystem.Linux:
                return axisName + linID;
            case ControllerManager.OperatingSystem.OSX:
                return axisName + osxID;
            default:
                return null;
        }
    }



}