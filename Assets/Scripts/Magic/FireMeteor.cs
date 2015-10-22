using UnityEngine;
using System.Collections;

public class FireMeteor : Ability, CastDelay, CasterEffect, Cooldown
{

    public GameObject enchantEffect;
    public GameObject onFireEffect;
    public float onFireTime_max;
    public float cooldown;
    private float delayTimer;
    public bool delayBool = true;
    private bool delaying;
    public float castTime;
    private FireMeteor_Object fireMeteor_object;

    void Start()
    {
        handledEndCast = true;
        enchantEffect = owner.transform.Find("enchantEffect").gameObject;
        abilityReady = false;
        SetupCooldown();
    }
    public override void SetupAbility()
    {
        triggerOnce = true;
    }
    public override void Cast()
    {
        if (abilityReady)
        {
            CastDelayStart();
        }
        else
        {
            Debug.Log("AbilityReady_cast: " + abilityReady);
            Debug.Log("Ability not ready.");
        }
    }

    public override void EndCast()
    {
        Debug.Log("Endcast");
        if (abilityReady)
        {
            CancelInvoke();
            Shoot();
            CastDelayEnd();
        }
    }
    public void Shoot()
    {
        SetupObj();
        Instantiate(ability_object);
    }
    public void CastDelayStart()
    {
        if (delayBool)
        {
            if (!delaying)
            {
                Debug.Log("Endcast will be called");
                enchantEffect.SetActive(true);
                Invoke("EndCast", castTime);
                delaying = true;
            }
            else
            {
                owner.GetComponent<PlayerMovement>().disabled = true;
            }
        }
    }

    public void CastDelayEnd()
    {
        ResetCooldown();
        enchantEffect.SetActive(false);
        owner.GetComponent<PlayerMovement>().disabled = false;
        delaying = false;
    }

    public void SetupCooldown()
	{
        cdTimer = 0f;
    }

    public void ResetCooldown()
    {
        cdTimer = 0f;
        abilityReady = false;
    }

    void Update()
    {
        CooldownUpdate();
    }

    public void CooldownUpdate()
    {
        if (cdTimer < cooldown)
        {
            cdTimer += Time.deltaTime;
        }
        else
        {
            abilityReady = true;
        }
    }

    public override void SetupObj()
    {
        Debug.Log(ability_point);
        fireMeteor_object = ability_object.GetComponent<FireMeteor_Object>();
        fireMeteor_object.ability = this;
        ability_object.transform.position = ability_point.position;
        ability_object.transform.rotation = owner.transform.rotation;
    }

    public void CauseEffect()
    {
        //Debug.Log ("Onfire");
        CancelInvoke();
        onFireEffect.SetActive(true);
        owner.GetComponent<PlayerHealth>().onFire = true;
        Invoke("EndEffect", onFireTime_max);
    }

    public void EndEffect()
    {
        //Debug.Log ("Ceasefire");
        onFireEffect.SetActive(false);
        owner.GetComponent<PlayerHealth>().onFire = false;
    }
}
