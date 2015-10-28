using UnityEngine;
using System.Collections;

public class FireSword_Object : MonoBehaviour {

	public bool active;
	public float forceMagnitude;
	public int damage;


	public float slashTime;

	public Ability ability;

	private bool slashed;


	// Use this for initialization
	void Start () {
		slashed = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Slash(){
		Debug.Log ("Slash");
		slashed = false;
		GetComponent<Animator> ().SetTrigger ("slash");
		//Invoke ("ResetSlash", slashTime);
	}

	void OnTriggerEnter(Collider col){
		if(!slashed){
			if (col.gameObject.tag == "Player") {
				PlayerHealth healthP = col.GetComponent<PlayerHealth> ();
				healthP.TakeDamage (damage, true, ability.owner.GetComponent<PlayerAttack>().playerNum);
				Rigidbody rigid = col.attachedRigidbody;
				Vector3 direction = col.transform.position - ability.owner.transform.position;
				rigid.AddForce(direction.normalized * forceMagnitude, ForceMode.Impulse);
				slashed = true;
			}
			else if(col.gameObject.tag == "Boss"){
				BossHealth health = col.GetComponent<BossHealth> ();
				health.TakeDamage (damage);
				slashed = true;
			}
		}



	}

	void ResetSlash(){
		slashed = false;
	}
}
