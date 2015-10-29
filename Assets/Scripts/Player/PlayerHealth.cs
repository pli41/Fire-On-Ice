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

	//1 means zero reduction, 0.5 means half reduction, 0 means invincible
	public float damageReduction;

	public bool onFire;
	public float onFireTime;
	public GameObject onFireEffect;

	public float damageSFXTime;
	private float damageSFXTimer;
	private bool damageSFXReady;

	GameObject[] allgrounds;
    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    //PlayerShooting playerShooting;
    bool isDead;
    bool damaged;
	GameObject gameManager;
	int joystickNum;
	Rigidbody rigid;

	private GameManager gm;

    void Awake ()
    {
		rigid = GetComponent<Rigidbody> ();
		gm = GameObject.Find ("GameManager").GetComponent<GameManager>();
		joystickNum = GetComponent<PlayerAttack> ().joystickNum;
		damageReduction = 1;
		allgrounds = GameObject.FindGameObjectsWithTag("Island");
        anim = GetComponent <Animator> ();
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
		//Debug.Log ("On Fire");
		onFireEffect.SetActive (true);
		Invoke ("CeaseFire", onFireTime);
	}
	
	public void CeaseFire(){
		//Debug.Log ("CeaseFire");
		CancelInvoke ();
		onFireEffect.SetActive (false);
		onFire = false;
	}


    public void TakeDamage (float amount, bool burn, int sourcePlayerNum)
    {



        if (gm.GameInProgress) {
			float finalDamage = amount * damageReduction;

			currentHealth -= finalDamage;
			
			healthSlider.value = currentHealth;

			//handle damage record
			gm.playerList[sourcePlayerNum-1].GetComponent<PlayerAttack>().damageDealt += finalDamage;

			//Audio
			if (amount > 0f && damageSFXReady) {
				damaged = true;
				playerAudio.Play ();
				damageSFXReady = false;
			}
			
			
			if (burn) {
				burnGround (amount);
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


    void Death ()
    {
        isDead = true;

        //playerShooting.DisableEffects ();

        anim.SetTrigger ("Die");

        playerAudio.clip = deathClip;
        playerAudio.Play ();

        playerMovement.enabled = false;

		gameObject.SetActive (false);
        //playerShooting.enabled = false;
    }
}
