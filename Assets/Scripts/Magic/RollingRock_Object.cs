using UnityEngine;
using System.Collections;

public class RollingRock_Object : MonoBehaviour {

	public Ability ability;
	public int damage;
	public float forceMagnitude;
	public float forceRadius;

	public float maxSpeed; 
	public float rollingTime;
	public float acc;

	public float explosionForceMag;

	private Rigidbody rigid;
	private float timer;

	// Use this for initialization
	void Start () {
		rigid = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 deltaSpeed = transform.forward * acc * Time.deltaTime;

		if(rigid.velocity.magnitude < maxSpeed){
			rigid.velocity += deltaSpeed;
		}

		//Watch for explosion
		if(timer < rollingTime){
			timer += Time.deltaTime;
		}
		else{
			Explode();
		}

	}

	void Explode(){
		Debug.Log ("EXPLODE");
		rigid.velocity = Vector3.zero;
		Invoke ("DestroyRock", 2f);
	}

	void DestroyRock(){
		Destroy (gameObject);
	}
}
