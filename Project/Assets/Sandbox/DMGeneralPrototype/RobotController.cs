using UnityEngine;

public class RobotController : MonoBehaviour
{
    public Animator animator;

    public void MoveDown()
    {
        animator.SetBool("ifDown", true);
    }

    public void MoveUp()
    {
        animator.SetBool("ifUp", true);
    }

    public void Win()
    {

    }

    public void Lose()
    {

    }

    public void ResetMove()
    {
        animator.SetBool("ifDown", false);
        animator.SetBool("ifUp", false);
    }
}
