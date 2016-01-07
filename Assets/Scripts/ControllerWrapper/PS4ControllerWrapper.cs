using UnityEngine;
using System.Collections;
using System;

public class PS4ControllerWrapper : ControllerInputWrapper {

    public override float GetAxis(Axis axis, int joyNum, bool isRaw = false)
    {
        float scale = 1;
        string axisName = "";
        switch (axis)
        {
            case Axis.LeftStickX:
                axisName = getAxisName(joyNum, "X", "X", "X");
                break;
            case Axis.LeftStickY:
                axisName = getAxisName(joyNum, "Y", "Y", "Y");
                scale = -1;
                break;
            case Axis.RightStickX:
                axisName = getAxisName(joyNum, "3", "3", "3");
                break;
            case Axis.RightStickY:
                axisName = getAxisName(joyNum, "4", "4", "4");
                break;
        }

        if (isRaw)
        {
            return Input.GetAxisRaw(axisName) * scale;
        }
        else
        {
            return Input.GetAxis(axisName) * scale;
        }
    }

    public override bool GetButton(Buttons button, int joyNum, bool isDown = false)
    {
        string buttonName = getButtonHelper(button, joyNum);

        if (isDown)
        {
            return Input.GetButtonDown(buttonName);
        }
        return Input.GetButton(buttonName);
    }

    string getButtonHelper(Buttons button, int joyNum)
    {
        string buttonName = "";
        switch (button)
        {
            case Buttons.A:
                buttonName = getButtonName(joyNum, "1", "1", "1");
                break;
            case Buttons.B:
                buttonName = getButtonName(joyNum, "2", "2", "2");
                break;
            case Buttons.X:
                buttonName = getButtonName(joyNum, "0", "0", "0");
                break;
            case Buttons.Y:
                buttonName = getButtonName(joyNum, "3", "3", "3");
                break;
            case Buttons.LeftBumper:
                buttonName = getButtonName(joyNum, "4", "4", "4");
                break;
            case Buttons.RightBumper:
                buttonName = getButtonName(joyNum, "5", "5", "5");
                break;
            case Buttons.LeftStickClick:
                buttonName = getButtonName(joyNum, "0", "0", "0");
                break;
            case Buttons.RightStickClick:
                buttonName = getButtonName(joyNum, "0", "0", "0");
                break;
            case Buttons.Start:
                buttonName = getButtonName(joyNum, "12", "12", "12");
                break;
        }
        return buttonName;
    }

    public override bool GetButtonUp(Buttons button, int joyNum)
    {
        string buttonName = getButtonHelper(button, joyNum);

        return Input.GetButtonUp(buttonName);
    }

    public override float GetTrigger(Triggers trigger, int joyNum, bool isRaw = false)
    {
        string triggerName = "";
        switch (trigger)
        {
            case Triggers.LeftTrigger:
                triggerName = getAxisName(joyNum, "5", "5", "5");
                break;
            case Triggers.RightTrigger:
                triggerName = getAxisName(joyNum, "6", "6", "6");
                break;
        }
        if (isRaw)
        {
            return Input.GetAxisRaw(triggerName);
        }
        return Input.GetAxis(triggerName);

    }
}
