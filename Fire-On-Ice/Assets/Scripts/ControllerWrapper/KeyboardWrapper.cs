using UnityEngine;
using System.Collections;
using System;

public class KeyboardWrapper : ControllerInputWrapper {
    Camera mainCamera;
    Transform currentPlayer;

    public KeyboardWrapper()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    public override float GetAxis(Axis axis, int joyNum, bool isRaw = false)
    {
        string axisName = "";
        float scale = 1;
        Vector3 vec = Vector3.zero;
        switch (axis)
        {
            case Axis.LeftStickX:
                axisName = getAxisName(0, "Horizontal", "Horizontal", "Horizontal");
                scale = 0.09f;
                break;
            case Axis.LeftStickY:
                axisName = getAxisName(0, "Vertical", "Vertical", "Vertical");
                scale = 0.09f;
                break;
            case Axis.RightStickX:
                vec = retrieveMouseOffset();
                vec -= Vector3.up * vec.y;
                return vec.normalized.x;
                
            case Axis.RightStickY:
                vec = retrieveMouseOffset();
                vec -= Vector3.up * vec.y;
                return vec.normalized.z;
        }
        return Input.GetAxis(axisName) * scale;
    }

    Vector3 retrieveMouseOffset()
    {
        if (currentPlayer == null || mainCamera == null)
        {
            return Vector3.zero;
        }
        Ray checkRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(checkRay, out hit, 100))
        {
            Debug.Log(hit.collider.name);
            return hit.point - currentPlayer.position;
        }

        Debug.DrawLine(checkRay.origin, checkRay.origin + checkRay.direction * 100);
        
        return (checkRay.origin + checkRay.direction * 100) - currentPlayer.position;
    }

    public override float GetTrigger(Triggers trigger, int joyNum, bool isRaw = false)
    {

        string triggerName = "";
        switch (trigger)
        {
            case Triggers.RightTrigger:
                triggerName = getButtonName(0, "4", "4", "4");
                break;
            case Triggers.LeftTrigger:
                triggerName = getButtonName(0, "1", "1", "1");
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
                buttonName = getButtonName(0, "3", "3", "3");
                break;
            case Buttons.LeftBumper:
                buttonName = getButtonName(0, "2", "2", "2");
                break;
            case Buttons.A:
                buttonName = getButtonName(0, "Fire", "Fire", "Fire");
                break;
            case Buttons.Start:
                buttonName = getButtonName(0, "Confirm", "Confirm", "Confirm");
                break;
        }
        if (isDown)
        {
            return Input.GetButtonDown(buttonName);
        }
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

    public void setPlayer(Transform player)
    {
        this.currentPlayer = player;
        this.mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }
}
