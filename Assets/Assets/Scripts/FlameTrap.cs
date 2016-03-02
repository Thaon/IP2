using UnityEngine;
using System.Collections;

public class FlameTrap : MonoBehaviour {

	public GameObject flameSpawn;

	// Update is called once per frame
	//void Update () {
	//	if (Input.GetKeyDown(KeyCode.E)) {
	//		SpawnFlames ();
	//	}
	//}

        //we'll be activating the trap from the input manager
		public void Activate(){
			GameObject flames = (GameObject)Instantiate (flameSpawn, transform.position, Quaternion.identity);
			flames.tag = "flames";
		}
	
}
