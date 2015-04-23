using UnityEngine;
using System.Collections;

public class Island : MonoBehaviour {

	public float shrinkSize = 5f;
	public float timeBetShrink = 5f;

	private float timer;
	Mesh mesh;
	MeshCollider meshC;
	Vector3[] vertices;
	GameObject[] fireballs;


	// Use this for initialization
	void Start () {
		mesh = GetComponent<MeshFilter> ().mesh;
		meshC = GetComponent<MeshCollider> ();
		vertices = mesh.vertices;
		fireballs = GameObject.FindGameObjectsWithTag("Fireball");
		Debug.Log (vertices.Length);
	}
	
	// Update is called once per frame
	void Update () {
		if(timer >= timeBetShrink){
			timer = 0f;
			Shrink();
		}
		else{
			timer += Time.deltaTime;
		}
		checkVertices ();
	}

	void checkVertices(){
		fireballs = GameObject.FindGameObjectsWithTag("Fireball");
		for (int i = 0; i< vertices.Length; i++){
			foreach (GameObject fireball in fireballs){
				ParticleSystem ps = fireball.GetComponent<ParticleSystem>();
				//Debug.Log(Vector3.Distance(vertice, fireball.transform.position));
				Debug.Log(ps.transform.position);
				if(Vector3.Distance(vertices[i], ps.transform.position)< 4f){
					Debug.Log("Shakalaka");
					vertices[i].Set(vertices[i].x, vertices[i].y-0.1f, vertices[i].z);

					mesh.vertices = vertices;
					meshC.sharedMesh = mesh;
					meshC.convex = !meshC.convex;
//					Vector3 curr = thisV;
//					curr.y -= 5f;
//					thisV = curr;
				}
			}
		}


	}


	void Shrink(){
		Vector3 curScale = transform.localScale;
		curScale.x -= shrinkSize;
		curScale.z -= shrinkSize;
		transform.localScale = curScale;
	}
}
