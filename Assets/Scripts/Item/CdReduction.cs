using UnityEngine;
using System.Collections;

public class CdReduction : Item {
	Ability[] ability;
	float[] cdOld;
	public override void takeEffect(GameObject player){
		ability = player.transform.GetComponent<PlayerAttack>().abilities;
		cdOld = new float[ability.Length];
		for (int i=0; i<ability.Length; i++) {
			cdOld[i] = ability[i].cooldown;
			ability[i].cooldown = ability[i].cooldown / 4f;
			ability[i].abilityReady = true;
			ability[i].cdTimer = 0f;
		}
		base.takeEffect (player);
	}
	public override void deleteEffect(){
		for (int i=0; i<cdOld.Length; i++) {
			ability[i].cooldown = cdOld[i];
			//Debug.Log(cdOld[i]+"!!!!!!!");
		}
		base.deleteEffect ();

	}
}
