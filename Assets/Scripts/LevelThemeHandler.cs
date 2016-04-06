using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Ip2
{
    public class LevelThemeHandler : MonoBehaviour
    {

        // Theme ID variable (Debug or can be reused?)
        public int themeNumber = 1;

        // Underwater Content:
        public Sprite underwaterBaseTile;
        public Sprite underwaterTrapTile;
        public Sprite underwaterBackground;

        // Sky Temple Content:
        public Sprite skyBaseTile;
        public Sprite skyTrapTile;
        public Sprite skyBackground;

        // Tomb Entrance Content:
        public Sprite tombBaseTile;
        public Sprite tombTrapTile;
        public Sprite tombBackground;

        // Colosseum Content:
        public Sprite colosseumBaseTile;
        public Sprite colosseumTrapTile;
        public Sprite colosseumBackground;

        // Objects we want to change
        GameObject[] levelTiles;
        List<GameObject> normalTiles;
        List<GameObject> trapTiles;
        GameObject gameBackground;

        GameObject[] crumbleTiles;
        GameObject[] arrowTiles;

        // Use this for initialization
        void Start()
        {
            //get the theme selected from the persistent data object
            PersistentData pData = FindObjectOfType(typeof(PersistentData)) as PersistentData;
            themeNumber = pData.m_themeSelected;

            normalTiles = new List<GameObject>();
            trapTiles = new List<GameObject>();

            crumbleTiles = GameObject.FindGameObjectsWithTag("Trap: Crumble");
            arrowTiles = GameObject.FindGameObjectsWithTag("Trap: Arrow");

            FetchTilesAndBackground();
            SwitchLevelSprites(themeNumber);
        }

        // Find the game objects we want and assign them to the variables
        public void FetchTilesAndBackground()
        {
            levelTiles = GameObject.FindGameObjectsWithTag("Surface");

            foreach (GameObject levelTile in levelTiles)
            {
                if (levelTile.name == "Tile: Normal")
                {
                    normalTiles.Add(levelTile);
                }

                if (levelTile.name == "Trap Tile: Spikes" || levelTile.name == "Trap Tile: Flame")
                {
                    trapTiles.Add(levelTile);
                }
            }

            foreach (GameObject crumbleTile in crumbleTiles)
            {
                trapTiles.Add(crumbleTile);
            }

            foreach (GameObject arrowTile in arrowTiles)
            {
                trapTiles.Add(arrowTile);
            }


            gameBackground = GameObject.Find("Background");

        }

        public void SwitchLevelSprites(int themeValue)
        {

            // Underwater Theme is 1
            if (themeValue == 1)
            {
                foreach (GameObject normalTile in normalTiles)
                {
                    normalTile.gameObject.GetComponent<SpriteRenderer>().sprite = underwaterBaseTile;
                }

                foreach (GameObject trapTile in trapTiles)
                {
                    trapTile.gameObject.GetComponent<SpriteRenderer>().sprite = underwaterTrapTile;
                }

                gameBackground.GetComponent<Image>().sprite = underwaterBackground;
            }

            // Tomb Theme is 2
            if (themeValue == 2)
            {
                foreach (GameObject normalTile in normalTiles)
                {
                    normalTile.gameObject.GetComponent<SpriteRenderer>().sprite = tombBaseTile;
                }

                foreach (GameObject trapTile in trapTiles)
                {
                    trapTile.gameObject.GetComponent<SpriteRenderer>().sprite = tombTrapTile;
                }

                gameBackground.GetComponent<Image>().sprite = tombBackground;
            }

            // Sky Temple Theme is 3
            if (themeValue == 3)
            {
                foreach (GameObject normalTile in normalTiles)
                {
                    normalTile.gameObject.GetComponent<SpriteRenderer>().sprite = skyBaseTile;
                }

                foreach (GameObject trapTile in trapTiles)
                {
                    trapTile.gameObject.GetComponent<SpriteRenderer>().sprite = skyTrapTile;
                }

                gameBackground.GetComponent<Image>().sprite = skyBackground;
            }

            // Colosseum Theme is 4
            if (themeValue == 4)
            {
                foreach (GameObject normalTile in normalTiles)
                {
                    normalTile.gameObject.GetComponent<SpriteRenderer>().sprite = colosseumBaseTile;
                }

                foreach (GameObject trapTile in trapTiles)
                {
                    trapTile.gameObject.GetComponent<SpriteRenderer>().sprite = colosseumTrapTile;
                }

                gameBackground.GetComponent<Image>().sprite = colosseumBackground;
            }
        }
    }
}
