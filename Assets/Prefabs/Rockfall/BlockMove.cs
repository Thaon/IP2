using UnityEngine;
using System.Collections;

public class BlockMove : MonoBehaviour {

	float BlockSPD=0;
	float Timer=0.0f;
	// Use this for initialization
	void Start () 
	{
		GameObject Rock = GameObject.Find("Rockfall");
		RockFall Move = Rock.GetComponent<RockFall> ();
		BlockSPD = Move.BlockSpeed;
		if(Move.IsRise==false)
		{ 
			BlockSPD*=-1.0f;
		}


	}
	
	// Update is called once per frame
	void Update () 
	{
		Timer += Time.deltaTime;
		transform.Translate(0.0f,BlockSPD*Time.deltaTime,0.0f);
		if (Timer > 6) 
		{
			Destroy(gameObject);
		}

	}
}
