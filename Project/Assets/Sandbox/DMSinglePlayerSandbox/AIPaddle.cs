using UnityEngine;

namespace DMGeneralPrototype
{
    public class AIPaddle : Paddle
    {
        public Transform ballTransform;

        public float min = -1.0f;
        public float max = 1.0f;

        float difficulty;

        protected override void OnStart()
        {
            difficulty = Random.Range(min, max);

            UpdatePrefabs();
        }

        protected override void OnUpdate()
        {
            Debugger.Instance.DebugInfo("Difficulty", difficulty.ToString());
            difficulty -= 0.01f * Time.deltaTime;

            if (ballTransform.transform.position.x > difficulty && ballTransform.transform.position.z > transform.position.z)
            {
                Move(1);
                animator.SetBool(flipDir ? "ifUp" : "ifDown", true);
            }
            else
            {
                animator.SetBool(flipDir ? "ifUp" : "ifDown", false);
            }

            if (ballTransform.transform.position.x > difficulty && ballTransform.transform.position.z < transform.position.z)
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