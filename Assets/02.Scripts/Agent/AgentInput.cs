using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static Define;
using NaughtyAttributes;

public class AgentInput : MonoBehaviour, IAgentInput
{
    [field : SerializeField, Foldout("Movement Event")]
    public UnityEvent<float> OnMovementInput { get; set; }
    [field : SerializeField, Foldout("Movement Event")]
    public UnityEvent OnJumpInput { get; set; }
    [field : SerializeField, Foldout("Movement Event")]
    public UnityEvent OnDashInput { get; set; }

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
    public bool IsAttack => isAttack;

    

    private float meleeAttackCoolTimer = 0f;
    private float rangeAttackCoolTimer = 0f;

    private readonly float meleeAttackCoolTime = 0.5f;
    private readonly float rangeAttackCoolTime = 1f;
    #endregion

    #region �뽬 üũ ����
    [SerializeField]
    private float dashCoolTime = 3f;
    private float dashTimer = 0f;
    #endregion

    private Player player;

    private void Start()
    {
        player = Define.Player.GetComponent<Player>();

        meleeAttackCoolTimer = meleeAttackCoolTime;
        rangeAttackCoolTimer = rangeAttackCoolTime;
    }

    private void Update()
    {
        if (Time.timeScale == 0) return;

        if (player.Death == false)
        {
            if (isAttack == false)
            {
                Movement(Input.GetAxisRaw("Horizontal"));
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Jump();
                }
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    Dash();
                }
            }
            else if(isAttack == true)
            {
                Movement(0);
            }

            AttackInput();
        }
        meleeAttackCoolTimer += Time.deltaTime;
        rangeAttackCoolTimer += Time.deltaTime;

        dashTimer += Time.deltaTime;
    }

    private void AttackInput()
    {
        if (meleeAttackCoolTimer >= meleeAttackCoolTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Movement(0);
                if (isAttack == false)
                    isAttack = true;
            }
            if (Input.GetMouseButton(0))
            {
                if (isAttack == true)
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
                    meleeAttackCoolTimer = 0f;
                    //isAttack = false;
                }
            }
        }

        if (rangeAttackCoolTimer >= rangeAttackCoolTime)
        {
            if (Input.GetMouseButtonDown(1))
            {
                
                if (isAttack == false)
                    isAttack = true;
            }
            if (Input.GetMouseButton(1))
            {
                if (isAttack == true)
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
                    rangeAttackCoolTimer = 0f;
                    //isAttack = false;
                }
            }
        }
    }

    public void StopAttack()
    {
        isAttack = false;
    }

    public void Movement(float value)
    {
        OnMovementInput?.Invoke(value);
    }

    public void Jump()
    {
        OnJumpInput?.Invoke();
    }

    public void Dash()
    {
        if(dashTimer >= dashCoolTime)
        {
            OnDashInput?.Invoke();
            dashTimer = 0f;
        }
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
