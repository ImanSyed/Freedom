using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour {
	public int restrictionDuration = 2;
	int restriction = 0;
	public float speed = 5;
	public float jumpSpeed = 5;
	float gravity = 50;
	private Vector3 moveDirection = Vector3.zero;
	bool flag;
	float t;
	public GameObject platform;

	float creationDist;

	void Start(){
		creationDist = platform.transform.position.x;
	}

	void Update () {
		
		t += Time.deltaTime;
		if (t >= restrictionDuration) {
			flag = true;
		}

		if (creationDist - transform.position.x <= 4) {
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			Debug.Log ("Space");
			if (flag) {
				Debug.Log ("Free");
				restriction++;
				flag = false;
				t = 0;
				Vector3 v = transform.position;
				if (restriction == 1) {
					v.y = v.y - 75f;
					platform.transform.position = v;
					platform.SetActive (true);
					gameObject.GetComponent<Rigidbody> ().useGravity = true;
				}
				if (restriction == 2) {
					platform.transform.position = v;
					v = Vector3.one;
					v.x = 35;
					v.z = 5;
					platform.transform.localScale = v;
				}
			}
		}

		if (restriction == 0) {
			CharacterController controller = GetComponent<CharacterController> ();
			moveDirection = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
			moveDirection = transform.TransformDirection (moveDirection);
			moveDirection *= speed;
			controller.Move (moveDirection * Time.deltaTime);
		} else {
			CharacterController controller = GetComponent<CharacterController> ();
			if (controller.isGrounded) {
				moveDirection = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
				moveDirection = transform.TransformDirection (moveDirection);
				moveDirection *= speed;
			}
			moveDirection.y -= gravity * Time.deltaTime;
			controller.Move (moveDirection * Time.deltaTime);
		}
	}
		
}
