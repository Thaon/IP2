using UnityEngine;
using System.Collections;
public class ArrowLeft : MonoBehaviour {
	public float speed;
	
	// Use this for initialization
	void Start () { 
		rigidbody2D.AddForce(new Vector2(0 - 100, 0), ForceMode2D.Impulse);
        StartCoroutine(SelfDestroy());
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
    IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
}