using UnityEngine;
using System.Collections;

public class FireSword_Object : MonoBehaviour {

	public bool active;
	public float forceMagnitude;
	public int damage;

	public float slashDuration;

	public float slashTime;

	public Ability ability;
	public AudioClip slash;


	private bool slashed;
	private AudioSource audioS;
	private float timer;
	public bool slashReady;

	// Use this for initialization
	void Start () {
		slashed = true;
		audioS = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(!slashReady){
			if(timer < slashTime){
				timer += Time.deltaTime;
			}
			else{
				slashReady = true;
			}
		}
		else{
			timer = 0f;
		}
	}

	public void Slash(){
		//Debug.Log ("Slash");
		if(slashReady){
			slashReady = false;
			slashed = false;
			GetComponent<Animator> ().SetTrigger ("slash");
			if(audioS.clip.name == "swordSummoned"){
				if(!audioS.isPlaying){
					audioS.clip = slash;
				}
			}
			else if(audioS.clip.name == slash.name){
				if(!audioS.isPlaying && slashReady){
					audioS.Stop();
					audioS.Play();
				}
			}

		}
		//Invoke ("ResetSlash", slashTime);
	}

	void OnTriggerEnter(Collider col){
		Debug.Log ("Sword Collides");
		if(!slashed){
			slashed = true;
			if (col.gameObject.tag == "Player") {
				PlayerHealth healthP = col.GetComponent<PlayerHealth> ();
				healthP.TakeDamage (damage, true, ability.owner.GetComponent<PlayerAttack>().playerNum);
				Rigidbody rigid = col.attachedRigidbody;
				Vector3 direction = col.transform.position - ability.owner.transform.position;
				rigid.AddForce(direction.normalized * forceMagnitude, ForceMode.Impulse);
			}
			else if(col.gameObject.tag == "Boss"){
				BossHealth health = col.GetComponent<BossHealth> ();
				health.TakeDamage (damage);
			}
		}
	}

	void ResetSlash(){
		slashed = false;
	}


}
