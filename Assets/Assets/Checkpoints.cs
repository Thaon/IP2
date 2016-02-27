using UnityEngine;
using System.Collections;

public class Checkpoints : MonoBehaviour {

	public int playerScore;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (playerScore >= 5) {
			Application.LoadLevel ("Endscene");
		}
	}
	void OnTriggerEnter2D(Collider2D other){
		Debug.Log ("Collided");
		playerScore += 1;


}

}

