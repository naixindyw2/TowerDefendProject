using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.Battle
{
    public class TouchInput
    {
        private float joystickX;
        private float joystickY;

        public void SetJoystickDir(float x, float y)
        {
            this.joystickX = x;
            this.joystickY = y;
        }

        public void Update()
        {

        }
    }
}