using UnityEngine;

public class PlayerAttack_Key : MonoBehaviour
{

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
	
	void Awake ()
	{
		inCharge = false;
		onFire = false;
		
		if(currentMagic.tag == "Fireball"){
			fireball = currentMagic.GetComponent<Fireball>();
			coolDown = fireball.cooldown;
		}
		
	}
	
	
	void FixedUpdate ()
	{
		timer += Time.deltaTime;
		
		
		if(Input.GetMouseButton(0) && timer >= coolDown && !inCharge)
		{
			inCharge = true;
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
}
