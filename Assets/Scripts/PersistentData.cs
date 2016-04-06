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
        public int m_roundNumnber = 0;

        public int m_winningPlayer = -1;
        public int m_dictator = -1;

        public int m_scoreToWin = 2;

        public int player1Score = 0;
        public int player2Score = 0;
        public int player3Score = 0;
        public int player4Score = 0;

        Text m_p1score;
        Text m_p2score;
        Text m_p3score;
        Text m_p4score;
        Text m_roundNumberInfo;

        public GameState m_state;

        public int m_themeSelected;

        void Start()
        {
            DontDestroyOnLoad(this.gameObject);

            m_UIControls = GameObject.Find("UI Updater");

            m_p1score = GameObject.Find("Player Score: Red").GetComponent<Text>();
            m_p2score = GameObject.Find("Player Score: Blue").GetComponent<Text>();
            m_p3score = GameObject.Find("Player Score: Green").GetComponent<Text>();
            m_p4score = GameObject.Find("Player Score: Yellow").GetComponent<Text>();
            m_roundNumberInfo = GameObject.Find("Round Info: Round Number").GetComponent<Text>();

            if (Application.loadedLevel == 0)
            {
                m_state = GameState.menu;
                m_currentLevel = 0;
            }
            //if (Application.loadedLevel == 1)
            //{
            //    m_state = GameState.game;
            //    m_currentLevel = 1;
            //}
            //if (Application.loadedLevel == 2)
            //{
            //    m_state = GameState.scoreScreen;
            //    m_currentLevel = 2;
            //}
        }

        void Update()
        {
            if (m_UIControls != null)
            {
                if (m_state == GameState.game || m_state == GameState.dictatorSelection)
                {
                    m_UIControls.GetComponent<UpdateUI>().showMain = true;
                    m_UIControls.GetComponent<UpdateUI>().countdownActive = true;

                    m_p1score.text = player1Score.ToString();
                    m_p2score.text = player2Score.ToString();
                    m_p3score.text = player3Score.ToString();
                    m_p4score.text = player4Score.ToString();
                    m_roundNumberInfo.text = m_roundNumnber.ToString();

                    if (m_UIControls.GetComponent<UpdateUI>().roundCountdown <= 0.1f)
                    {
                        //set up a winner here
                        Application.LoadLevel(2);
                    }
                }

                if (m_state == GameState.roundFinished)
                {
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
                                player.GetComponent<Player>().SetXSpeed(1);
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

            if (Application.loadedLevel != m_currentLevel) //we check if the level has changed and we update variables accordingly
            {
                m_UIControls = GameObject.Find("UI Updater");

                m_p1score = GameObject.Find("Player Score: Red").GetComponent<Text>();
                m_p2score = GameObject.Find("Player Score: Blue").GetComponent<Text>();
                m_p3score = GameObject.Find("Player Score: Green").GetComponent<Text>();
                m_p4score = GameObject.Find("Player Score: Yellow").GetComponent<Text>();

                m_currentLevel = Application.loadedLevel;
                //print("setting dictator from scene transition");
                SetDictator(m_dictator);
                if (Application.loadedLevel == 0) //we are in the menu, no dictators here
                {
                    m_dictator = -1;
                    m_state = GameState.menu;
                }
                //if (Application.loadedLevel == 1)
                //{
                //    m_state = GameState.game;
                //    m_currentLevel = 1;
                //}
                //if (Application.loadedLevel == 2)
                //{
                //    m_state = GameState.scoreScreen;
                //    m_currentLevel = 2;
                //}
            }
        }

        public void ResetData()
        {
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
