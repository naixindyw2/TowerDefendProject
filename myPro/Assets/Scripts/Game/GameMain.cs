using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Battle;

namespace Game
{
    public class GameMain : MonoBehaviour
    {
        void Start()
        {            
            GameBattleManager.Instance.Init();
            InputManager.Instance.Init();
            TimelineManager.Instance.Init();
            TimelineManager.Instance.Start();
        }

        // Update is called once per frame
        void Update()
        {
            TimelineManager.Instance.Update();
        }
    }
}

