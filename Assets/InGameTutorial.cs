using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InGameTutorial : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		GetComponent<RectTransform> ().sizeDelta = new Vector2(Screen.width, Screen.height);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
