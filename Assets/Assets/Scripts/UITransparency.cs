using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UITransparency : MonoBehaviour {
	
	// SET THIS UP FOR A COLLIDER!

	public GameObject[] spriteImages;
	public GameObject[] textObjects;

	public bool activateTransparency = false;


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
}
