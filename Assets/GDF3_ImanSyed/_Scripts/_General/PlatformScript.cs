using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour {

	public Mesh mesh;
	public Material material;
	public Transform player;

	bool isActive;
	bool rChild, lChild, fChild, bChild = false;
	string posName = "";

	bool awake;


	void Start(){
		isActive = false;
		gameObject.tag = "Finish";
		gameObject.AddComponent<MeshFilter> ().mesh = mesh;
		gameObject.AddComponent<MeshRenderer> ().material = material;
		gameObject.AddComponent<BoxCollider> ().size = Vector3.one;
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		awake = true;
	}

	public void ActivateMe(){
		isActive = true;
	}


	public void DeactivateMe(){
		isActive = false;
	}

	void Update(){
		if (!awake && mesh == null) {
			Destroy (gameObject);
		}

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
				;
				fChild = true;
				new GameObject ("Child Platform").AddComponent<PlatformScript> ().Initialize (this, Vector3.forward, "forward");
			}
		} 

	}

	private void Initialize(PlatformScript obj, Vector3 pos, string n){
		mesh = obj.mesh;
		material = obj.material;
		transform.parent = obj.transform;
		transform.localScale = Vector3.one;
		transform.localPosition = pos;
		posName = n;
		int x = 0;
		PlatformScript[] ps = FindObjectsOfType<PlatformScript> ();
		foreach (PlatformScript p in ps) {
			if (obj.transform.position == p.transform.position) {
				x++;
				if (x == 2) {
					Destroy (this);
				}
			}
		}
	}
}
