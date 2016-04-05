using UnityEngine;
using System.Collections;

public class RockFall : MonoBehaviour {

	public GameObject Block;
	public float BlockSpeed= 5.0f;
	public bool IsRise;
	float fucknugget;
	int RanA, RanB, RanC, RanD;
	public GameObject[] RockSpawn;
	// Use this for initialization
	// Update is called once per frame
	void Update () 
	{
		fucknugget += Time.deltaTime;

		if(fucknugget>1.5f)
		{
			RanA=Random.Range(1,4);
			RanB=Random.Range(5,8);
			RanC=Random.Range(9,12);
			RanD=Random.Range(13,16);
			for(int i=0; i<RockSpawn.Length; i++)
			{ 
				if(i==RanA ||i==RanB || i==RanC || i==RanD)
				{
					Instantiate(Block,RockSpawn[i].transform.position,RockSpawn[i].transform.rotation);
				}
			}
			fucknugget=0;
		}

	}
}
