using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

	public Transform startPoint;
	public Transform endPoint;
	private bool startMovement;
	public float speed;
	private float distance;
	private float timer;
	// Use this for initialization
	void Start () {
		distance = (endPoint.position - startPoint.position).magnitude;
		Debug.Log (distance);
	}
	
	// Update is called once per frame
	void Update () {
		if(startMovement){
			timer+= Time.deltaTime;
			gameObject.transform.position = Vector3.Lerp (startPoint.position, endPoint.position, timer*speed/distance);
			Debug.Log(gameObject.transform.position);
			Debug.Log("Moving");
		}

		if(Input.GetKeyDown(KeyCode.Space)){
			startMovement = true;
		}
	}
}
