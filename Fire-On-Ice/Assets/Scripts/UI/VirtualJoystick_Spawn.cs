using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VirtualJoystick_Spawn : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    /// <summary>
    /// parent object of the joystick script attach object.
    /// </summary>
    [SerializeField]
    VirtualJoystick joystick;
    VirtualJoystickSystem joystickSys;

    public void Start()
    {
        joystickSys = transform.parent.GetComponent<VirtualJoystickSystem>();
    }

    //handle enabling of joystick here, and disabling of joystick inside joystick class.
    public virtual void OnPointerDown(PointerEventData ped)
    {
        Debug.Log("On Pointer Down");
        if (joystickSys.pushToAppear)
        {
            joystickSys.ShowJoystick(ped);
        }
    }

    public virtual void OnDrag(PointerEventData ped)
    {
        joystick.Drag(ped);
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        Debug.Log("On Pointer Up");
        joystickSys.HideJoystick();

    }
}
