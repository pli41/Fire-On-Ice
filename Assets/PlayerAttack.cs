using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

	public GameObject currentMagic;

	private Fireball fireball;
	private float damage;
	private float knockDist;
	private float coolDown;


	private float timer;
	
	void Awake ()
	{
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

	}
	

	
	
	void Shoot ()
	{
		timer = 0f;

		currentMagic.transform.position = transform.position + transform.forward + new Vector3(0, 1f, 0);
		currentMagic.transform.rotation = transform.rotation;
		Instantiate (currentMagic);
	}
}
