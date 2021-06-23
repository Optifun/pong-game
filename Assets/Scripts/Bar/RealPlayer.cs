using System;
using UnityEngine;

namespace Bar
{
    public class RealPlayer : BasePlayer
    {
        private void FixedUpdate()
        {
            if (Input.anyKey)
            {
                float direction = Input.GetAxis($"Player{playerNum} Input");
                if (Math.Abs(direction) < 0.1f)
                    return;
                Move(direction);
            }
        }
    }
}