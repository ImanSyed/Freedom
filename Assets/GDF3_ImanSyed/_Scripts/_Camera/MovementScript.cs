using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour {

	public float speed = 5;

	float gravity = 30;
	private Vector3 moveDirection = Vector3.zero;

	public GameObject gm;

	public float jumpForce;

	CharacterController controller;
		
	void Start(){
		controller = GetComponent<CharacterController> ();
		gm = GameObject.FindGameObjectWithTag ("GameController");
	}

	void Update () {
		
		if (gm.GetComponent<RestrictionScript>().rNum == 0) {
			moveDirection = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
			moveDirection = transform.TransformDirection (moveDirection);
			moveDirection *= speed;
			controller.Move (moveDirection * Time.deltaTime);
		} else {
			if (controller.isGrounded) {
				moveDirection = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
				moveDirection = transform.TransformDirection (moveDirection);
				moveDirection *= speed;
				if (Input.GetButtonDown ("Jump")) {
					moveDirection.y = jumpForce;
				}
			}
			controller.Move (moveDirection * Time.deltaTime);
			moveDirection.y -= gravity * Time.deltaTime;

		}
	}	
}
