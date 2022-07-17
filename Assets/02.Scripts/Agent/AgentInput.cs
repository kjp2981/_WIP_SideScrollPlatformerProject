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

    // ���콺�� ���� �ð��� üũ �� ��������� ��������� �Ǻ� �� �Ѱ���
    [field : SerializeField, Foldout("Attack Event")]
    public UnityEvent<float> OnMeleeAttack { get; set; } // �ٰŸ� ����
    [field : SerializeField, Foldout("Attack Event")]
    public UnityEvent<float> OnRangeAttack { get; set; } // ���Ÿ� ����

    #region �޺� ���� üũ ����
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
