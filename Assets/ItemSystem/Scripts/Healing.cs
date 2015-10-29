using UnityEngine;
using System.Collections;

public class Healing : Item {
	public int healingAmount;
	public override void takeEffect(GameObject player){
		player.transform.GetComponent<PlayerHealth>().TakeDamage(-healingAmount,false, 0);
		base.takeEffect (player);

	}
}
