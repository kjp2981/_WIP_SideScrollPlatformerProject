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

    // 마우스를 누른 시간을 체크 후 약공격인지 강곡격인지 판별 후 넘겨줌(불 값을로 넘겨주기)
    [field : SerializeField, Foldout("Attack Event")]
    public UnityEvent<bool> OnMeleeAttack { get; set; } // 근거리 공격
    [field : SerializeField, Foldout("Attack Event")]
    public UnityEvent<bool> OnRangeAttack { get; set; } // 원거리 공격

    #region 콤보 공격 체크 변수
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

    private Player player;

    private void Start()
    {
        player = Define.Player.GetComponent<Player>();

        meleeAttackCoolTimer = meleeAttackCoolTime;
        rangeAttackCoolTimer = rangeAttackCoolTime;
    }

    private void Update()
    {
        if (player.Death == false)
        {
            if (isAttack == false)
            {
                Movement();
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Jump();
                }
            }

            AttackInput();
        }
        meleeAttackCoolTimer += Time.deltaTime;
        rangeAttackCoolTimer += Time.deltaTime;
    }

    private void AttackInput()
    {
        if (meleeAttackCoolTimer >= meleeAttackCoolTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
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
