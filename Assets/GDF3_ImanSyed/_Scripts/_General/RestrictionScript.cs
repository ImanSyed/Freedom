using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestrictionScript : MonoBehaviour {

	public int rNum = 0;
	public GameObject platform, pc, obs;

	public int rNumDuration = 2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.F)) {
			rNum++;
			Vector3 v = pc.transform.position;
			if (rNum == 1) {
				v.y = v.y - 75f;
				platform.transform.position = v;
				platform.SetActive (true);
			}
			if (rNum == 2) {
				v.y -= 1f;
				platform.transform.position = v;
				v = Vector3.one;
				v.x = 20;
				v.z = 3;
				platform.transform.localScale = v;
			}
			if (rNum == 3) {

			}
		}
	}


}
