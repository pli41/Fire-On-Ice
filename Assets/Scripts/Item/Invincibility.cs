using UnityEngine;
using System.Collections;

public class Invincibility : Item {
	public GameObject player;
	public override void takeEffect (GameObject player)
	{
		this.player = player;
		deleteEffect ();
		player.transform.GetComponent<PlayerHealth> ().damageReduction = 0f;
		base.takeEffect (player);
	}
	public override void deleteEffect ()
	{
		player.transform.GetComponent<PlayerHealth> ().damageReduction = 1f;
		base.deleteEffect ();

	}
}
