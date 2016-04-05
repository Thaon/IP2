using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

	public bool shouldBeLaunched;

	public float arrowTriggerDelay = 0.6f;
	float arrowTriggerDelayDefault;

	// Use this for initialization
	void Start () 
	{
		arrowTriggerDelayDefault = arrowTriggerDelay;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (shouldBeLaunched) 
		{
			if (arrowTriggerDelay > 0)
			{
				arrowTriggerDelay -= Time.deltaTime;
			}
		}
	}

	public void ResetArrow()
	{
		shouldBeLaunched = false;
		arrowTriggerDelay = arrowTriggerDelayDefault;
	}

	public void LaunchArrow()
	{
		shouldBeLaunched = true;
	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		if (arrowTriggerDelay < 0) 
		{
			if (other.gameObject.tag == "Surface" || other.gameObject.tag == "Trap: Arrow" || other.gameObject.tag == "Trap: Crumble" || other.gameObject.tag == "Trap: Flame" || other.gameObject.tag == "Trap: Spike" )
			{
				Debug.Log ("THIS HAS HIT SOMETHING");
				gameObject.SetActive(false);
			}
		}

	}
}
