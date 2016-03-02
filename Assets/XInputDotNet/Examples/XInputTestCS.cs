//THIS PLUGIN IS TAKEN AS IS FROM https://github.com/speps/XInputDotNet
//and modded to be used within the game
using UnityEngine;
using XInputDotNetPure; // Required in C#

namespace Ip2
{

    public class XInputTestCS : MonoBehaviour
    {
        bool playerIndexSet = false;
        PlayerIndex playerIndex;
        GamePadState[] state;
        GamePadState[] prevState;

        //tre trap system will have to be reworked to be flexible for modifications
        //probably having an "activation" class referencing scripts we can add as we want
        //also they should contain a tag to be easily indexed by the input manager
        FlameTrap[] m_redTraps;
        SpikeTrap[] m_yellowTraps;
        FallingBlocks[] m_greenTraps;
        ArrowTrap[] m_blueTraps;



        //adding references to the player GameObjects
        [SerializeField]
        public GameObject[] m_players;

        // Use this for initialization
        void Start()
        {
            // No need to initialize anything for the plugin
            state = new GamePadState[4];
            prevState = new GamePadState[4];

            //get all traps
            m_redTraps = FindObjectsOfType<FlameTrap>();
            m_yellowTraps = FindObjectsOfType<SpikeTrap>();
            m_greenTraps = FindObjectsOfType<FallingBlocks>();
            m_blueTraps = FindObjectsOfType<ArrowTrap>();

        }

        // Update is called once per frame
        void Update()
        {
            // Find a PlayerIndex
            if (!playerIndexSet)// || !prevState.IsConnected)
            {
                //state[4] = GamePad.GetState((PlayerIndex)0);
                for (int n = 0; n < 4; ++n)
                {
                    PlayerIndex testPlayerIndex = (PlayerIndex)n;
                    GamePadState testState = GamePad.GetState(testPlayerIndex);
                    if (testState.IsConnected)
                    {
                        Debug.Log(string.Format("GamePad found ", testPlayerIndex));
                        playerIndex = testPlayerIndex;
                        Debug.Log(testState.IsConnected);
                        state[n] = GamePad.GetState(testPlayerIndex);
                    }
                    Debug.Log(n);
                }
            }
            playerIndexSet = true;
            //Debug.Log("Player index set!");
            if (state[0].IsConnected)
                prevState[0] = state[0];
            if (state[1].IsConnected)
                prevState[1] = state[1];
            if (state[2].IsConnected)
                prevState[2] = state[2];
            if (state[3].IsConnected)
                prevState[3] = state[3];

            if (state[0].IsConnected)
                state[0] = GamePad.GetState((PlayerIndex)0); //getting the states for each player
            if (state[1].IsConnected)
                state[1] = GamePad.GetState((PlayerIndex)1);
            if (state[2].IsConnected)
                state[2] = GamePad.GetState((PlayerIndex)2);
            if (state[3].IsConnected)
                state[3] = GamePad.GetState((PlayerIndex)3);

            int i = 0;
            //Debug.Log(i);
            foreach (GameObject player in m_players)
            {
                if (state[i].IsConnected)
                {
                    if (!player.GetComponent<Player>().m_isTheDictator)
                    {
                        //move
                        player.GetComponent<Player>().m_hAxis = state[i].ThumbSticks.Left.X;
                        //Debug.Log(state[i].ThumbSticks.Left.X);
                        //Debug.Log(state[i].Buttons.A);
                        //jump
                        if (state[i].Buttons.A == ButtonState.Pressed)
                        {
                            player.GetComponent<PlayerJump>().m_pressingJumpBtn = true;
                        }
                        else if (state[i].Buttons.A == ButtonState.Released)
                        {
                            player.GetComponent<PlayerJump>().m_pressingJumpBtn = false;
                        }
                    }
                    else //if we are the dictator, we activate the traps
                    {
                        //ACTIVATE GREEN TRAPS
                        if (state[i].Buttons.A == ButtonState.Pressed && prevState[i].Buttons.A == ButtonState.Released) //if we are tapping the button
                        {
                            foreach(FallingBlocks greenTrap in m_greenTraps)
                            {
                                greenTrap.Activate();
                            }
                        }
                        //ACTIVATE BLUE TRAPS
                        if (state[i].Buttons.X == ButtonState.Pressed && prevState[i].Buttons.X == ButtonState.Released) //if we are tapping the button
                        {
                            foreach (ArrowTrap blueTrap in m_blueTraps)
                            {
                                blueTrap.Activate();
                            }
                        }
                        //ACTIVATE RED TRAPS
                        if (state[i].Buttons.B == ButtonState.Pressed && prevState[i].Buttons.B == ButtonState.Released) //if we are tapping the button
                        {
                            foreach (FlameTrap redTrap in m_redTraps)
                            {
                                redTrap.Activate();
                            }
                        }
                        //ACTIVATE YELLOW TRAPS
                        if (state[i].Buttons.Y == ButtonState.Pressed && prevState[i].Buttons.Y == ButtonState.Released) //if we are tapping the button
                        {
                            foreach (SpikeTrap yellowTrap in m_yellowTraps)
                            {
                                yellowTrap.Activate();
                            }
                        }
                    }
                }
                i++;
            }
            //Debug.Log(i);

            // Detect if a button was pressed this frame
            //if (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed)
            //{
            //    //GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value, 1.0f);
            //}
            //// Detect if a button was released this frame
            //if (prevState.Buttons.A == ButtonState.Pressed && state.Buttons.A == ButtonState.Released)
            //{
            //    //GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            //}

            // Set vibration according to triggers
            //GamePad.SetVibration(playerIndex, state.Triggers.Left, state.Triggers.Right);

            // Make the current object turn
            //transform.localRotation *= Quaternion.Euler(0.0f, state.ThumbSticks.Left.X * 25.0f * Time.deltaTime, 0.0f);
        }

        //void OnGUI()
        //{
        //    string text = "Use left stick to turn the cube, hold A to change color\n";
        //    text += string.Format("IsConnected {0} Packet #{1}\n", state.IsConnected, state.PacketNumber);
        //    text += string.Format("\tTriggers {0} {1}\n", state.Triggers.Left, state.Triggers.Right);
        //    text += string.Format("\tD-Pad {0} {1} {2} {3}\n", state.DPad.Up, state.DPad.Right, state.DPad.Down, state.DPad.Left);
        //    text += string.Format("\tButtons Start {0} Back {1} Guide {2}\n", state.Buttons.Start, state.Buttons.Back, state.Buttons.Guide);
        //    text += string.Format("\tButtons LeftStick {0} RightStick {1} LeftShoulder {2} RightShoulder {3}\n", state.Buttons.LeftStick, state.Buttons.RightStick, state.Buttons.LeftShoulder, state.Buttons.RightShoulder);
        //    text += string.Format("\tButtons A {0} B {1} X {2} Y {3}\n", state.Buttons.A, state.Buttons.B, state.Buttons.X, state.Buttons.Y);
        //    text += string.Format("\tSticks Left {0} {1} Right {2} {3}\n", state.ThumbSticks.Left.X, state.ThumbSticks.Left.Y, state.ThumbSticks.Right.X, state.ThumbSticks.Right.Y);
        //    GUI.Label(new Rect(0, 0, Screen.width, Screen.height), text);
        //}
    }
}