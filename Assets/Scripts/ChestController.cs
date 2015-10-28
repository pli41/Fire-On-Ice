using UnityEngine;
using System.Collections;

public class ChestController : MonoBehaviour {
	public int chestNum = 10;
	public GameObject chest;
	private Vector3 position;
	private	Quaternion rotation;
	public float timer;
	public float generateTime = 5;

	private GameManager gm;

	void Start(){
		gm = GameObject.Find ("GameManager").GetComponent<GameManager>();
	}

	// Update is called once per frame
	void Update () {
		if(gm.GameInProgress){
			timer += Time.deltaTime;
			if (timer > generateTime) {
				spawnChest ();
				timer=0;
			}
		}
	}

	void spawnChest(){
		int spawned=0;
		while(spawned<chestNum){
			position = new Vector3(Random.Range (-100f,100f),10,Random.Range(-100f,100f));
			//rotation = new Quaternion(0,Random.Range (0f,180f),0,0);

			//detectRay = Camera.main.ScreenPointToRay(position);
			//dir = new Vector3(position.x,0,position.z);


			RaycastHit hit;
			if(Physics.Raycast(position,new Vector3(0,-1,0),out hit,15)){
				Instantiate(chest ,position, Quaternion.Euler(0, 180f, 0));
				//Debug.DrawRay(temp.transform.position,Vector3.down,Color.green);
				spawned++;
			}

		}
	}
}
