using UnityEngine;
using System.Collections;

public class Ability : MonoBehaviour {

	public GameObject owner;
	public GameObject ability_object;
	public readonly float damage;

	public readonly float manaCost;
	public Transform ability_point;

	//For changable cooldowns like fireballs, will be used in cooldown counter
	public float cooldown_new;
	public bool abilityReady;
	public float cdTimer;

	public virtual void Cast(){}

	public virtual void EndCast(){}

	public virtual void SetupObj(){}
}
