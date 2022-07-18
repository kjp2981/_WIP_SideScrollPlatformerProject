using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static Define;

public class AgentAnimation : MonoBehaviour
{
    private RaycastHit2D ray;

    public UnityEvent OnAttackUncomplete = null;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private AgentAttack agentAttack;

    private readonly int hashIsMove = Animator.StringToHash("isMove");
    private readonly int hashJumpUp = Animator.StringToHash("jumpUp");
    private readonly int hashJumpDown = Animator.StringToHash("jumpDown");
    private readonly int hashIsMeleeAttack = Animator.StringToHash("isMeleeAttack");
    private readonly int hashIsRangeAttack = Animator.StringToHash("isRangeAttack");
    private readonly int hashMeleeCnt = Animator.StringToHash("meleeCnt");
    private readonly int hashRangeCnt = Animator.StringToHash("rangeCnt");
    private readonly int hashIsWeak = Animator.StringToHash("isWeak");

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        agentAttack = Player.GetComponent<AgentAttack>();
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

    public void MeleeAttackAnim(bool isWeak)
    {
        OnAttackUncomplete?.Invoke();
        animator.SetFloat(hashIsWeak, isWeak == true ? 1 : 0);
        animator.SetFloat(hashMeleeCnt, agentAttack.Combo);
        animator.SetTrigger(hashIsMeleeAttack);
    }

    public void RangeAttackAnim(bool isWeak)
    {
        OnAttackUncomplete?.Invoke();
        animator.SetFloat(hashRangeCnt, 1);
        animator.SetFloat(hashIsWeak, isWeak == true ? 1 : 0);
        animator.SetTrigger(hashIsRangeAttack);
    }

    public void StopAttack()
    {
        Player.GetComponent<AgentInput>().StopAttack();
    }
}
