using UnityEngine;
using System.Collections;

namespace Ip2
{
    class AudioManager : MonoBehaviour
    {
        [SerializeField]
        AudioClip[] m_musicTracks;
        AudioSource m_audioSource;

        void Start()
        {
            m_audioSource = GetComponent<AudioSource>();
            int ran = Random.Range(0, m_musicTracks.Length);
            m_audioSource.PlayOneShot(m_musicTracks[ran]);
        }
    }
}
