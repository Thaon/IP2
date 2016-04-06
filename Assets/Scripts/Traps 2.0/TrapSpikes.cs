using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Ip2
{
    public class TrapSpikes : MonoBehaviour
    {

        GameObject[] spikeTraps;
        GameObject[] spikeTrapButtons;

        public float trapCooldown = 2;
        public float buttonCooldown = 3;
        float trapCooldownResetValue;
        float buttonCooldownResetValue;

        Vector3 buttonOriginalScale;

        bool trapIsActive = false;
        bool trapIsReady = true;

        public AudioClip trapSpikesSoundEffect;

        // Use this for initialization
        void Start()
        {
            // Find all traps and buttons with the specified tag(s)
            spikeTraps = GameObject.FindGameObjectsWithTag("Trap: Spike");
            spikeTrapButtons = GameObject.FindGameObjectsWithTag("Button Prompt: Y");

            // Store the original cooldown values for resetting
            trapCooldownResetValue = trapCooldown;
            buttonCooldownResetValue = buttonCooldown;

            // Store the original scale for when we're resizing / lerping the scale
            buttonOriginalScale = spikeTrapButtons[0].gameObject.transform.localScale;
        }

        public void Activate()
        {
            // If the input button is pressed and the trap is ready, activate it
            if (Input.GetKeyDown(KeyCode.Y) && !trapIsActive && trapIsReady)
            {
                // Loop through and activate our traps
                for (int i = 0; i < spikeTraps.Length; i++)
                {
                    spikeTraps[i].gameObject.SetActive(true);
                }

                // Loop through our buttons and change the colour and scale
                for (int i = 0; i < spikeTrapButtons.Length; i++)
                {
                    spikeTrapButtons[i].gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.4f);

                    spikeTrapButtons[i].gameObject.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                }

                gameObject.GetComponent<AudioSource>().PlayOneShot(trapSpikesSoundEffect);

                trapIsActive = true;
                trapIsReady = false;
            }
        }

        // Update is called once per frame
        void Update()
        {
            // Cooldown for the trap before resetting trap
            if (trapIsActive && trapCooldown > 0)
            {
                trapCooldown -= Time.deltaTime;
            }
            else
            {
                for (int i = 0; i < spikeTraps.Length; i++)
                {
                    spikeTraps[i].gameObject.SetActive(false);
                }

                trapCooldown = trapCooldownResetValue;
                trapIsActive = false;
            }

            // Button cooldown that's longer than trap cooldown to avoid trap spamming.
            if (!trapIsReady && buttonCooldown > 0)
            {
                buttonCooldown -= Time.deltaTime;

                for (int i = 0; i < spikeTrapButtons.Length; i++)
                {
                    spikeTrapButtons[i].gameObject.transform.localScale = Vector3.Lerp(spikeTrapButtons[i].gameObject.transform.localScale, buttonOriginalScale, 0.015f); // Last value needs maths for proper timing
                }
            }
            else
            {
                for (int i = 0; i < spikeTrapButtons.Length; i++)
                {
                    spikeTrapButtons[i].gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                }

                buttonCooldown = buttonCooldownResetValue;
                trapIsReady = true;
            }

        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player" && trapIsActive)
            {
                other.GetComponent<Player>().Respawn();
            }
        }
    }
}
