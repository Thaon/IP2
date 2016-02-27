using UnityEngine;
using System.Collections;

public class FlameTrap : MonoBehaviour {

	public GameObject flameSpawn;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.E)) {
			SpawnFlames ();
		}
	}
		public void SpawnFlames(){
			GameObject flames = (GameObject)Instantiate (flameSpawn, transform.position, Quaternion.identity);
			flames.tag = "flames";
		}
	
}
