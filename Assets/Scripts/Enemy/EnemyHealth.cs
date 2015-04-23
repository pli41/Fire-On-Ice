using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;


    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    BoxCollider boxCollider;
    bool isDead;
    bool isSinking;
	Rigidbody rigid;


    void Awake ()
    {
        anim = GetComponent <Animator> ();
        enemyAudio = GetComponent <AudioSource> ();
        hitParticles = GetComponentInChildren <ParticleSystem> ();
        boxCollider = GetComponent <BoxCollider> ();
		rigid = GetComponent<Rigidbody> ();
        currentHealth = startingHealth;
    }


    void Update ()
    {
        if(isSinking)
        {
            transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }

	public void TakeDamage (int amount)
	{
		
		//currentHealth -= amount;
		Vector3 scale = transform.localScale;
		scale.Set (transform.localScale.x - 0.2f, transform.localScale.y - 0.2f, transform.localScale.z -0.2f);
		transform.localScale = scale;


		rigid.mass *= 0.9f;
		if(currentHealth <= 0 && !isDead)
		{
			Death ();
		}
	}

	public void TakeDamage (int amount, Vector3 hitPoint, Transform source)
    {
        if(isDead)
            return;

        //enemyAudio.Play ();
        //currentHealth -= amount;
        
        hitParticles.transform.position = hitPoint;
        hitParticles.Play();

		transform.localScale.Set (transform.localScale.x -1f, transform.localScale.y -1f, transform.localScale.z -1f);
		rigid.mass *= 0.7f;

		Vector3 force = (transform.position - source.position);

		//rigid.AddForce (force.normalized * 2);
        if(currentHealth <= 0)
        {
            Death ();
        }
    }


    void Death ()
    {
        isDead = true;

        boxCollider.isTrigger = true;


        enemyAudio.clip = deathClip;
        //enemyAudio.Play ();
    }


    public void StartSinking ()
    {
        GetComponent <NavMeshAgent> ().enabled = false;
        GetComponent <Rigidbody> ().isKinematic = true;
        isSinking = true;
        //ScoreManager.score += scoreValue;
        Destroy (gameObject, 2f);
    }


}
