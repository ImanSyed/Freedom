using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorScript : MonoBehaviour {


	void LateUpdate(){
		/*
		transform.rotation = Quaternion.Euler (Vector3.zero);
		transform.localRotation = Quaternion.Euler (Vector3.zero);
		*/
	}
		

	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Finish") {
			col.gameObject.SendMessage ("ActivateMe");
		}
	}

	void OnTriggerExit(Collider col){
		if (col.gameObject.tag == "Finish") {
			col.gameObject.SendMessage ("DeactivateMe");
		}
	}
}
