using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectActionManager : MonoBehaviour
{
	public float actionDelay;

	public GameObject creditsPanel;

	public Image playBorder;
	public Image creditsBorder;

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
        ControllerManager.setUpControls();
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

        //Mouse


		float directX = ControllerManager.GetAxis(ControllerInputWrapper.Axis.LeftStickX, 1, true);
		float directY = ControllerManager.GetAxis(ControllerInputWrapper.Axis.LeftStickY, 1, true);
        switch (currentType)
        {
            case ActionType.Play:

				
				if (ControllerManager.GetButton(ControllerInputWrapper.Buttons.A, 1, true)||
				    ControllerManager.GetButton(ControllerInputWrapper.Buttons.A, 2, true)||
				    ControllerManager.GetButton(ControllerInputWrapper.Buttons.A, 3, true)||
				    ControllerManager.GetButton(ControllerInputWrapper.Buttons.A, 4, true)&&
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
				if (ControllerManager.GetButton(ControllerInputWrapper.Buttons.A, 1, true)||
				    ControllerManager.GetButton(ControllerInputWrapper.Buttons.A, 2, true)||
				    ControllerManager.GetButton(ControllerInputWrapper.Buttons.A, 3, true)||
				    ControllerManager.GetButton(ControllerInputWrapper.Buttons.A, 4, true)&&
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
                if (ControllerManager.GetButtonAll(ControllerInputWrapper.Buttons.A, true))
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

		//switch (currentType){
		//	case ActionType.Play:
		//		playBorder.enabled = true;
		//		creditsBorder.enabled = false;
		//		break;
		//	case ActionType.Credits:
		//		playBorder.enabled = false;
		//		creditsBorder.enabled = true;
		//		break;
				
		//	case ActionType.Quit:
		//		playBorder.enabled = false;
		//		creditsBorder.enabled = false;
		//		quitBtn.Select();
		//		break;
		//}
	}

    public void HighlightPlayButton()
    {
        playBorder.enabled = true;
        playBorder.GetComponent<RectTransform>().Find("Image").gameObject.SetActive(true);
        playBorder.GetComponent<RectTransform>().Find("Text").gameObject.SetActive(true);
        creditsBorder.enabled = false;
        creditsBorder.GetComponent<RectTransform>().Find("Image").gameObject.SetActive(false);
        Debug.Log("HighlightPlayButton");
    }

    public void HighlightCreditButton()
    {
        playBorder.enabled = false;
        playBorder.GetComponent<RectTransform>().Find("Image").gameObject.SetActive(false);
        playBorder.GetComponent<RectTransform>().Find("Text").gameObject.SetActive(false);
        creditsBorder.enabled = true;
        creditsBorder.GetComponent<RectTransform>().Find("Image").gameObject.SetActive(true);
        Debug.Log("HighlightCreditButton");
    }

    public void SelectButton(string button)
    {
        if (button == "Play")
        {
            WhiteOut();
        }
        else if (button == "Credit")
        {
            creditsPanel.SetActive(true);
            scrollPanel.isPlaying = true;
        }
        else if (button == "Quit")
        {
            Application.Quit();
        }
    }


}