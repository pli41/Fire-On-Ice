using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerHealth : MonoBehaviour
{
	public float startingHealth = 100;
	public float currentHealth;
	public Slider healthSlider;
	public Image damageImage;
	public AudioClip deathClip;
	public float flashSpeed = 5f;
	public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
	public GameObject deathlight;



	//1 means zero reduction, 0.5 means half reduction, 0 means invincible
	public float damageReduction;
	
	public bool onFire;
	public float onFireTime;
	public GameObject onFireEffect;
	
	public TextMesh mesh;
	
	public float damageSFXTime;
	private float damageSFXTimer;
	private bool damageSFXReady;

	GameObject canvas;
	InGameHealthUI inGameHealthUI;
	GameObject[] allgrounds;
	Animation anim;
	AudioSource playerAudio;
	PlayerMovement playerMovement;
	PlayerAttack playerAttack;
	//PlayerShooting playerShooting;
	bool isDead;
	bool damaged;
	GameObject gameManager;
	int joystickNum;
	Rigidbody rigid;
	AudioSource announcerAudio;
	public AudioClip[] damageClips;

	private GameManager gm;
	private UI_Manager uim;
	
	void Awake ()
	{
		onFire = false;
		announcerAudio = GetComponents<AudioSource> () [1];
		playerAttack = GetComponent<PlayerAttack> ();

		canvas = GameObject.Find ("HUDCanvas");
		//Debug.Log ("PlayerHealthUI_" + playerAttack.playerNum);

		inGameHealthUI = canvas.GetComponent<RectTransform> ().Find ("PlayerHealthUI_" + playerAttack.playerNum)
			.GetComponent<InGameHealthUI>();
		inGameHealthUI.playerHealth = this;
		rigid = GetComponent<Rigidbody> ();
		gm = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		uim = GameObject.Find ("GameManager").GetComponent<UI_Manager> ();
		joystickNum = playerAttack.joystickNum;
		damageReduction = 1;
		allgrounds = GameObject.FindGameObjectsWithTag("Island");
		anim = GetComponent <Animation> ();
		playerAudio = GetComponent <AudioSource> ();
		playerMovement = GetComponent <PlayerMovement> ();

		//playerShooting = GetComponentInChildren <PlayerShooting> ();
		currentHealth = startingHealth;
		SetupHealthUI ();
	}
	
	void SetupHealthUI(){
		gameManager = GameObject.FindGameObjectWithTag ("GameManager");
		healthSlider = gameManager.GetComponent<UI_Manager> ().playerUIs [joystickNum-1]
		.GetComponent<RectTransform> ().Find ("HealthUI_P" + joystickNum + "/HealthSlider").GetComponent<Slider>();
	}
	
	
	void Update ()
	{
		if (rigid.IsSleeping()) {
			rigid.WakeUp();
		}
		//        if(damaged)
		//        {
		//            damageImage.color = flashColour;
		//        }
		//        else
		//        {
		//            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		//        }
		
		damaged = false;
		
		if(onFire && !onFireEffect.activeInHierarchy){
			CauseOnFire();
		}

		HandleHurtSFX ();
		
		
	}
	
	public void HandleHurtSFX(){
		if(damageSFXTimer < damageSFXTime && damageSFXReady == false){
			damageSFXTimer += Time.deltaTime;
		}
		else{
			damageSFXReady = true;
			damageSFXTimer = 0f;
		}
	}
	
	public void CauseOnFire(){
		//Debug.Log ("On Fire in playerhealth" + " in player " + playerAttack.playerNum);
		onFireEffect.SetActive (true);
		Invoke ("CeaseFire", onFireTime);
	}
	
	public void CeaseFire(){
		CancelInvoke ();
		//Debug.Log ("CeaseFire in playerhealth" + " in player " + playerAttack.playerNum);
		onFireEffect.SetActive (false);
		onFire = false;
	}
	
	
	public void TakeDamage (float amount, bool burn, int sourcePlayerNum)
	{
		if (gm.GameInProgress) {
			float finalDamage;
			if(amount > 0){
				finalDamage = amount * damageReduction;
			}
			else{
				finalDamage = amount;
			}

			inGameHealthUI.ShowUI();
			currentHealth -= finalDamage;

			if(finalDamage > 20f && sourcePlayerNum > 0){
				Debug.Log("Big damage detected");
				PlayRandomAudio(damageClips, announcerAudio);
			}

			healthSlider.value = currentHealth;
			
			//handle damage record
			if(sourcePlayerNum > 0){
				gm.playerList[sourcePlayerNum-1].GetComponent<PlayerAttack>().damageDealt += finalDamage;
			}
			
			if (Mathf.Abs(finalDamage) > 0.01f)
			{
				if(finalDamage > 0){
					anim.CrossFade("TakeDamage2", 0.1f);
					mesh.text = "-" + (int)(finalDamage+1);
					mesh.color = Color.red;
				}
				else{
					mesh.text = "+" + (int)(-finalDamage);
					mesh.color = Color.green;
				}

				if(!(mesh.text.Equals("+0") && mesh.text.Equals ("-0"))){
					GameObject textLabel =	(GameObject)Instantiate(mesh.gameObject);
					textLabel.transform.parent = transform;
					textLabel.transform.localPosition.Set(0, 2, 0);
				}
			}
			
			
			
			//Audio
			if (finalDamage > 0f && damageSFXReady) {
				damaged = true;
				playerAudio.Play ();
				damageSFXReady = false;
			}
			
			
			if (burn) {
				//burnGround (amount);
				onFire = true;
			}
			
			if (currentHealth <= 0 && !isDead) {
				Death ();
			}
		}
	}
	
	
	
	
	public void burnGround(float amount){
		
		foreach(GameObject go in allgrounds){
			if(go){
				MeltingIsland island = go.GetComponent<MeltingIsland>();
				if(island.active){
					float range;
					
					if(amount < 30f){
						range = 2f;
					}
					else if(amount >= 30f && amount < 60f){
						range = 3f;
					}
					else{
						range = 5f;
					}
					if(Vector3.Distance(transform.position, go.transform.position) < range){
						island.meltByExplode(amount);
					}
				}
			}
		}
	}

	void PlayRandomAudio(AudioClip[] clips, AudioSource audioS){
		audioS.Stop ();
		int clipNum = Random.Range (0, clips.Length - 1);
		audioS.clip = clips [clipNum];
		Debug.Log ("Clip " + clipNum + " played");
		audioS.Play ();
	}
	
	void Death ()
	{
		isDead = true;
		uim.SetPlayerDeathByNum (playerAttack.playerNum);
		//playerShooting.DisableEffects ();
		
		//anim.SetTrigger ("Die");
		playerAudio.clip = deathClip;
		playerAudio.Play ();
		rigid.useGravity = false;
		GetComponent<Collider> ().enabled = false;
		playerAttack.disabled = true;
		playerMovement.disabled = true;
		anim.CrossFade ("Death", 0.25f);
		Instantiate (deathlight, transform.position, Quaternion.identity);
		gameObject.SetActive (false);
		//playerShooting.enabled = false;
	}
}
