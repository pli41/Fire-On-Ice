using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	//Following target
	public Transform target;
	//The angle between horizontal plane and the camera's pointing direction
	public float verticalAngle;
	//The distance between the camera and the character in the forward direction of the character.
	public float camera_OffsetForward;
	//The distance between the camera and the character in the up direction of the character.
	public float camera_OffsetUpward;
	//The rotating speed of the camera
	public float rotateSpeed;
	//The translating speed of the camera
	public float translateSpeed;
	
	//private Camera cam;
	// Use this for initialization
	void Start () {
		//cam = gameObject.GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void Update () {
		CamTranslate ();
		CamRotate ();
	}
	
	void CamRotate(){
		Vector3 targetDir = FindTargetCameraDirection (target);
		targetDir.Set (targetDir.x, 0f+verticalAngle, targetDir.z);
		float step = rotateSpeed * Time.deltaTime;
		Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
		transform.rotation = Quaternion.LookRotation(newDir);
	}
	
	void CamTranslate(){
		float step = translateSpeed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(FindTargetCameraPoint(target), transform.position, step);
	}
	
	
	Vector3 FindTargetCameraPoint(Transform player){
		//Vector3 playerPos = player.position;
		Vector3 targetPos = player.position - player.forward * camera_OffsetForward + player.up * camera_OffsetUpward;
		return targetPos;
	}
	
	Vector3 FindTargetCameraDirection(Transform player){
		Vector3 direction = player.position - transform.position;
		return direction;
	}
	
}