using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour
{

    VirtualJoystickSystem joystickSys;

    public Image BG;
    public Image Knob;
    public Vector2 InputDirection;
    public Vector2 KnobRange;

    public Vector3 debug;

    public void Start()
    {
        joystickSys = transform.parent.GetComponent<VirtualJoystickSystem>();
        //transform.parent.gameObject.SetActive(false);
    }

    public void OnEnable()
    {
        Reset();
    }

    public void OnDisable()
    {
        Reset();
    }

    public void Drag(PointerEventData ped)
    {
        Vector2 pos = Vector2.zero;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            BG.rectTransform,
            ped.position,
            ped.pressEventCamera,
            out pos
            ))
        {
            pos.x /= BG.rectTransform.sizeDelta.x / 2f;
            pos.y /= BG.rectTransform.sizeDelta.y / 2f;
            pos = pos.magnitude > 1f ? pos.normalized : pos;


            InputDirection = pos;

            Knob.rectTransform.anchoredPosition = new Vector3(
                pos.x * BG.rectTransform.sizeDelta.x * KnobRange.x / 2f,
                pos.y * BG.rectTransform.sizeDelta.y * KnobRange.y / 2f,
                0
                );
            debug = Knob.rectTransform.anchoredPosition;
        }
    }

    public void Reset()
    {
        Knob.rectTransform.anchoredPosition = Vector3.zero;
    }
}
