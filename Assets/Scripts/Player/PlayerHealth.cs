﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

	public bool onFire;
	public float onFireTime;
	public GameObject onFireEffect;

	GameObject[] allgrounds;
    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    //PlayerShooting playerShooting;
    bool isDead;
    bool damaged;


    void Awake ()
    {
		allgrounds = GameObject.FindGameObjectsWithTag("Island");
        anim = GetComponent <Animator> ();
        playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent <PlayerMovement> ();
        //playerShooting = GetComponentInChildren <PlayerShooting> ();
        currentHealth = startingHealth;
    }


    void Update ()
    {
        if(damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;

		if(onFire && !onFireEffect.activeInHierarchy){
			CauseOnFire();
		}
    }

	public void CauseOnFire(){
		Debug.Log ("On Fire");
		onFireEffect.SetActive (true);
		Invoke ("CeaseFire", onFireTime);
	}
	
	public void CeaseFire(){
		Debug.Log ("CeaseFire");
		CancelInvoke ();
		onFireEffect.SetActive (false);
		onFire = false;
	}


    public void TakeDamage (int amount, bool burn)
    {
        
        currentHealth -= amount;

        healthSlider.value = currentHealth;

		//Audio
		if(amount > 0){
			damaged = true;
			playerAudio.Play ();
		}
		else{

		}
        

		if(burn){
			burnGround (amount);
			onFire = true;
		}

        if(currentHealth <= 0 && !isDead)
        {
            Death ();
        }
    }



	public void burnGround(int amount){

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
