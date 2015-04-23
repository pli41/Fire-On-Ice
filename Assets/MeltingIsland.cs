using UnityEngine;
using System.Collections;

public class MeltingIsland : MonoBehaviour {

	public float colorChange = 0.1f;
	public float scaleChange = 0.1f;
	public float meltTime = 2f;

	float timer;
	Material mat;

	// Use this for initialization
	void Start () {
		mat = GetComponent<MeshRenderer> ().material;
		timer = 0f;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay(Collider col){
		if(col.gameObject.tag == "Player"){
			//Debug.Log("Player detected");
			PlayerAttack PA = col.gameObject.GetComponent<PlayerAttack>();

			if(PA.onFire){
				meltTime = 0.6f;
				Debug.Log("Fast melting");
			}
			else{
				meltTime = 2f;
				Debug.Log("Melting");
			}

			if(timer > meltTime){
				timer = 0;
				Melt(PA.onFire);
			}
			else{
				timer += Time.deltaTime;
			}

		}
	}

	void Melt(bool onFire){
		//change color

		Color color = mat.color;
		color.g -= colorChange;
		color.r -= colorChange;
		mat.color = color;

		//change thickness
		Vector3 scale = transform.localScale;
		scale.y -= scaleChange;

		if(scale.y < 0){
			Destroy(gameObject);
		}
		transform.localScale = scale;
	}
}
