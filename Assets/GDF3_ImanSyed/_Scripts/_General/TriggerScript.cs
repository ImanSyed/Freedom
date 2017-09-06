using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour {

	public GameObject Plat;
	public GameObject spawnLeft;
	public GameObject spawnRight;
	public GameObject spawnTop;
	public GameObject spawnBottom;



	void OnTriggerEnter(Collider col){
		
		if (col.gameObject.tag == "Player") {
			if (transform.localPosition.x > 0) {
				Debug.Log ("Right");
				GameObject ob = (GameObject)Instantiate (Plat, spawnRight.transform.position, Plat.transform.rotation);
				Destroy(ob.transform.Find("Trigger Left").gameObject);

			} 
			else if(transform.localPosition.x < 0) {
				Debug.Log ("Left");
				GameObject ob = (GameObject)Instantiate (Plat, spawnLeft.transform.position, Plat.transform.rotation);
				Destroy(ob.transform.Find("Trigger Right").gameObject);

			}
			else if(transform.localPosition.z < 0) {
				Debug.Log ("Bottom");
				GameObject ob = (GameObject)Instantiate (Plat, spawnBottom.transform.position, Plat.transform.rotation);
				Destroy(ob.transform.Find("Trigger Top").gameObject);

			}
			else if(transform.localPosition.z > 0) {
				Debug.Log ("Top");
				GameObject ob = (GameObject)Instantiate (Plat, spawnTop.transform.position, Plat.transform.rotation);
				Destroy(ob.transform.Find("Trigger Bottom").gameObject);

			}
			Destroy (gameObject);
		}
	}
}
