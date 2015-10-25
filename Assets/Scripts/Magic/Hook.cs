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
    
    void Update()
    {
		timeUntilReset = (int)(cooldown - cdTimer + 1f);
        udpateHookAimLength();
        lineRender.SetPosition(0, owner.transform.position + Vector3.up * 1);
        lineRender.SetPosition(1, owner.transform.position + owner.transform.forward.normalized * hookAimLength + Vector3.up * 1);

    }
    
    void Start()
    {
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

        isCasting = true;
    }

    public override void EndCast()
    {
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

}
