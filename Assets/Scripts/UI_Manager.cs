using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class UI_Manager : MonoBehaviour {

	public GameManager gm;

	private List<GameObject> playerList;
	public List<GameObject> playerUIs = new List<GameObject>();
	public List<GameObject> abilityUIs = new List<GameObject>();

	public GameObject winUI;
	public Text survivorText;
	public Text damageText;
	public Text treasureText;

	public GameObject readyText;
	public GameObject goText;
	public float readyTextDuration;
	public float goTextDuration;

	public Animator whiteCoverAnim;
	public Animator tutorialAnim;

	private bool inWinUI;
	private string[] colorsForPlayerNum = {"<color=red>RED</color>", "<color=blue>BLUE</color>"
		, "<color=green>GREEN/colors", "<color=yellow>YELLOW</color>"};
	private GameObject[] cooldownUIs;
	
	// Use this for initialization
	public void Start () {
		playerList = gm.playerList;
		cooldownUIs = GameObject.FindGameObjectsWithTag ("Cooldown");
		SetupAbilityPanels ();
		SetupAbilityIcons ();
	}

	public void Update(){
		if(inWinUI){
			if(ControllerInputWrapper.GetButton(ControllerInputWrapper.Buttons.A, 1) ||
			   ControllerInputWrapper.GetButton(ControllerInputWrapper.Buttons.A, 2) ||
			   ControllerInputWrapper.GetButton(ControllerInputWrapper.Buttons.A, 3) ||
			   ControllerInputWrapper.GetButton(ControllerInputWrapper.Buttons.A, 4)){
				Application.LoadLevel("Level3");
			}
			else if(ControllerInputWrapper.GetButton(ControllerInputWrapper.Buttons.B, 1) ||
			        ControllerInputWrapper.GetButton(ControllerInputWrapper.Buttons.B, 2) ||
			        ControllerInputWrapper.GetButton(ControllerInputWrapper.Buttons.B, 3) ||
			        ControllerInputWrapper.GetButton(ControllerInputWrapper.Buttons.B, 4)){
				Application.LoadLevel("Main_Menu");
			}
		}
	}
	
	public void HideTutorial(){
		whiteCoverAnim.SetTrigger ("transparencyInc");
		tutorialAnim.SetTrigger ("transparencyInc");
	}

	public void ReadyTextEnable(){
		if(readyText){
			readyText.SetActive (true);
		}
		Destroy (readyText, readyTextDuration);
		Invoke ("GoTextEnable", readyTextDuration);
	}

	public void GoTextEnable(){
		if(goText){
			goText.SetActive (true);
		}
		Destroy (goText, goTextDuration);
		Invoke ("UIReady", goTextDuration);
	}

	public void UIReady(){
		CancelInvoke ();
		gm.StartGame ();
	}

	void SetupAbilityPanels(){
		//set ability UIs
		for(int j = 0; j < playerList.Count; j++){
			playerUIs[j].SetActive(true);
		}

	}

	void SetupAbilityIcons(){
		//Debug.Log ("SetupAbilityIcons");
		for (int i = 0; i < playerList.Count; i++){
			//Get sprites from playerAttack

			List<Sprite> sprites = new List<Sprite>();
			for (int j = 0; j < 3; j++){
				sprites.Add(playerList[i].GetComponent<PlayerAttack>().abilities[j].icon);
			}
			//Debug.Log(sprites.Count);
			//Assign sprites
			//int playerNum = playerList[i].GetComponent<PlayerAttack>().joystickNum-1;

			for (int k = 0; k < 3; k++){

				Image image = abilityUIs[i].GetComponent<RectTransform>().Find("Ability_" + (k+1))
					.GetComponent<Image>();
				image.sprite = sprites[k];
			}

		}
	}


	void OnGUI(){
		//update cooldown UI
		//i is player number; j is ability number.
		if(gm.GameInProgress){
			for (int i = 0; i < playerList.Count; i++) {
				for (int j = 0; j < 3; j++){
					int playerNum = playerList[i].GetComponent<PlayerAttack>().playerNum;
					
					bool abilityReady = playerList[i].GetComponent<PlayerAttack>().abilities[j].abilityReady;
					
					GameObject cooldownUI = abilityUIs[playerNum-1].GetComponent<RectTransform>().Find("Ability_" + (j+1) 
					                                                                                   + "/Cooldown").gameObject;
					cooldownUI.SetActive(!abilityReady);
					if(!abilityReady){
						Text text = cooldownUI.GetComponent<RectTransform>().Find("Text").GetComponent<Text>();
						text.text = playerList[i].GetComponent<PlayerAttack>().abilities[j].timeUntilReset.ToString();
					}
				}
			}
		}
	}

	public void SetPlayerDeathByNum(int playerNum){
		playerUIs [playerNum-1].GetComponent<RectTransform>().Find ("Avatar").GetComponent<AnimateGif>().start = true;
		//Debug.Break();
	}

	public void ShowWinScreen(int winPlayerNum, int mostDamagePlayerNum, int chestHunterNum){
		survivorText.text = "Survivor: " + colorsForPlayerNum[winPlayerNum-1] + " Player!";

		if(mostDamagePlayerNum == 0){
			damageText.text = "WTH! You all did 0 damage!";
		}
		else{
			damageText.text = "Damage Dealer: "+  colorsForPlayerNum[mostDamagePlayerNum-1] + " Player!";
		}

		if(chestHunterNum == 0){
			treasureText.text = "Well, what a bunch of fighting nerds!";
		}
		else{
			treasureText.text = "Treasure Hunter: " + colorsForPlayerNum[chestHunterNum-1] + " Player!";
		}
		inWinUI = true;
		winUI.SetActive (true);
	}

	public void Rematch(){
		Application.LoadLevel ("Level3");
	}

	public void BackToMainMenu(){
		Application.LoadLevel ("Main_Menu");
	}

}
