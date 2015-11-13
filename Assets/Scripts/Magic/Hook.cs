using UnityEngine;
using System.Collections;

public class Hook : Ability {
    public float hookShootSpeed;
    public float hookRetractSpeed;
    public float hookAimSpeed;
    public float maxHookLength;
	

    private float hookAimLength;
    private float hookLength;
    private bool isCasting;
    private LineRenderer lineRender;
	private HookScript hook;
	private Animation anim;

    void Update()
    {
		timeUntilReset = (int)(cooldown - cdTimer + 1f);
        udpateHookAimLength();
        lineRender.SetPosition(0, owner.transform.position + Vector3.up * 1);
        lineRender.SetPosition(1, owner.transform.position + owner.transform.forward.normalized * hookAimLength + Vector3.up * 1);

    }
    
    void Start()
    {
		anim = owner.GetComponent<Animation> ();
        abilityReady = true;
        lineRender = GetComponent<LineRenderer>();
        lineRender.SetPosition(0, transform.position);
        lineRender.SetPosition(1, transform.position);
    }

    public override void Cast()
    {
        //print("I am here");
		if (!abilityReady) {
			hook.releaseObject();
		}
		DisableMove ();
		anim.CrossFade ("Cast", 0.1f);
        isCasting = true;
    }

    public override void EndCast()
    {
		anim.CrossFade ("Attack1", 0.1f);
		Invoke ("EnableMove", anim.GetClip("Attack1").length);
		Invoke ("Shoot", anim.GetClip("Attack1").length/2);
    }

	public void Shoot(){
		isCasting = false;
		abilityReady = false;
		GameObject obj = (GameObject)Instantiate(ability_object, owner.transform.position + Vector3.up * 2, new Quaternion());
		hook = obj.GetComponent<HookScript>();
		hook.owner = this.owner.transform;
		hook.hookAbility = this;
		hook.maxLength = hookAimLength;
	}

    public override void SetupAbility()
    {
        
    }

    public override void SetupObj()
    {

    }

    void udpateHookAimLength()
    {
        if (isCasting)
        {
            hookAimLength += Time.deltaTime * hookAimSpeed;
            if (hookAimLength > maxHookLength)
            {
                hookAimLength = maxHookLength;
            }
        }
        else
        {
            hookAimLength = 0;
        }
    }

	public void DisableMove(){
		owner.GetComponent<PlayerMovement> ().canMove = false;
	}
	
	public void EnableMove(){
		owner.GetComponent<PlayerMovement> ().canMove = true;
	}
}
