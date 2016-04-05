using UnityEngine;
using System.Collections;

public class PlayerAudio : MonoBehaviour {

	// USAGE: Preferably attach an empty game object with an audio source component onto the player object. Assign each audio source object to the appropriate public game objects.

	// ADJUST AUDIO PITCH PER PLAYER TO:
	// Player 1 Pitch: 1.0
	// Player 2 Pitch: 0.92
	// Player 3 Pitch: 1.2
	// Player 4 Pitch: 0.84

	public GameObject player1Audio;
	public GameObject player2Audio;
	public GameObject player3Audio;
	public GameObject player4Audio;

	AudioSource player1AudioSource;
	AudioSource player2AudioSource;
	AudioSource player3AudioSource;
	AudioSource player4AudioSource;

	AudioClip pointSound;
	AudioClip jumpSound;
	AudioClip hitSound;

	// Use this for initialization
	void Start () 
	{
		player1AudioSource = player1Audio.GetComponent<AudioSource> ();
		player2AudioSource = player2Audio.GetComponent<AudioSource> ();
		player3AudioSource = player3Audio.GetComponent<AudioSource> ();
		player4AudioSource = player4Audio.GetComponent<AudioSource> ();

		// Fetch the files from the resources folder: if moved, files MUST be located in a .../Resources/ folder
		pointSound = Resources.Load <AudioClip> ("Item Sounds/Audio_PointSound");
		jumpSound = Resources.Load <AudioClip> ("Player Sounds/Audio_PlayerJump");
		hitSound = Resources.Load<AudioClip> ("Player Sounds/Audio_PlayerHit");

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Public methods for playing sounds - will probably need to be adjusted to be integrated with player actions!

	public void PlaySoundPoint(int playerNumber)
	{
		if (playerNumber == 1) 
		{
			player1AudioSource.PlayOneShot (pointSound);
		}

		if (playerNumber == 2) 
		{
			player2AudioSource.PlayOneShot (pointSound);
		}

		if (playerNumber == 3) 
		{
			player3AudioSource.PlayOneShot (pointSound);
		}

		if (playerNumber == 4) 
		{
			player4AudioSource.PlayOneShot (pointSound);
		}
	}

	public void PlaySoundJump(int playerNumber)
	{
		if (playerNumber == 1) 
		{
			player1AudioSource.PlayOneShot (jumpSound);
		}
		
		if (playerNumber == 2) 
		{
			player2AudioSource.PlayOneShot (jumpSound);
		}
		
		if (playerNumber == 3) 
		{
			player3AudioSource.PlayOneShot (jumpSound);
		}
		
		if (playerNumber == 4) 
		{
			player4AudioSource.PlayOneShot (jumpSound);;
		}
	}

	public void PlaySoundHit(int playerNumber)
	{
		if (playerNumber == 1) 
		{
			player1AudioSource.PlayOneShot (hitSound);
		}
		
		if (playerNumber == 2) 
		{
			player2AudioSource.PlayOneShot (hitSound);
		}
		
		if (playerNumber == 3) 
		{
			player3AudioSource.PlayOneShot (hitSound);
		}
		
		if (playerNumber == 4) 
		{
			player4AudioSource.PlayOneShot (hitSound);
		}
	}
}
