using UnityEngine;
using System.Collections;

namespace Ip2
{
    public class ArrowBlock : MonoBehaviour
    {

        public GameObject upArrowSprite;
        public GameObject downArrowSprite;
        public GameObject leftArrowSprite;
        public GameObject rightArrowSprite;

        public GameObject arrowObject;
        Vector3 initialArrowPosition;
        public bool arrowShouldBeLaunched = false;

        public int directionNumber = 1;



        // Use this for initialization
        void Start()
        {
            initialArrowPosition = arrowObject.transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            // Up (North)
            if (directionNumber == 1)
            {
                upArrowSprite.gameObject.SetActive(true);
                downArrowSprite.gameObject.SetActive(false);
                leftArrowSprite.gameObject.SetActive(false);
                rightArrowSprite.gameObject.SetActive(false);
                arrowObject.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
            }

            // Right (East)
            if (directionNumber == 2)
            {
                upArrowSprite.gameObject.SetActive(false);
                downArrowSprite.gameObject.SetActive(false);
                leftArrowSprite.gameObject.SetActive(false);
                rightArrowSprite.gameObject.SetActive(true);
                arrowObject.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 270f));
            }

            // Down (South)
            if (directionNumber == 3)
            {
                upArrowSprite.gameObject.SetActive(false);
                downArrowSprite.gameObject.SetActive(true);
                leftArrowSprite.gameObject.SetActive(false);
                rightArrowSprite.gameObject.SetActive(false);
                arrowObject.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 180f));
            }

            // Left (West)
            if (directionNumber == 4)
            {
                upArrowSprite.gameObject.SetActive(false);
                downArrowSprite.gameObject.SetActive(false);
                leftArrowSprite.gameObject.SetActive(true);
                rightArrowSprite.gameObject.SetActive(false);
                arrowObject.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 90f));
            }

            if (arrowShouldBeLaunched)
            {
                arrowObject.gameObject.transform.Translate(Vector3.up * (Time.deltaTime * 15), Space.Self);
            }
        }

        public void LaunchArrow()
        {
            arrowObject.gameObject.transform.position = initialArrowPosition;
            arrowObject.SetActive(true);
            arrowShouldBeLaunched = true;
            arrowObject.gameObject.GetComponent<Arrow>().LaunchArrow();
        }

        public void ResetArrow()
        {
            arrowObject.gameObject.transform.position = initialArrowPosition;
            arrowShouldBeLaunched = false;
            arrowObject.gameObject.GetComponent<Arrow>().ResetArrow();
            arrowObject.SetActive(false);
        }
    }
}