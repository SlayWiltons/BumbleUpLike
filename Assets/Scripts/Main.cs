using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Characters;

namespace SceneEnv
{
    public class Main : MonoBehaviour
    {
        public static Main self = null;
        [SerializeField] private Player player;
        GameStates stateGame;

        public Player Player
        {
            get { return player; }
            set
            {
                if (value)
                    player = value;
            }
        }

        public GameStates StateGame
        {
            get { return stateGame; }
        }


        void Awake()
        {
            if (self == null)
                self = this;
        }

        void Update()
        {
            if (CheckDefeat())
            {
                stateGame = GameStates.GameOver;
                return;
            }
        }

        bool CheckDefeat()
        {
            return !player.isActiveAndEnabled;
        }
    }
}
