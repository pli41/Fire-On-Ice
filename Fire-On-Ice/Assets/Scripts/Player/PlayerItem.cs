using UnityEngine;
using System.Collections;

public class PlayerItem : MonoBehaviour {

	private int joystickNum;

	public GameObject chestAround;
	public GameObject healingEffect;
	public int chestOpenedNum;
	private GameManager gm;

	// Use this for initialization
	void Start () {
		gm = GameObject.Find ("GameManager").GetComponent<GameManager>();
		joystickNum = GetComponent<PlayerAttack>().joystickNum;
	}
	
	// Update is called once per frame
	void Update () {
        //if (gm.GameInProgress)
        //{
        //    if (ControllerManager.GetButton(ControllerInputWrapper.Buttons.A, joystickNum))
        //    {
        //        if (chestAround)
        //        {
        //            //Debug.Log("Open chest");
        //            chestOpenedNum++;
        //            chestAround.GetComponent<Chest>().Open();
        //            chestAround = null;
        //        }

        //    }
        //}
    }
}
