using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

	public GameObject currentMagic;
	public bool onFire;
	public GameObject ps;
	public float ceaseFireTime = 5f;

	private Fireball fireball;
	private float damage;
	private float knockDist;
	private float coolDown;

	private float fireTimer;
	private float timer;
	
	void Awake ()
	{
		onFire = false;
		if(currentMagic.tag == "Fireball"){
			fireball = currentMagic.GetComponent<Fireball>();
			damage = fireball.damage;
			knockDist = fireball.knockDist;
			coolDown = fireball.coolDown;
		}
	}
	
	
	void FixedUpdate ()
	{
		timer += Time.deltaTime;


		if(Input.GetAxisRaw ("PS4_R2") >0 && timer >= coolDown)
		{
			Shoot ();
		}


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

	}
	
	void ceaseFire(){
		onFire = false;
	}
	
	
	void Shoot ()
	{
		timer = 0f;
		onFire = true;
		currentMagic.transform.position = transform.position + transform.forward * 1.5f + new Vector3(0, 1f, 0);
		currentMagic.transform.rotation = transform.rotation;
		Instantiate (currentMagic);
	}
}
