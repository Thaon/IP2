using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UITransparency : MonoBehaviour {

	public GameObject[] spriteImages;
	public GameObject[] textObjects;

	public bool activateTransparency;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		// Loop through each sprite and text object in the arrays and change their colour
		if (activateTransparency)
		{
			for (int i = 0; i < spriteImages.Length; i++) 
			{		
				spriteImages[i].gameObject.GetComponent<Image>().color = new Color (1.0f, 1.0f, 1.0f, 0.4f);
			}

			for (int i = 0; i < textObjects.Length; i++) 
			{		
				textObjects[i].gameObject.GetComponent<Text>().color = new Color (1.0f, 1.0f, 1.0f, 0.4f);
			}
		}
		else
		{
			for (int i = 0; i < spriteImages.Length; i++) 
			{		
				spriteImages[i].gameObject.GetComponent<Image>().color = new Color (1.0f, 1.0f, 1.0f, 1.0f);
			}

			for (int i = 0; i < textObjects.Length; i++) 
			{		
				textObjects[i].gameObject.GetComponent<Text>().color = new Color (1.0f, 1.0f, 1.0f, 1.0f);
			}
		}

	}

	// Transparency will enable / disable if a player with a player tags enters / exits the trigger. (Players will probably need an accurate rigidbody of some form with the tag "Player" for detection)

	void OnTriggerStay2D(Collider2D other) 
	{
		if (other.gameObject.tag == "Player") 
		{
			activateTransparency = true;
		}
	}

	void OnTriggerExit2D(Collider2D other) 
	{
		if (other.gameObject.tag == "Player") 
		{
			activateTransparency = false;
		}
	}
}
