using UnityEngine;
using System.Collections;

public class SpikeTrap : MonoBehaviour {
	
	public GameObject spikeSpawn;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Q)) {
			SpawnSpikes ();
		}
	}
	public void SpawnSpikes(){
		GameObject spikes = (GameObject)Instantiate (spikeSpawn, transform.position, Quaternion.identity);
		spikes.tag = "spikes";
	}
	
}
