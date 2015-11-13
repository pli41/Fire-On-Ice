using UnityEngine;
using System.Collections;

public class ScrollPanel : MonoBehaviour {

	public float scrollSpeed;

	public Vector3 startPos;
	public float endY;
	public bool isPlaying = true;
	public bool interrupted;
	public GameObject creditsPanel;

	RectTransform rectTran;

	// Use this for initialization
	void Start () {
		interrupted = false;
		rectTran = GetComponent<RectTransform> ();
		startPos = rectTran.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(rectTran.position.y < endY && isPlaying){
			rectTran.Translate(0, scrollSpeed * Time.deltaTime, 0);

//				position.Set(rectTran.position.x, rectTran.position.y+scrollSpeed * Time.deltaTime
//			                      , rectTran.position.z);
			//Debug.Log("Scrolling");
		}
		else{
			isPlaying = false;
		}

		if(ControllerInputWrapper.GetButton(ControllerInputWrapper.Buttons.B, 1, true)||
		   ControllerInputWrapper.GetButton(ControllerInputWrapper.Buttons.B, 2, true)||
		   ControllerInputWrapper.GetButton(ControllerInputWrapper.Buttons.B, 3, true)||
		   ControllerInputWrapper.GetButton(ControllerInputWrapper.Buttons.B, 4, true)){
			isPlaying = false;
			interrupted = true;
			creditsPanel.SetActive(false);
			rectTran.position = startPos;
		}
	}
}
