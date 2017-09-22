using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviourScript : MonoBehaviour {

	Vector3 pos;
	Vector3 startPos;
	int i = 1;
	bool changing;
	public bool followPlayer;


	public float step = 1;
	public float changeDelay = 2.5f;
	public string whichAxis;
	GameObject pc;
	Animator anim;

	void Start () {
		pc = GameObject.FindGameObjectWithTag ("Player");
		anim = GetComponent<Animator> ();
		startPos = pos;
	}

	void Update () {

		if (Mathf.Abs (transform.position.x - pc.transform.position.x) <= 1.5f && Mathf.Abs (transform.position.z - pc.transform.position.z) <= 1.5f) {
			if (anim.GetInteger ("State") != 1) {
				transform.Rotate(0,0,0);
				anim.SetInteger ("State", 1);
			}
		} else {
			if (anim.GetInteger ("State") != 0) {
				anim.SetInteger ("State", 0);
			}

			
		}

		if (anim.GetInteger ("State") == 0) {
			if (followPlayer) {
				Vector3 followPos = pc.transform.position;
				followPos.y = transform.position.y;
				transform.LookAt (followPos);
				transform.position += transform.forward * step * 60 * Time.deltaTime;
			} else {
				if (!changing) {
					changing = true;
					StartCoroutine (changeDirection (changeDelay));
				}
				pos = transform.position;
				if (whichAxis == "x") {
					pos.x += step * i;
				} else if (whichAxis == "z") {
					pos.z += step * i;
				} else if (whichAxis == "y") {
					pos.y += step * i;
				}
				transform.position = pos;
			}
		} 


	}

	IEnumerator changeDirection(float delay){
		yield return new WaitForSeconds (delay);
		i = i * -1;
		Vector3 rot = transform.rotation.eulerAngles;
		rot.y = transform.rotation.eulerAngles.y - 90 * i;
		transform.Rotate (rot);
		changing = false;
	}
}
