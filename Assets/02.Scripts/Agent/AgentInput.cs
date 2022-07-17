using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;

public class AgentInput : MonoBehaviour, IAgentInput
{
    [field : SerializeField, Foldout("Movement Event")]
    public UnityEvent<float> OnMovementInput { get; set; }
    [field : SerializeField, Foldout("Movement Event")]
    public UnityEvent OnJumpInput { get; set; }

    // 마우스를 누른 시간을 체크 후 약공격인지 강곡격인지 판별 후 넘겨줌
    [field : SerializeField, Foldout("Attack Event")]
    public UnityEvent<float> OnMeleeAttack { get; set; } // 근거리 공격
    [field : SerializeField, Foldout("Attack Event")]
    public UnityEvent<float> OnRangeAttack { get; set; } // 원거리 공격

    #region 콤보 공격 체크 변수
    private float meleeAttackTimer = 0f;
    private float rangeAttackTimer = 0f;
    #endregion

    private void Update()
    {
        Movement();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (Input.GetMouseButtonDown(0))
        {
            meleeAttackTimer += Time.deltaTime;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            MeleeAttack(meleeAttackTimer);
            meleeAttackTimer = 0f;
        }
    }

    public void Movement()
    {
        OnMovementInput?.Invoke(Input.GetAxisRaw("Horizontal"));
    }

    public void Jump()
    {
        OnJumpInput?.Invoke();
    }

    public void MeleeAttack(float timer)
    {
        OnMeleeAttack?.Invoke(timer);
    }

    public void RangeAttack(float timer)
    {
        OnRangeAttack?.Invoke(timer);
    }
}
