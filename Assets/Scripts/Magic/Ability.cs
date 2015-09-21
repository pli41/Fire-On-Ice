using UnityEngine;
using System.Collections;

public class Ability : MonoBehaviour {

	public GameObject owner;
	public GameObject ability_object;
	public readonly float damage;
	public readonly float cooldown;
	public readonly float manaCost;
	public Transform ability_point;

	public bool abilityReady;

	public virtual void Cast(){}

	public virtual void EndCast(){}

	public virtual void SetupObj(){}
}
