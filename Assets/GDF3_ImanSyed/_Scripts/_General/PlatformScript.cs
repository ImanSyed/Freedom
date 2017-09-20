using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour {

	public Mesh mesh;
	public Material material;
	public Transform player;
	public GameObject obs;

	RestrictionScript gm;

	public bool isActive;
	bool rChild, lChild, fChild, bChild = false;
	string posName = "";


	void Start(){
		gm = FindObjectOfType<RestrictionScript> ();
		isActive = false;
		gameObject.tag = "Finish";
		gameObject.AddComponent<MeshFilter> ().mesh = mesh;
		gameObject.AddComponent<MeshRenderer> ().material = material;
		gameObject.AddComponent<BoxCollider> ().size = Vector3.one;
		player = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	public void ActivateMe(){
		isActive = true;
	}


	public void DeactivateMe(){
		isActive = false;
	}

	void Update(){
		if (isActive) {
			if (player.position.x - transform.position.x >= 3.5f && !rChild && posName != "left") {
				rChild = true;
				new GameObject ("Child Platform").AddComponent<PlatformScript> ().Initialize (this, Vector3.right, "right");
			} else if (player.position.x - transform.position.x <= -3.5f && !lChild && posName != "right") {
				lChild = true;
				new GameObject ("Child Platform").AddComponent<PlatformScript> ().Initialize (this, Vector3.left, "left");
			}
			if (player.position.z - transform.position.z <= -3.5f && !bChild && posName != "forward") {
				bChild = true;
				new GameObject ("Child Platform").AddComponent<PlatformScript> ().Initialize (this, Vector3.back, "back");
			} else if (player.position.z - transform.position.z >= 3.5f && !fChild && posName != "back") {
				fChild = true;
				new GameObject ("Child Platform").AddComponent<PlatformScript> ().Initialize (this, Vector3.forward, "forward");
			}
		} 

	}

	private void Initialize(PlatformScript parent, Vector3 pos, string n){
		gm = FindObjectOfType<RestrictionScript> ();
		obs = GameObject.FindGameObjectWithTag ("Respawn");
		mesh = parent.mesh;
		material = parent.material;
		transform.parent = parent.transform;
		transform.localScale = Vector3.one;
		transform.localPosition = pos;
		posName = n;
		int x = 0;
		PlatformScript[] ps = FindObjectsOfType<PlatformScript> ();
		foreach (PlatformScript p in ps) {
			if (parent.transform.position == p.transform.position) {
				x++;
				if (x == 2) {
					Destroy (gameObject);
				}
			}
		}
		if(gm.rNum >= 3){
			float xpos = Random.Range (-0.45f, 0.45f);
			float ypos = Random.Range (0.75f, 1.5f);

		pos = new Vector3(xpos, ypos, 0);
		GameObject gay = Instantiate (obs,Vector3.zero, Quaternion.identity, gameObject.transform);
		gay.transform.localPosition = pos;
		MakeObstacle ();
	}
	}
	void MakeObstacle(){
		
	}
}
