using UnityEngine;
using System.Collections;

public class FallingBlocks : MonoBehaviour {
	GameObject FallingBlock;

	// Use this for initialization
	void Start () {
		rigidbody2D.isKinematic = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.W)) {
		
			rigidbody2D.isKinematic = false;

		}
	}
	
}
