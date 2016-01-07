using UnityEngine;
using System.Collections;

public class SpeedPotion : Item {
	public float increasement;
	public float oldSpeed;
	GameObject thisPlayer;
	public override void takeEffect(GameObject player){

		this.thisPlayer = player;
		oldSpeed = player.GetComponent<PlayerMovement> ().maxSpeed;
		thisPlayer.GetComponent<PlayerMovement> ().oldMaxSpeed = increasement;
		thisPlayer.GetComponent<PlayerMovement> ().maxSpeed = increasement;
		base.takeEffect (player);
	}

	public override void deleteEffect(){
		thisPlayer.transform.GetComponent<PlayerMovement> ().oldMaxSpeed = oldSpeed;
		thisPlayer.GetComponent<PlayerMovement> ().maxSpeed = oldSpeed;
		base.deleteEffect ();

	}
}
