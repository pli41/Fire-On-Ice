using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectActionManager : MonoBehaviour
{
    enum ActionType { Play, Quit };
    ActionType currentType;

    void Start()
    {
        ControllerInputWrapper.setControlTypes();
        ControllerInputWrapper.setPlatform();

        currentType = ActionType.Play;
    }

    void Update()
    {
        switch (currentType)
        {
            case ActionType.Play:
                if (ControllerInputWrapper.GetButtonAll(ControllerInputWrapper.Buttons.A, true))
                {
                    Application.LoadLevel("SelectionScreen");
                }
                break;

            case ActionType.Quit:

                break;
        }
    }
}