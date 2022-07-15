using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentAnimation : MonoBehaviour
{
    private RaycastHit2D ray;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private readonly int hashIsMove = Animator.StringToHash("isMove");
    private readonly int hashJumpUp = Animator.StringToHash("jumpUp");
    private readonly int hashJumpDown = Animator.StringToHash("jumpDown");

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    public void SpriteFlipX(float value)
    {
        if(value > 0)
        {
            spriteRenderer.flipX = true;
        }
        else if(value < 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    public void MovementAnim(float value)
    {
        animator.SetBool(hashIsMove, value != 0);
    }

    public void JumpUp()
    {
        animator.SetTrigger(hashJumpUp);
    }

    public void JumpAnim(float yVelocity)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("JumpUp") == true)
        {
            animator.SetTrigger(hashJumpDown);
        }
    }
}
