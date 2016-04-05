using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TrapFlames : MonoBehaviour {
	
	GameObject[] flameTraps;
	GameObject[] flameTrapButtons;
	
	public float trapCooldown = 2;
	public float buttonCooldown = 3;
	float trapCooldownResetValue;
	float buttonCooldownResetValue;
	
	Vector3 buttonOriginalScale;
	
	bool trapIsActive = false;
	bool trapIsReady = true;

	public AudioClip trapFlamesSoundEffect;
	
	// Use this for initialization
	void Start () 
	{
		// Find all traps and buttons with the specified tag(s)
		flameTraps = GameObject.FindGameObjectsWithTag ("Trap: Flame");
		flameTrapButtons = GameObject.FindGameObjectsWithTag ("Button Prompt: B");
		
		trapCooldownResetValue = trapCooldown;
		buttonCooldownResetValue = buttonCooldown;
		
		buttonOriginalScale = flameTrapButtons [0].gameObject.transform.localScale;
	}
	
	// Update is called once per frame
	void Update () 
	{
		// If the input button is pressed and the trap is ready, activate it
		if (Input.GetKeyDown(KeyCode.B) && !trapIsActive && trapIsReady) 
		{
			// Loop through and activate our traps
			for (int i = 0; i < flameTraps.Length; i++)
			{
				flameTraps[i].gameObject.SetActive(true);
			}

			// Loop through our buttons and change the colour and scale
			for (int i = 0; i < flameTrapButtons.Length; i++)
			{
				flameTrapButtons[i].gameObject.GetComponent<SpriteRenderer>().color = new Color (1.0f, 1.0f, 1.0f, 0.4f);
				
				flameTrapButtons[i].gameObject.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
			}

			gameObject.GetComponent<AudioSource>().PlayOneShot(trapFlamesSoundEffect);

			trapIsActive = true;
			trapIsReady = false;
		}
		
		// Cooldown for the trap before resetting trap
		if (trapIsActive && trapCooldown > 0) 
		{
			trapCooldown -= Time.deltaTime;
		} 
		else 
		{
			for (int i = 0; i < flameTraps.Length; i++)
			{
				flameTraps[i].gameObject.SetActive(false);
			}
			
			trapCooldown = trapCooldownResetValue;
			trapIsActive = false;
		}
		
		// Button cooldown that's longer than trap cooldown to avoid trap spamming.
		if (!trapIsReady && buttonCooldown > 0) 
		{
			buttonCooldown -= Time.deltaTime;
			
			for (int i = 0; i < flameTrapButtons.Length; i++)
			{
				flameTrapButtons[i].gameObject.transform.localScale = Vector3.Lerp(flameTrapButtons[i].gameObject.transform.localScale, buttonOriginalScale, 0.015f); // Last value needs maths for proper timing
			}
		} 
		else 
		{
			for (int i = 0; i < flameTrapButtons.Length; i++)
			{
				flameTrapButtons[i].gameObject.GetComponent<SpriteRenderer>().color = new Color (1.0f, 1.0f, 1.0f, 1.0f);
			}
			
			buttonCooldown = buttonCooldownResetValue;
			trapIsReady = true;
		}
		
	}
	
}
