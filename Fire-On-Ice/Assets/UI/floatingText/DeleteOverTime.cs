using UnityEngine;
using System.Collections;

public class DeleteOverTime : MonoBehaviour {
    public float time = 2f;

    private float timer;

	// Use this for initialization
	void Start () {
		transform.rotation = Quaternion.Euler (0, 0, 0);
        timer = time;
	}
	
	// Update is called once per frame
	void Update()
    {
		transform.rotation = Quaternion.Euler (0, 0, 0);
        timer = Mathf.MoveTowards(timer, 0, Time.deltaTime);
        if (timer <= 0)
        {
            Destroy(this.gameObject);
        }
    
	}
}
