using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectActionManager : MonoBehaviour
{
	public float actionDelay;

	public GameObject creditsPanel;

	public GameObject playBorder;
	public GameObject creditsBorder;
	public Button quitBtn;
	public ScrollPanel scrollPanel;

    enum ActionType { Play, Credits, Quit };
    ActionType currentType;
	float timer;
	bool actionReady;

    void Start()
    {
        ControllerInputWrapper.setControlTypes();
        ControllerInputWrapper.setPlatform();
        currentType = ActionType.Play;
    }

    void Update()
    {
		CheckSelected ();

		if(!actionReady){
			if(timer < actionDelay){
				timer += Time.deltaTime;
			}
			else{
				actionReady = true;
			}
		}
		else{
			timer = 0f;
		}

		float directX = ControllerInputWrapper.GetAxisRaw(ControllerInputWrapper.Axis.LeftStickX, 1);
		float directY = ControllerInputWrapper.GetAxisRaw(ControllerInputWrapper.Axis.LeftStickY, 1);
        switch (currentType)
        {
            case ActionType.Play:

				
                if (ControllerInputWrapper.GetButtonAll(ControllerInputWrapper.Buttons.A, true))
                {
                    Application.LoadLevel("SelectionScreen");
                }
				if (directX < -0.05f || directY < -0.05f && actionReady && !scrollPanel.isPlaying)
				{
					currentType = ActionType.Credits;
					actionReady = false;
				}
                break;
			case ActionType.Credits:
				if (ControllerInputWrapper.GetButtonAll(ControllerInputWrapper.Buttons.A, true) && !scrollPanel.isPlaying)
				{
					creditsPanel.SetActive(true);
					scrollPanel.isPlaying = true;
				}
				if (directX > 0.05f || directY < -0.05f && actionReady && !scrollPanel.isPlaying)
				{
					currentType = ActionType.Quit;
					actionReady = false;
				}
				if (directY > 0.05f && actionReady && !scrollPanel.isPlaying)
				{
					currentType = ActionType.Play;
					actionReady = false;
				}
				break;

            case ActionType.Quit:
				if (ControllerInputWrapper.GetButtonAll(ControllerInputWrapper.Buttons.A, true))
				{
					Application.Quit();
				}
				if (directX < -0.05f || directY > 0.05f && actionReady && !scrollPanel.isPlaying)
				{
					currentType = ActionType.Credits;
					actionReady = false;
				}
                break;
        }
    }

	void CheckSelected(){
		if(currentType != ActionType.Quit){
			GameObject myEventSystem = GameObject.Find("EventSystem");
			myEventSystem .GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
		}

		switch (currentType){
			case ActionType.Play:
				playBorder.SetActive(true);
				creditsBorder.SetActive(false);
				break;
			case ActionType.Credits:
				playBorder.SetActive(false);
				creditsBorder.SetActive(true);
				break;
				
			case ActionType.Quit:
				playBorder.SetActive(false);
				creditsBorder.SetActive(false);
				quitBtn.Select();
				break;
		}
	}
}