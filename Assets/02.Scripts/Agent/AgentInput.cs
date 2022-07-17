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

    // ���콺�� ���� �ð��� üũ �� ��������� ��������� �Ǻ� �� �Ѱ���(�� ������ �Ѱ��ֱ�)
    [field : SerializeField, Foldout("Attack Event")]
    public UnityEvent<bool> OnMeleeAttack { get; set; } // �ٰŸ� ����
    [field : SerializeField, Foldout("Attack Event")]
    public UnityEvent<bool> OnRangeAttack { get; set; } // ���Ÿ� ����

    #region �޺� ���� üũ ����
    private float meleeAttackTimer = 0f;
    private float rangeAttackTimer = 0f;

    private readonly float meleeStrongAttackTime = 0.7f;
    private readonly float rangeStrongAttackTime = 0.7f;

    private bool isAttack = false;
    #endregion

    private void Update()
    {
        if (isAttack == false)
        {
            Movement();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (isAttack == false)
                isAttack = true;
        }
        if (Input.GetMouseButton(0))
        {
            if(isAttack == true)
                meleeAttackTimer += Time.deltaTime;
        }
        if (Input.GetMouseButtonUp(0) || meleeAttackTimer >= meleeStrongAttackTime)
        {
            if (isAttack == true)
            {
                if (meleeAttackTimer <= meleeStrongAttackTime)
                {
                    MeleeAttack(true);
                }
                else if (meleeAttackTimer > meleeStrongAttackTime)
                {
                    MeleeAttack(false);
                }
                meleeAttackTimer = 0f;
                //isAttack = false;
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if(isAttack == false)
                isAttack = true;
        }
        if (Input.GetMouseButton(1))
        {
            if(isAttack == true)
                rangeAttackTimer += Time.deltaTime;
        }
        if (Input.GetMouseButtonUp(1) || rangeAttackTimer >= rangeStrongAttackTime)
        {
            if (isAttack == true)
            {
                if (rangeAttackTimer <= rangeStrongAttackTime)
                {
                    RangeAttack(true);
                }
                else if (rangeAttackTimer > rangeStrongAttackTime)
                {
                    RangeAttack(false);
                }
                rangeAttackTimer = 0f;
                //isAttack = false;
            }
        }
    }

    public void StopAttack()
    {
        isAttack = false;
    }

    public void Movement()
    {
        OnMovementInput?.Invoke(Input.GetAxisRaw("Horizontal"));
    }

    public void Jump()
    {
        OnJumpInput?.Invoke();
    }

    public void MeleeAttack(bool isWeak)
    {
        OnMeleeAttack?.Invoke(isWeak);
    }

    public void RangeAttack(bool isWeak)
    {
        OnRangeAttack?.Invoke(isWeak);
    }
}
