using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectActionManager : MonoBehaviour
{
	public float actionDelay;

	public GameObject creditsPanel;

	public GameObject playBorder;
	public GameObject creditsBorder;

	public GameObject whiteCover;
	public float whiteCoverAnimDuration;

	public Button quitBtn;
	public ScrollPanel scrollPanel;

    public AudioSource scrollAS;
    public AudioSource selectAS;
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

				
				if (ControllerInputWrapper.GetButton(ControllerInputWrapper.Buttons.A, 1, true)||
				    ControllerInputWrapper.GetButton(ControllerInputWrapper.Buttons.A, 2, true)||
				    ControllerInputWrapper.GetButton(ControllerInputWrapper.Buttons.A, 3, true)||
				    ControllerInputWrapper.GetButton(ControllerInputWrapper.Buttons.A, 4, true)&&
				    !scrollPanel.isPlaying)
				{
                    selectAS.Stop();
                    selectAS.Play();
					WhiteOut();
                }
				if (directX < -0.05f || directY < -0.05f && actionReady && !scrollPanel.isPlaying)
				{
                    scrollAS.Stop();
                    scrollAS.Play();
					currentType = ActionType.Credits;
					actionReady = false;
				}
                break;
			case ActionType.Credits:
				if (ControllerInputWrapper.GetButton(ControllerInputWrapper.Buttons.A, 1, true)||
				    ControllerInputWrapper.GetButton(ControllerInputWrapper.Buttons.A, 2, true)||
				    ControllerInputWrapper.GetButton(ControllerInputWrapper.Buttons.A, 3, true)||
				    ControllerInputWrapper.GetButton(ControllerInputWrapper.Buttons.A, 4, true)&&
				    !scrollPanel.isPlaying)
				{

                    selectAS.Stop();
                    selectAS.Play();
                    creditsPanel.SetActive(true);
					scrollPanel.isPlaying = true;
				}
				if (directX > 0.05f || directY < -0.05f && actionReady && !scrollPanel.isPlaying)
				{
                    scrollAS.Stop();
                    scrollAS.Play();
                    currentType = ActionType.Quit;
					actionReady = false;
				}
				if (directY > 0.05f && actionReady && !scrollPanel.isPlaying)
				{
                    scrollAS.Stop();
                    scrollAS.Play();
                    currentType = ActionType.Play;
					actionReady = false;
				}
				break;

            case ActionType.Quit:
				if (ControllerInputWrapper.GetButtonAll(ControllerInputWrapper.Buttons.A, true))
				{

                    selectAS.Stop();
                    selectAS.Play();
                    Application.Quit();
				}
				if (directX < -0.05f || directY > 0.05f && actionReady && !scrollPanel.isPlaying)
				{
                    scrollAS.Stop();
                    scrollAS.Play();
                    currentType = ActionType.Credits;
					actionReady = false;
				}
                break;
        }
    }

	void WhiteOut(){
		Animator whiteCoverAnim = whiteCover.GetComponent<Animator> ();
		whiteCoverAnim.SetBool ("whiteOutStart", true);
		Invoke ("GoToSelectionScreen", whiteCoverAnimDuration);
	}

	void GoToSelectionScreen(){
		CancelInvoke ();
        SceneManager.LoadScene("SelectionScreen");
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