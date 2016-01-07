using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InGameHealthUI : MonoBehaviour {

	public PlayerHealth playerHealth;
	public float showDuration;
	public float XOffset;
	public float YOffest;

	private float currentHealth;
	private RectTransform healthbar;
	private float healthToScaleRatio = 100f;
	float newScaleX;
	// Use this for initialization
	void Start () {
		healthbar = transform.Find ("filler").GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
		currentHealth = playerHealth.currentHealth;
		UpdateHealthBar ();
	}

	void UpdateHealthBar(){
		//update health bar scale
		newScaleX = currentHealth / healthToScaleRatio;
		Vector3 newScale = healthbar.localScale;
		if(newScaleX < 0f){
			newScale.x = 0f;
		}
		else{
			newScale.x = newScaleX;
		}

		healthbar.localScale = newScale;
		//update health bar position
		Vector3 wantedPos = Camera.main.WorldToScreenPoint (playerHealth.transform.position);
		wantedPos.x += XOffset;
		wantedPos.y += YOffest;
		transform.position = wantedPos;
	}

	public void ShowUI() {
		Debug.Log ("Show Health");
		gameObject.SetActive (true);
		CancelInvoke ();
		Invoke ("HideUI", showDuration);
	}

	void HideUI(){
		Debug.Log ("Hide Health");
		gameObject.SetActive (false);
	}
}
