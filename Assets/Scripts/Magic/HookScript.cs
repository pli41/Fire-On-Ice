using UnityEngine;
using System.Collections;

public class HookScript : MonoBehaviour {
    public Transform owner;
    public Hook hookAbility;
    public float maxLength;

    LineRenderer lineRender;
    Transform hookedPlayer;
    float currentLength;
    bool isFiring;
    bool isRetracting;
    bool isHooked;
	bool isHookedObstacle;

    void Awake()
    {
        lineRender = GetComponent<LineRenderer>();
        isFiring = true;
    }

    void Update()
    {
        updatePlayerMovement();
        lineRender.SetPosition(0, owner.position);
        lineRender.SetPosition(1, this.transform.position);
        //owner.GetComponent<PlayerMovement>().canMove = false;
        checkRetract();
		if (isHookedObstacle) {
			obstacleLogic ();
		}
        if (isFiring)
        {
            fireLogic();
        }
        if (isRetracting)
        {
            retractLogic();
        }
        if (isHooked)
        {
            hookedLogic();
        }
        checkDestroy();
        transform.rotation = owner.rotation;
    }

	void obstacleLogic() {
		owner.GetComponent<PlayerMovement> ().canTurn = false;
		currentLength -= Time.deltaTime * hookAbility.hookShootSpeed;
		owner.position = this.transform.position + -owner.forward.normalized * currentLength;
	}

    void hookedLogic()
    {
		owner.GetComponent<PlayerMovement> ().canTurn = false;
        currentLength -= Time.deltaTime * hookAbility.hookRetractSpeed;
        transform.position = owner.position + owner.forward.normalized * currentLength;
        hookedPlayer.position = transform.position;

    }

    void fireLogic()
    {
		owner.GetComponent<PlayerMovement> ().canTurn = false;
        currentLength += Time.deltaTime * hookAbility.hookShootSpeed;
        transform.position = owner.position + owner.forward.normalized * currentLength;
    }

    void retractLogic()
    {
		owner.GetComponent<PlayerMovement> ().canTurn = false;
        GetComponent<Collider>().enabled = false;
        currentLength -= Time.deltaTime * hookAbility.hookShootSpeed;
        transform.position = owner.position + owner.forward.normalized * currentLength;

    }

    void updatePlayerMovement()
    {
        PlayerMovement pMovement = owner.GetComponent<PlayerMovement>();
        if (isFiring)
        {
            pMovement.canMove = false;
            pMovement.canTurn = false;
        }
        else
        {
            pMovement.canMove = true;
            pMovement.canTurn = true;
        }
    }

	public void releaseObject() {
		if (isHooked || isFiring) {
			isHooked = false;
			isFiring = false;
			isRetracting = true;
		}
	}

    void checkRetract()
    {
        Vector3 delta = this.transform.position - owner.transform.position;
        if (delta.magnitude > maxLength)
        {
            isRetracting = true;
            isFiring = false;
            isHooked = false;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if ((collider.tag == "Player" && owner.GetComponent<PlayerMovement>().joystickNum != collider.GetComponent<PlayerMovement>().joystickNum))
        {
            hookedPlayer = collider.transform;
            isHooked = true;
            isFiring = false;
            isRetracting = false;
        }
		if (collider.tag == "Obstacle") {
			hookedPlayer = collider.transform;
			isHookedObstacle = true;
			isFiring = false;
			isRetracting = false;
			isHooked = false;
		}
    }

    void checkDestroy()
    {
        if (isRetracting || isHooked || isHookedObstacle)
        {
            if (currentLength < 2)
            {
				owner.GetComponent<PlayerMovement> ().canTurn = true;
                hookAbility.abilityReady = true;
                Destroy(this.gameObject);

            }
        }
    }
}
