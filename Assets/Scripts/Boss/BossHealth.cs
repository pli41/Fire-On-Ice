using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour {

	public Material oldMaterial;
	public Material damagedMaterial;

	public int startingHealth;
	public int currentHealth;

	public Slider healthSlider;

	Collider col;
	Renderer rend;
	bool damaged;
	bool isDead;

	// Use this for initialization
	void Start () {
		currentHealth = startingHealth;
		rend = GetComponent<Renderer> ();
		oldMaterial = rend.material;

	}
	
	// Update is called once per frame
	void Update () {
		if(damaged){
			damaged = false;
			rend.material = oldMaterial;
		}
	}

	public void TakeDamage (int amount)
	{
		Debug.Log ("Boss taking damage");

		damaged = true;

		rend.material = damagedMaterial;

		currentHealth -= amount;
		
		healthSlider.value = currentHealth/10;

		if(currentHealth <= 0 && !isDead)
		{
			Death ();
		}
	}

	void Death(){
		Debug.Log ("Boss died");
		col.enabled = false;
		isDead = true;
		gameObject.SetActive (false);
	}
}
