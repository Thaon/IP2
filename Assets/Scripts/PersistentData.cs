using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Ip2
{
    public enum GameState { menu, game, roundFinished, dictatorSelection, modeSelection, scoreScreen};

    public class PersistentData : MonoBehaviour
    {
        public GameObject m_UIControls;

        int m_currentLevel;
        int m_previousLevel;
        public int m_roundNumnber = 0;

        public int m_winningPlayer = -1;
        public int m_dictator = -1;

        public int m_scoreToWin = 5;

        public int player1Score = 0;
        public int player2Score = 0;
        public int player3Score = 0;
        public int player4Score = 0;

        public Text m_p1score;
        public Text m_p2score;
        public Text m_p3score;
        public Text m_p4score;
        public Text m_roundNumberInfo;

        [SerializeField]
        public GameState m_state;

        public int m_themeSelected;

        void Start()
        {
            DontDestroyOnLoad(this.gameObject);

            m_state = GameState.menu;
            m_currentLevel = 0;
            m_previousLevel = m_currentLevel;
        }

        void Update()
        {
            //we refresh the referendes here every time we switch scene
            m_currentLevel = Application.loadedLevel;

            if (m_previousLevel != m_currentLevel)
            {
                RefreshReferences();
                m_previousLevel = m_currentLevel;
            }

            if (m_UIControls != null)
            {
                if (m_state == GameState.game || m_state == GameState.dictatorSelection)
                {
                    m_UIControls.GetComponent<UpdateUI>().showMain = true;
                    m_UIControls.GetComponent<UpdateUI>().countdownActive = true;
                    m_UIControls.GetComponent<UpdateUI>().showChoiceScreen = false;                     

                    if (m_roundNumberInfo != null)
                        m_roundNumberInfo.text = "ROUND " + m_roundNumnber.ToString();

                }

                if (m_state == GameState.roundFinished)
                {
                    //print("Round finished");
                    m_UIControls.GetComponent<UpdateUI>().showMain = true;
                    m_UIControls.GetComponent<UpdateUI>().countdownActive = false;
                    m_UIControls.GetComponent<UpdateUI>().showRoundWinner = true;
                    GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                    if (players.Length > 0)
                    {
                        foreach (GameObject player in players)
                        {
                            //print(player.GetComponent<Player>().m_uniqueID);
                            if (player.GetComponent<Player>().m_isTheDictator == true)
                            {
                                player.GetComponent<Player>().SetXSpeed(0);
                            }
                        }
                    }

                }

                if (m_state == GameState.modeSelection)
                {
                    m_UIControls.GetComponent<UpdateUI>().showMain = false;
                    m_UIControls.GetComponent<UpdateUI>().countdownActive = false;
                    m_UIControls.GetComponent<UpdateUI>().showRoundWinner = false;
                    m_UIControls.GetComponent<UpdateUI>().showChoiceScreen = true;
                }
            }

            
        }

        void RefreshReferences() //call this when we switch scene
        {
            m_UIControls = GameObject.Find("UI Updater");

            if (GameObject.Find("Player Score: Red"))
            {
                m_p1score = GameObject.Find("Player Score: Red").GetComponent<Text>();
                m_p2score = GameObject.Find("Player Score: Blue").GetComponent<Text>();
                m_p3score = GameObject.Find("Player Score: Green").GetComponent<Text>();
                m_p4score = GameObject.Find("Player Score: Yellow").GetComponent<Text>();
            }
            if (GameObject.Find("Round Info: Round Number") != null)
                m_roundNumberInfo = GameObject.Find("Round Info: Round Number").GetComponent<Text>();
        }

        public void ResetData()
        {
            print("Resetting data");
            m_winningPlayer = -1;
            player1Score = 0;
            player2Score = 0;
            player3Score = 0;
            player4Score = 0;
        }

        public void LoadNewLevel(int scene, int theme)
        {
            print("Loading");
            m_themeSelected = theme;
            GameModeSelector mode = FindObjectOfType(typeof(GameModeSelector)) as GameModeSelector;
            m_roundNumnber++;
            switch (scene)
            {
                case 1:
                    Application.LoadLevel(mode.m_LVUp.text);
                break;

                case 2:
                    Application.LoadLevel(mode.m_LVRight.text);
                break;

                case 3:
                    Application.LoadLevel(mode.m_LVDown.text);
                break;

                case 4:
                    Application.LoadLevel(mode.m_LVLeft.text);
                break;
            }
            m_state = GameState.game;
        }

        public void SetDictator(int winningPlayer)
        {
            if (winningPlayer >= 0)
            {
                print("setting dictator from function");
                //first we set the dictator
                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                if (players.Length > 0)
                {
                    foreach (GameObject player in players)
                    {
                        //print(player.GetComponent<Player>().m_uniqueID);
                        player.GetComponent<Player>().m_isTheDictator = false;
                    }
                    players[winningPlayer].GetComponent<Player>().m_isTheDictator = true;
                }
                m_dictator = winningPlayer;
            }
        }

        public int GetWinningPlayerNumber()
        {
            int max = 0;
            int player = -1;

            if (player1Score > max)
            {
                max = player1Score;
                player = 0;
            }
            if (player2Score > max)
            {
                max = player2Score;
                player = 1;
            }
            if (player3Score > max)
            {
                max = player3Score;
                player = 2;
            }
            if (player4Score > max)
            {
                max = player4Score;
                player = 3;
            }
            return player;
        }
    }
}
