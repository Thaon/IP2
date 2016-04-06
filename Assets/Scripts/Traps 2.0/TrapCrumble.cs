using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TrapCrumble : MonoBehaviour {
	
	GameObject[] crumbleTraps;
	GameObject[] crumbleTrapButtons;
	
	public float trapCooldown = 2;
	public float buttonCooldown = 3;
	float trapCooldownResetValue;
	float buttonCooldownResetValue;
	
	Vector3 buttonOriginalScale;
	
	bool trapIsActive = false;
	bool trapIsReady = true;

	public AudioClip trapCrumbleSoundEffect;
	
	// Use this for initialization
	void Start () 
	{
		// Find all traps and buttons with the specified tag(s)
		crumbleTraps = GameObject.FindGameObjectsWithTag ("Trap: Crumble");
		crumbleTrapButtons = GameObject.FindGameObjectsWithTag ("Button Prompt: A");
		
		// Store the original cooldown values for resetting
		trapCooldownResetValue = trapCooldown;
		buttonCooldownResetValue = buttonCooldown;
		
		// Store the original scale for when we're resizing / lerping the scale
		buttonOriginalScale = crumbleTrapButtons [0].gameObject.transform.localScale;
	}
	
    public void Activate()
    {
        // If the input button is pressed and the trap is ready, activate it
        if (Input.GetKeyDown(KeyCode.A) && !trapIsActive && trapIsReady)
        {
            // Loop through and activate our traps
            for (int i = 0; i < crumbleTraps.Length; i++)
            {
                crumbleTraps[i].gameObject.SetActive(false);
            }

            // Loop through our buttons and change the colour and scale
            for (int i = 0; i < crumbleTrapButtons.Length; i++)
            {
                crumbleTrapButtons[i].gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.4f);

                crumbleTrapButtons[i].gameObject.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            }

            gameObject.GetComponent<AudioSource>().PlayOneShot(trapCrumbleSoundEffect);

            trapIsActive = true;
            trapIsReady = false;
        }
    }

	// Update is called once per frame
	void Update ()
    {		
		// Cooldown for the trap before resetting trap
		if (trapIsActive && trapCooldown > 0) 
		{
			trapCooldown -= Time.deltaTime;
		} 
		else 
		{
			for (int i = 0; i < crumbleTraps.Length; i++)
			{
				crumbleTraps[i].gameObject.SetActive(true);
			}
			
			trapCooldown = trapCooldownResetValue;
			trapIsActive = false;
		}
		
		// Button cooldown that's longer than trap cooldown to avoid trap spamming.
		if (!trapIsReady && buttonCooldown > 0) 
		{
			buttonCooldown -= Time.deltaTime;
			
			for (int i = 0; i < crumbleTrapButtons.Length; i++)
			{
				crumbleTrapButtons[i].gameObject.transform.localScale = Vector3.Lerp(crumbleTrapButtons[i].gameObject.transform.localScale, buttonOriginalScale, 0.01f); // Last value needs maths for proper timing
			}
		} 
		else 
		{
			for (int i = 0; i < crumbleTrapButtons.Length; i++)
			{
				crumbleTrapButtons[i].gameObject.GetComponent<SpriteRenderer>().color = new Color (1.0f, 1.0f, 1.0f, 1.0f);
			}
			
			buttonCooldown = buttonCooldownResetValue;
			trapIsReady = true;
		}
		
	}
	
}
