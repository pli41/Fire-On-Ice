using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VirtualJoystickSystem : MonoBehaviour {
    public enum JoystickState { }

    /// <summary>
    /// if true, then the joystick will be hidden until pointer hits the joystick zone.
    /// </summary>
    public bool pushToEnable;
    /// <summary>
    /// joystick object
    /// </summary>
    [SerializeField]
    VirtualJoystick joystick;

    [SerializeField]
    /// <summary>
    /// spawn object
    /// </summary>
    VirtualJoystick_Spawn spawn;

	// Use this for initialization
	void Start () {
        if (pushToEnable)
        {
            joystick.gameObject.SetActive(false);
        }
            
	}

    /// <summary>
    /// 
    /// </summary>
    public void ShowJoystick(PointerEventData ped)
    {
        if (pushToEnable)
        {
            joystick.gameObject.SetActive(true);
            Debug.Log(ped.position);
            Vector2 pos = Vector2.zero;
            //RectTransformUtility.ScreenPointToLocalPointInRectangle()
            joystick.GetComponent<RectTransform>().anchoredPosition = ped.position;
        }
            
    }

    /// <summary>
    /// 
    /// </summary>
    public void HideJoystick()
    {
        if(pushToEnable)
            joystick.gameObject.SetActive(false);
    }   
}
