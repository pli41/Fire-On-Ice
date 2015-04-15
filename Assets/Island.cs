using UnityEngine;
using System.Collections;

public class Island : MonoBehaviour {

	public float shrinkSize = 5f;
	public float timeBetShrink = 5f;

	private float timer;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(timer >= timeBetShrink){
			timer = 0f;
			Shrink();
		}
		else{
			timer += Time.deltaTime;
		}
	}

	void Shrink(){
		Vector3 curScale = transform.localScale;
		curScale.x -= shrinkSize;
		curScale.z -= shrinkSize;
		transform.localScale = curScale;
	}
}
