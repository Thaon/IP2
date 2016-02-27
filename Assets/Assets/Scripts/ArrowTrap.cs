using UnityEngine;
using System.Collections;

public class ArrowTrap : MonoBehaviour {
	
	public GameObject arrowSpawn;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.R)) {
			SpawnArrows ();
		}
	}
	public void SpawnArrows(){
		GameObject arrow = (GameObject)Instantiate (arrowSpawn, transform.position, Quaternion.identity);
		arrow.tag = "arrow";
	}
	
}
