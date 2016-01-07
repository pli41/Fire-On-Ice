using UnityEngine;
using System.Collections;

public class Boundary : MonoBehaviour {

	void OnTriggerEnter(Collider col){
		Destroy (col.gameObject);
	}
}
