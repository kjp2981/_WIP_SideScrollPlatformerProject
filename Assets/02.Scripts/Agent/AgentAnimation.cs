using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static Define;

public class AgentAnimation : MonoBehaviour
{
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
    private readonly int hashHit = Animator.StringToHash("Hit");
    private readonly int hashDie = Animator.StringToHash("Die");
    private readonly int hashDash = Animator.StringToHash("Dash");

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        agentAttack = GetComponentInParent<AgentAttack>();
    }

    public void SpriteFlipX(float value)
    {
        if(value > 0)
        {
            //spriteRenderer.flipX = true;
            Vector3 vec = transform.localScale;
            vec.x = -1;
            transform.localScale = vec;
        }
        else if(value < 0)
        {
            //spriteRenderer.flipX = false;
            Vector3 vec = transform.localScale;
            vec.x = 1;
            transform.localScale = vec;
        }
    }

    public void MovementAnim(float value)
    {
        animator.SetBool(hashIsMove, value != 0);
    }

    public void DashAnimation(bool isDash)
    {
        animator.SetBool(hashDash, isDash);
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
            //OnAttackUncomplete?.Invoke(); // 점프 후 착지시 약간의 멈움지기게 하기
        }
    }

    public void MeleeAttackAnim(bool isWeak)
    {
        OnAttackUncomplete?.Invoke();
        animator.SetFloat(hashIsWeak, isWeak == true ? 1 : 0);
        animator.SetFloat(hashMeleeCnt, agentAttack.MWCombo);
        animator.SetTrigger(hashIsMeleeAttack);
    }

    public void RangeAttackAnim(bool isWeak)
    {
        OnAttackUncomplete?.Invoke();
        animator.SetFloat(hashRangeCnt, 1);
        animator.SetFloat(hashIsWeak, isWeak == true ? 1 : 0);
        animator.SetTrigger(hashIsRangeAttack);
    }

    public void HitAnimation()
    {
        animator.SetTrigger(hashHit);
    }

    public void DieAnimation()
    {
        animator.SetTrigger(hashDie);
    }

    public void StopAttack()
    {
        Define.Player.GetComponent<AgentInput>().StopAttack();
    }
}
