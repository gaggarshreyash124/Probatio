using UnityEngine;

public class JumpTest : MonoBehaviour
{
    public Animator animator;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Jump");
        }
    }
}
