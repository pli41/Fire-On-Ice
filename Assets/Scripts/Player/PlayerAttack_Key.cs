using UnityEngine;

public class PlayerAttack_Key : MonoBehaviour
{
	
	public GameObject currentMagic;
	public bool onFire;
	public GameObject ps;
	public float ceaseFireTime = 5f;
	public GameObject enchantEffect;
	public float coolDown = 2f;
	
	private Fireball fireball;
	private float damage;
	private float knockDist;
	
	private float chargeTime;
	private float fireTimer;
	private float timer;
	
	private bool inCharge;
	
	void Awake ()
	{
		inCharge = false;
		onFire = false;
		if(currentMagic.tag == "Fireball"){
			fireball = currentMagic.GetComponent<Fireball>();
			damage = fireball.damage;
			coolDown = fireball.coolDown;
		}
	}
	
	
	void FixedUpdate ()
	{
		timer += Time.deltaTime;
		
		
		if(Input.GetMouseButtonDown(0) == true && timer >= coolDown && !inCharge)
		{
			Debug.Log("Start shooting");
			inCharge = true;
		}
		
		if(inCharge){
			Charge();
		}
		
		//on fire effect
		if(fireTimer > ceaseFireTime){
			ceaseFire();
			fireTimer = 0;
		}
		else{
			if(onFire){
				fireTimer += Time.deltaTime;
			}
		}
		
		if(onFire){
			ps.SetActive(true);
		}
		else{
			ps.SetActive(false);
		}
		
		if(inCharge){
			enchantEffect.SetActive (true);
		}
		else{
			enchantEffect.SetActive (false);
		}	
		
	}
	
	void ceaseFire(){
		onFire = false;
	}
	
	void Charge(){

		if(chargeTime <= 3f){
			chargeTime += Time.deltaTime;
		}
		else{
			Debug.Log("Fully charged");
		}
		
		if(Input.GetMouseButton(0) == false){
			Shoot(chargeTime);
			chargeTime = 0;
		}
		
	}
	
	void Shoot (float chargeTime)
	{
		float size = chargeTime / 3f * 10f;
		Debug.Log ("Shoot");
		timer = 0f;
		inCharge = false;
		onFire = true;
		fireball.size = size;
		currentMagic.transform.position = transform.position + transform.forward * 1.5f + new Vector3(0, 1f, 0);
		currentMagic.transform.rotation = transform.rotation;
		Instantiate (currentMagic);
		coolDown = 0.3f * size;
	}
}
