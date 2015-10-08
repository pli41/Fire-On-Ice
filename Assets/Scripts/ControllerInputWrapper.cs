using UnityEngine;
using System.Collections;

//Class to handle mapping of different controllers in Unity over different OS(Windows and Mac Os X primarily). As of now coding for XBox 360 controller
public static class ControllerInputWrapper
{

    //Enums to handle all possible inputs on a controller. 3 Broad categories "Buttons", "Triggers" and "Axis". 
    //Linux environment is supported but for Dpad Keys only wired controllers are supported.
    //PS3 stuff yet to come. A,B,X and Y will map to corresponding buttons on PS3
    //A -> Square
    //B -> X
    //X -> Circle
    //Y -> Triangle
    public enum Axis { LeftStickY, LeftStickX, RightStickY, RightStickX, DPadY, DPadX };
    public enum Buttons { A, B, X, Y, RightBumper, LeftBumper, Back, Start, LeftStickClick, RightStickClick };
    public enum Triggers { RightTrigger, LeftTrigger };
    public enum ControlType { Xbox, PS3, PS4 };
    public enum OperatingSystem { Win, OSX, Linux };
    private static string LEFT_OSX_TRIGGER = "LeftOSXTrigger";
    private static string LEFT_LINUX_TRIGGER = "LeftLinuxTrigger";
    private static string LEFT_WIN_TRIGGER = "LeftWinTrigger";
    private static string RIGHT_OSX_TRIGGER = "RightOSXTrigger";
    private static string RIGHT_LINUX_TRIGGER = "RightLinuxTrigger";
    private static string RIGHT_WIN_TRIGGER = "RightWinTrigger";
    private static string RIGHT_OSX_STICK_Y = "RightOSXStickY";
    private static string RIGHT_WIN_STICK_Y = "RightWinStickY";
    private static string RIGHT_OSX_STICK_X = "RightOSXStickX";
    private static string RIGHT_WIN_STICK_X = "RightWinStickX";
    private static string RIGHT_PS_STICK_X = "RightPSStickX";
    private static string RIGHT_PS_STICK_Y = "RightPSStickY";
    private static string LEFT_STICK_Y = "Vertical";
    private static string LEFT_STICK_X = "Horizontal";
    private static string DPAD_WIN_STICK_Y = "DpadWinStickY";
    private static string DPAD_WIN_STICK_X = "DpadWinStickX";
    private static string DPAD_LINUX_STICK_X = "DpadLinuxStickX";
    private static string DPAD_LINUX_STICK_Y = "DpadLinuxStickY";
    private static string DPAD_PS_STICK_X = "DpadPSStickX";
    private static string DPAD_PS_STICK_Y = "DpadPSStickY";

    public static OperatingSystem currentSystem;
    public static ControlType[] controllerNames;

    public static void setPlatform()
    {
        string OSstring = SystemInfo.operatingSystem.ToString();
        Debug.Log(OSstring);
        if (OSstring.Contains("Windows"))
        {
            currentSystem = OperatingSystem.Win;
        }
        else if (OSstring.Contains("OS X"))
        {
            currentSystem = OperatingSystem.OSX;
        }
        // Debug.Log("Hello");
    }

    public static void setControlTypes()
    {
        string[] names = Input.GetJoystickNames();
        controllerNames = new ControlType[names.Length];
        int i = 0;
        foreach (string name in names)
        {
            Debug.Log(name);
            if (name.Contains("Xbox"))
            {
                controllerNames[i] = ControlType.Xbox;
            }
            else if (name.Contains("Wireless"))
            {
                controllerNames[i] = ControlType.PS4;
            }
            else if (name.Contains("PLAYSTATION"))
            {
                controllerNames[i] = ControlType.PS3;
            }
            i++;
        }
    }

    static string retrieveCorrectTrigger(Triggers trigger, int joystickNumber)
    {
        ControlType cType = controllerNames[joystickNumber];
        if (cType.CompareTo(ControlType.Xbox) == 0)
        {
            switch (trigger)
            {
                case Triggers.LeftTrigger:
                    switch (currentSystem)
                    {
                        case OperatingSystem.Win:
                            return "9";
                        case OperatingSystem.OSX:
                            return "5";
                        case OperatingSystem.Linux:
                            return "3";
                    }
                    break;
                case Triggers.RightTrigger:
                    switch (currentSystem)
                    {
                        case OperatingSystem.Win:
                            return "10";
                        case OperatingSystem.OSX:
                        case OperatingSystem.Linux:
                            return "6";
                    }
                    break;
            }
        }
        else if (cType.CompareTo(ControlType.PS4) == 0)
        {
            switch (trigger)
            {
                case Triggers.LeftTrigger:
                    return "5";
                case Triggers.RightTrigger:
                    return "6";
            }
        }
        else if (cType.CompareTo(ControlType.PS3) == 0)
        {
            switch (trigger)
            {
                case Triggers.LeftTrigger:
                    return "5";
                case Triggers.RightTrigger:
                    return "6";
            }

        }
        return "q";
    }


    public static float GetTriggerRaw(Triggers trigger, int joystickNumber)
    {
        if (joystickNumber - 1 >= controllerNames.Length)
        {
            return 0;
        }
        string inputName = "j" + joystickNumber + "_Axis";
        //This checks of the case of a PS3 controller.
        if (controllerNames[joystickNumber - 1].CompareTo(ControlType.PS3) == 0)
        {
            inputName = "j" + joystickNumber + "_Button";
            switch (trigger)
            {
                case Triggers.LeftTrigger:
                    if (Input.GetButton(inputName + "8"))
                    {
                        return 1;
                    }
                    else return 0;
                case Triggers.RightTrigger:
                    if (Input.GetButton(inputName + "9"))
                    {
                        return 1;
                    }
                    else return 0;

            }
        }
        /////////////////////////////////////////////////////////////////
        inputName = inputName + retrieveCorrectTrigger(trigger, joystickNumber - 1);
        
        return Input.GetAxisRaw(inputName);
    }

    static string retrieveCorrectButton(Buttons button, int joystickNumber)
    {
        Debug.Log("Joystick number: " + joystickNumber);
        ControlType cType = controllerNames[joystickNumber - 1];
        if (cType.CompareTo(ControlType.Xbox) == 0)
        {
            switch (button)
            {
                case Buttons.RightBumper:
                    switch (currentSystem)
                    {
                        case OperatingSystem.Win:
                        case OperatingSystem.Linux:
                            return "5";
                        case OperatingSystem.OSX:
                            return "14";
                    }
                    break;
            }
        }
        else if (cType.CompareTo(ControlType.PS4) == 0)
        {
            switch (button)
            {
                case Buttons.RightBumper:
                    return "5";
                case Buttons.A:
                    return "1";

            }
        }
        else if (cType.CompareTo(ControlType.PS3) == 0)
        {
            switch (button)
            {
                case Buttons.RightBumper:
                    return "11";
            }
        }

        return "q";
    }



    public static bool GetButton(Buttons button, int joystickNumber, bool buttonDown = false)
    {
        if (joystickNumber - 1 >= controllerNames.Length)
        {
            return false;
        }
        string inputName = "j" + joystickNumber + "_Button";
        inputName += retrieveCorrectButton(button, joystickNumber);
        if (!buttonDown)
        {
            return Input.GetButton(inputName);
        }
        return Input.GetButtonDown(inputName);
    }

    public static float GetAxisRaw(Axis axis, int joyStickNumber, bool isRaw = true)
    {
        if (joyStickNumber - 1 >= controllerNames.Length)
        {
            return 0;
        }
        string inputName = "j" + joyStickNumber + "_Axis";
        inputName = inputName + retrieveCorrectAxis(axis, joyStickNumber - 1);
        int scale = getAxisScale(axis, joyStickNumber - 1);
        if (isRaw)
			return Input.GetAxisRaw(inputName) * scale;
        else
			return Input.GetAxis(inputName) * scale;
    }

    static int getAxisScale(Axis axis, int joystickNumber)
    {
        ControlType cType = controllerNames[joystickNumber];
        switch (cType)
        {
            case ControlType.PS4:
            case ControlType.PS3:
                switch(axis)
                {
                    case Axis.LeftStickY:
                        return -1;
                    case Axis.RightStickY:
                        return -1;
                }
                break;
        }   
        return 1;
    }

    static char retrieveCorrectAxis(Axis axis, int joystickNumber)
    {
        ControlType cType = controllerNames[joystickNumber];
        if (cType.CompareTo(ControlType.Xbox) == 0)
        {
            switch (axis)
            {
                case Axis.LeftStickY:
                    return 'Y';
                case Axis.LeftStickX:
                    return 'X';
                case Axis.RightStickX:
                    switch (currentSystem)
                    {
                        case OperatingSystem.Win:
                        case OperatingSystem.Linux:
                            return '4';
                        case OperatingSystem.OSX:
                            return '3';
                    }
                    break;
                case Axis.RightStickY:
                    switch (currentSystem)
                    {
                        case OperatingSystem.Win:
                        case OperatingSystem.Linux:
                            return '5';
                        case OperatingSystem.OSX:
                            return '4';
                    }
                    break;

            }

        }
        else if (cType.CompareTo(ControlType.PS3) == 0)
        {
            switch (axis)
            {
                case Axis.LeftStickX:
                    return 'X';
                case Axis.LeftStickY:
                    return 'Y';
                case Axis.RightStickX:
                    return '3';
                case Axis.RightStickY:
                    return '4';
            }
        }
        else if (cType.CompareTo(ControlType.PS4) == 0)
        {
            switch (axis)
            {
                case Axis.LeftStickX:
                    return 'X';
                case Axis.LeftStickY:
                    return 'Y';
                case Axis.RightStickX:
                    return '3';
                case Axis.RightStickY:
                    return '4';
            }
        }
        return 'q'; //q does not exist
    }


}