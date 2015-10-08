using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FireMeteor_Object : MonoBehaviour {

    public Ability ability;

    public GameObject[] players;
    private List<GameObject> playerList = new List<GameObject>();

    public float forceMagnitude;
    public float explosionRadius;

    public float explosionForceMag;
    public float acc;
    public float maxSpeed;

    public float damage = 10f;
    public float verticalSpeed = 5f;
    public float horizontalSpeed = 5f;

    private Rigidbody rigid;
    private float timer;
    private MeshRenderer rend;
    private Transform explosion;
    private Transform fire;
    private bool exploded;

    // Use this for initialization
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        SetupPlayerList();
        fire = transform.Find("Fire");
        explosion = transform.Find("Explosion");
        rend = GetComponent<MeshRenderer>();
        rigid = GetComponent<Rigidbody>();
        forceMagnitude = horizontalSpeed;
        exploded = false;
    }

    void SetupPlayerList()
    {
        foreach (GameObject p in players)
        {
            playerList.Add(p);
        }
    }

    void Update()
    {
        if (!exploded)
        {
            Vector3 deltaSpeed = (transform.forward - transform.up) * acc * Time.deltaTime;

            if (rigid.velocity.magnitude < maxSpeed)
            {
                rigid.velocity += deltaSpeed;
            }
        }
    }
    void OnCollisionEnter(Collision col)
    {
        Explode();
        exploded = true;
        
    }

    void Explode()
    {
        Debug.Log("EXPLODE");
        rigid.velocity = Vector3.zero;
        rend.enabled = false;
        explosion.gameObject.SetActive(true);
        fire.gameObject.SetActive(false);
        CheckPlayers();
        Invoke("DestroyRock", 4f);
    }
    void CheckPlayers()
    {
        foreach (GameObject p in players)
        {
            if (Vector3.Distance(p.transform.position, transform.position) < explosionRadius)
            {
                Debug.Log("Explosion in range");
                p.GetComponent<Rigidbody>().AddExplosionForce(forceMagnitude, transform.position, explosionRadius);
                p.GetComponent<PlayerHealth>().TakeDamage((int)(damage / Vector3.Distance(p.transform.position, transform.position)), true);
            }
        }
    }

    void DestroyMeteor()
    {
        Destroy(gameObject);
    }

}
