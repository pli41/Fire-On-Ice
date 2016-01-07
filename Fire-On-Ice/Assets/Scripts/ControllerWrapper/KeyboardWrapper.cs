using UnityEngine;
using System.Collections;
using System;

public class KeyboardWrapper : ControllerInputWrapper {

    public override float GetAxis(Axis axis, int joyNum, bool isRaw = false)
    {
        string axisName = "";
        switch (axis)
        {
            case Axis.LeftStickX:
                axisName = getAxisName(0, "Horizontal", "Horizontal", "Horizontal");
                break;
            case Axis.LeftStickY:
                axisName = getAxisName(0, "Vertical", "Vertical", "Vertical");
                break;
        }
        return Input.GetAxis(axisName);
    }

    public override float GetTrigger(Triggers trigger, int joyNum, bool isRaw = false)
    {
        string triggerName = "";
        switch (trigger)
        {
            case Triggers.RightTrigger:
                triggerName = getButtonName(0, "1", "1", "1");
                break;
            case Triggers.LeftTrigger:
                triggerName = getButtonName(0, "4", "4", "4");
                break;
        }

        if (Input.GetButton(triggerName))
        {
            return 1f;
        }
        else
        {
            return 0f;
        }
    }

    public override bool GetButton(Buttons button, int joyNum, bool isDown = false)
    {
        string buttonName = "";
        switch (button)
        {
            case Buttons.RightBumper:
                buttonName = getButtonName(0, "2", "2", "2");
                break;
            case Buttons.LeftBumper:
                buttonName = getButtonName(0, "3", "3", "3");
                break;
            case Buttons.A:
                buttonName = getButtonName(0, "Fire", "Fire", "Fire");
                break;
            case Buttons.Start:
                buttonName = getButtonName(0, "Confirm", "Confirm", "Confirm");
                break;
        }
        Debug.Log(buttonName);
        return Input.GetButton(buttonName);
    }

    public override bool GetButtonUp(Buttons button, int joyNum)
    {
        throw new NotImplementedException();
    }

    protected override string getAxisName(int joyNum, string winID, string linID, string osxID)
    {
        string axisName = "k" + "_Axis_" + winID;


        return axisName;
    }

    protected override string getButtonName(int joyNum, string winID, string linID, string osxID)
    {
        string buttonName = "k" + "_Button_" + winID;
        return buttonName;
    }
}
