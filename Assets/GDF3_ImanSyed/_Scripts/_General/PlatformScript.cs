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
	float startTime;
	bool willFall, isMoving;
	public float moveSpeed = 0.25f;
	Vector3 startPos, endPos;

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
		/*if (gm.rNum >= 5) {
			float r = Random.Range (0, 5);
			if(r <= 1 && isActive && !willFall){     	//Destroy after delay
				willFall = true;
				StartCoroutine(FallDown (3));
			}
		}*/
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
	/*	if (isMoving) {
			if (transform.position != endPos) {
				float distCovered = (Time.time - startTime) * moveSpeed;
				float fracJourney = distCovered / 2;
				transform.position = Vector3.Lerp (startPos, endPos, fracJourney);
			} else {
				isMoving = false;
			}
		}*/

	}

	private void Initialize(PlatformScript parent, Vector3 pos, string n){
		willFall = true;

		gm = FindObjectOfType<RestrictionScript> ();
		obs = GameObject.FindGameObjectWithTag ("Respawn");
		mesh = parent.mesh;
		material = parent.material;
		transform.parent = parent.transform;
		transform.localScale = Vector3.one;
		endPos = pos;
		//pos.y = -2f;
		startPos = pos;
		startTime = Time.deltaTime;
		isMoving = true;
		transform.localPosition = pos;
		posName = n;

		int x = 0;
		PlatformScript[] ps = FindObjectsOfType<PlatformScript> ();
		foreach (PlatformScript p in ps) {
			if (transform.position == p.transform.position) {
				x++;
				if (x == 2) {
					Destroy (gameObject);      //Destroy duplicates
				}
			}
		}



		if (gm.rNum >= 4) {
			float distance = Random.Range (0.05f, 0.165f);
			if (transform.position.x > 0) {
				pos.x += distance;
			}else{
				pos.x -= distance;
			}
			transform.localPosition = pos; //Platform distance
		}

		if(gm.rNum >= 3){
			float xpos = Random.Range (-0.425f, 0.425f);
			float ypos = Random.Range (0.3f, 1.5f);
			pos = new Vector3(xpos, ypos, 0);
			GameObject ob = Instantiate (obs,Vector3.zero, Quaternion.identity, gameObject.transform);  //Spawn obstacle
			ob.transform.localPosition = pos;
			Color c = new Color (Random.Range (0, 1f), Random.Range (0, 1f), Random.Range (0, 1f), Random.Range (0, 1f));
			ob.GetComponent<MeshRenderer> ().material.color = c;
		}

		if (x < 2) {
			float r1 = Random.Range (0, 10);
			float r2 = Random.Range (0, 10);
			float r3 = Random.Range (0, 10);
			if (r1 <= 5) {
				gm.SpawnStuff (1, transform.position);
			}
			if (r2 <= 7) {
				gm.SpawnStuff (2, transform.position);
			}
			if (r3 <= 4) {
				gm.SpawnStuff (3, transform.position);   //Spawn stuff
			}

		}
	}

	IEnumerator FallDown(float delay){
		yield return new WaitForSeconds (delay);
		transform.DetachChildren ();
		Destroy (gameObject);
		willFall = false;
	}

}
