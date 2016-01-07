using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AnimateGif : MonoBehaviour {

	public Sprite[] frames;
	public int framesPerSecond;
	public bool looping;
	public bool start;


	private int frameNum;
	private bool playedOnce;
	private float startedDuration;
	private bool setup;
	// Use this for initialization
	void Start () {
		frameNum = 0;
		playedOnce = false;
		setup = false;
	}

	
	// Update is called once per frame
	void Update () {

		if(start){
			if(!setup){
				setup = true;
				startedDuration = Time.time;
			}
			int index = (int)((Time.time-startedDuration) * framesPerSecond); 
			if(!(playedOnce && !looping)){
				frameNum = index % frames.Length;
			}

			if(!looping){
				if(frameNum == frames.Length-1){
					frameNum = frames.Length-1;
					playedOnce = true;
				}
			}

			GetComponent<Image>().sprite = frames[frameNum];
		}
	}
}
