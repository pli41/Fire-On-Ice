using UnityEngine;
using System.Collections;

public class Ability : MonoBehaviour {

	public GameObject owner;
	public GameObject ability_object;
	public readonly float damage;
	public bool handledEndCast;
	public readonly float manaCost;
	public Transform ability_point;
	public bool triggerOnce;


	public bool endCasted;

	public float cooldown;
	public float timeUntilReset;

	public Sprite icon;
	//public string name;
	public string description;
	
	//For changable cooldowns like fireballs, will be used in cooldown counter
	//public float cooldown_new;

	public bool abilityReady;
	public float cdTimer;

	public virtual void Cast(){}

	public virtual void EndCast(){}

	public virtual void SetupObj(){}

	public virtual void SetupAbility(){}
}
