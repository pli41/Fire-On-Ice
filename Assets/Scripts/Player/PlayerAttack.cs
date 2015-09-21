using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
	public string playerString;
	public GameObject currentMagic;
	public bool onFire;
	public GameObject ps;
	public float ceaseFireTime = 5f;
	public GameObject enchantEffect;
	public float coolDown = 2f;
	public Transform fireballPoint;
	
	private float knockDist;
	private Fireball fireball;
	private float chargeTime;
	private float fireTimer;
	private float timer;
	private bool inCharge;



	public Ability[] abilities = new Ability[3];






	void Awake ()
	{
		inCharge = false;
		onFire = false;

		if(currentMagic.tag == "Fireball"){
			fireball = currentMagic.GetComponent<Fireball>();
			coolDown = fireball.coolDown;
		}

	}
	
	
	void FixedUpdate ()
	{



		timer += Time.deltaTime;
		if(Input.GetAxisRaw ("PS4_R2_" + playerString) > 0 && timer >= coolDown && !inCharge)
		{
			inCharge = true;

			abilities[0].cast();
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
		Debug.Log ("Charging");
		
		
		if(chargeTime <= 3f){
			chargeTime += Time.deltaTime;
		}
		else{
			Debug.Log("Fully charged");
		}
		
		if(Input.GetAxisRaw ("PS4_R2_" + playerString) <= 0){
			Shoot(chargeTime);
			chargeTime = 0;
			timer = 0f;
		}
		
	}
	
	void Shoot (float chargeTime)
	{
		float size = chargeTime;
		Debug.Log ("Shoot");
		inCharge = false;
		onFire = true;
		fireball.size = size;


		currentMagic.transform.position = fireballPoint.position;
		//currentMagic.transform.position = transform.position + transform.forward * 1.5f + new Vector3(0, 1f, 0);
		currentMagic.transform.rotation = transform.rotation;
		Instantiate (currentMagic);
		coolDown = 0.3f * size;

		//Debug.Break ();
	}
}
