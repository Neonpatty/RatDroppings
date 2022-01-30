using UnityEngine;

namespace DMGeneralPrototype
{
    public enum Player
    {
        P1,
        P2
    }

    public class PlayerPaddle : Paddle
    {
        public Player player;

        [HideInInspector]
        public KeyCode upKey;
        [HideInInspector]
        public KeyCode downKey;

        protected override void OnStart()
        {
            upKey = KeybindManager.Instance.GetKeybind(player == Player.P1 ? "P1UpKey" : "P2UpKey");
            downKey = KeybindManager.Instance.GetKeybind(player == Player.P1 ? "P1DownKey" : "P2DownKey");
        }

        protected override void OnUpdate()
        {
            if (Input.GetKey(upKey) && transform.position.z < maxDistFromCenter)
            {
                Move(1);
                animator.SetBool(flipDir ? "ifUp" : "ifDown", true);
            }
            else
            {
                animator.SetBool(flipDir ? "ifUp" : "ifDown", false);
            }

            if (Input.GetKey(downKey) && transform.position.z > -maxDistFromCenter)
            {
                Move(-1);
                animator.SetBool(flipDir ? "ifDown" : "ifUp", true);
            }
            else
            {
                animator.SetBool(flipDir ? "ifDown" : "ifUp", false);
            }
        }
    }
}