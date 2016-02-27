using UnityEngine;
using System.Collections;
public class ArrowRight : MonoBehaviour {
	public float speed;
	
	// Use this for initialization
	void Start () { 
		rigidbody2D.AddForce(new Vector2(100, 0), ForceMode2D.Impulse);

	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(speed * Time.deltaTime, 0, 0);
	}
	void OnCollisionEnter2D(Collision2D c){
		if (c.gameObject.tag == "Platform") {
			Destroy(gameObject);
		}
	}

}
