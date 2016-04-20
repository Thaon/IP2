using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace Ip2
{
    class GameModeSelector : MonoBehaviour
    {
        [SerializeField]
        List<string> m_scenesMG1;
        [SerializeField]
        List<string> m_scenesMG2;

        private List<List<string>> m_totalScenes;

        private List<List<string>> m_selectedScenes;

        public GameObject m_UIControls;

        public Text m_LVUp, m_LVRight, m_LVDown, m_LVLeft;

        void Start()
        {

            m_UIControls = GameObject.Find("UI Updater");

            m_totalScenes = new List<List<string>>();
            m_selectedScenes = new List<List<string>>();
            m_totalScenes.Add(m_scenesMG1);
            m_totalScenes.Add(m_scenesMG2);

            //initialise the selected scenes list
            foreach (List<string> list in m_totalScenes)
            {
                m_selectedScenes.Add(new List<string>());
            }

            //get a selection of scenes according to the minigame
            int setToSelect = Random.Range(0, m_totalScenes.Count);
            List<string> gamemode = m_totalScenes[setToSelect];
            string selection1 = GetRandomScene(gamemode, setToSelect); //up
            string selection2 = GetRandomScene(gamemode, setToSelect); //right
            string selection3 = GetRandomScene(gamemode, setToSelect); //down
            string selection4 = GetRandomScene(gamemode, setToSelect); //left

            //update the UI
            m_LVUp.text = selection1;
            m_LVRight.text = selection2;
            m_LVDown.text = selection3;
            m_LVLeft.text = selection4;

        }

        public string GetRandomScene(List<string> scenes, int set)
        {
            int selectedScene = (int)Random.Range(0, scenes.Count - 1);
            //print(selectedScene);
            //print(set);
            string scene = scenes[selectedScene];
            //print(scene);
            //m_selectedScenes[set].Add(scene);
            //print(m_selectedScenes.Count);
            scenes.Remove(scene);
            if (scenes.Count == 0) //if scenes are finished, we fill back the list
            {
                foreach (string str in m_selectedScenes[set])
                {
                    scenes.Add(str);
                    m_selectedScenes[set].RemoveAt(0);
                }
            }
            return scene;
        }
    }
}
