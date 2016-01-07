using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

	public GameManager gm;
	public Transform startPoint;
	public Transform endPoint;
	public bool startMovement;
	public float speed;
	public bool endMovement;

	private float distance;
	private float timer;
	// Use this for initialization
	void Start () {
		distance = (endPoint.position - startPoint.position).magnitude;
	}
	
	// Update is called once per frame
	void Update () {
		if(startMovement){
			timer+= Time.deltaTime;
			gameObject.transform.position = Vector3.Lerp (startPoint.position, endPoint.position, timer*speed/distance);
			//Debug.Log(gameObject.transform.position);
			//Debug.Log("Moving");
		}

		if(Vector3.Distance(transform.position, endPoint.position) < 1f){
			endMovement = true;
			startMovement = false;
			//Debug.Log("Camera Set");
		}

	}
}
