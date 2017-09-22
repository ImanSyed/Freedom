using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestrictionScript : MonoBehaviour {

	public int rNum = 0;
	public GameObject platform, pc, cloud, enemy;
	public GameObject tree, rock1, rock2;

	public int rNumDuration = 2;

	bool spawnCloud, spawnEnemy;


	void Update () {
		if (Input.GetKeyDown (KeyCode.R)) {
			SceneManager.LoadScene ("1");
		}

		if (Input.GetKeyDown (KeyCode.F)) {
			rNum++;
			Vector3 v = pc.transform.position;
			if (rNum == 1) {
				v.y = v.y - 75f;
				platform.transform.position = v;
				platform.SetActive (true);			//Setting platform under player
			}
			if (rNum == 2) {
				v.y -= 1f;
				platform.transform.position = v;
				v = Vector3.one;
				v.x = 20;
				v.z = 3;
				platform.transform.localScale = v;  //Narrowing the platforms
			}
		}
		if (rNum >= 2) {
			if (!spawnCloud) {
				spawnCloud = true;
				StartCoroutine (Clouds (Random.Range(5,10))); //spawn cloud
			}
		}
		if (rNum >= 5) {
			if (!spawnEnemy) {
				spawnEnemy = true;
				StartCoroutine (Enemy (Random.Range(5,10))); //spawn cloud
			}
		}
	}


	public void SpawnStuff(int i, Vector3 pos){
		pos.x += Random.Range (-4, 4);
		pos.z += Random.Range (-2.25f, 2.25f);
		pos.y += 0.5f;
		float size = Random.Range (0.5f, 2);
		GameObject o;
		if (i == 1) {
			o = Instantiate (tree, pos, Quaternion.Euler(-90, 0, 0));
			o.transform.localScale = new Vector3 (size, size, size);
		} else if (i == 2) {
			o = Instantiate (rock1, pos, Quaternion.identity);
			o.transform.localScale = new Vector3 (size, size, size);
		} else {
			o = Instantiate (rock2, pos, Quaternion.identity);
			o.transform.localScale = new Vector3 (size, size, size);
		}
		Debug.Log ("Spawned");
	}


	IEnumerator Clouds(float delay){
		yield return new WaitForSeconds (delay);
		Vector3 pos = pc.transform.position;
		float yPos = Random.Range (1.5f, 6);
		float zPos = Random.Range (-4f, 4f);
		pos.y += yPos;
		pos.z += zPos;
		if (pc.transform.position.x >= 0) {
			pos.x += Random.Range (7, 15);
		} else {
			pos.x -= Random.Range (7, 15);
		}
		Instantiate (cloud, pos, Quaternion.identity);
		spawnCloud = false;
	}

	IEnumerator Enemy(float delay){
		yield return new WaitForSeconds (delay);
		Vector3 pos = pc.transform.position;
		if (pc.transform.position.x >= 0) {
			pos.x += Random.Range (7, 15);
		} else {
			pos.x -= Random.Range (7, 15);
		}
		Instantiate (enemy, pos, Quaternion.identity);
		spawnEnemy = false;
	}
}
