using UnityEngine;
using System.Collections;

public class FireBomb_Object : MonoBehaviour {

	private bool initialized;
	private bool attached;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(!initialized){
			Initialize();

		}

		if(attached){

		}




		Debug.Log (GetComponent<Rigidbody> ().velocity);
	}

	void Explode(){

	}

	void Initialize(){
		Vector3 objVelocity = transform.forward * 1000f + Vector3.up * 200f;
		GetComponent<Rigidbody> ().AddForce (objVelocity);
		initialized = true;
	}


}
