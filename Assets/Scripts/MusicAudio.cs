using UnityEngine;
using System.Collections;

public class MusicAudio : MonoBehaviour {
	
	AudioSource musicAudioSource;
	
	public AudioClip[] musicTracks;

	// Use this for initialization
	void Start () 
	{
		musicAudioSource = gameObject.GetComponent<AudioSource> ();

		// Load all the tracks from the music tracks folder: Resources/Music Tracks/
		musicTracks = Resources.LoadAll<AudioClip>("Music Tracks");
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void PlayMusicTrack(int trackNumber)
	{
		musicAudioSource.Stop ();

		// Play music track based on the specified number - must be a number within the array!
		musicAudioSource.PlayOneShot(musicTracks[trackNumber]);
	}

	public void PlayRandomMusicTrack()
	{
		musicAudioSource.Stop ();

		// Pick a random track from our array of music tracks and play it
		musicAudioSource.PlayOneShot(musicTracks[Random.Range(0, musicTracks.Length)]);
	}

	public void StopMusicTrack()
	{
		musicAudioSource.Stop ();
	}
}
