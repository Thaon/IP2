using UnityEngine;
using System.Collections;

public class ArrowTrap : MonoBehaviour {
	
	public GameObject arrowSpawn;
	
	// Update is called once per frame
	//void Update () {
	//	if (Input.GetKeyDown (KeyCode.R)) {
	//		SpawnArrows ();
	//	}
	//}

    //activate is called from the input manager
	public void Activate(){
		GameObject arrow = (GameObject)Instantiate (arrowSpawn, transform.position, Quaternion.identity);
		arrow.tag = "arrow";
	}
	
}
