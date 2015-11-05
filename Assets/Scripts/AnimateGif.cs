using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AnimateGif : MonoBehaviour {

	public Sprite[] frames;
	public int framesPerSecond;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		int index = (int)(Time.time * framesPerSecond); 
		index = index % frames.Length; 
		GetComponent<Image>().sprite = frames[index];
	}
}
