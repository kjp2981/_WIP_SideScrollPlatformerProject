using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static Define;
using NaughtyAttributes;
using UnityEngine.EventSystems;

public class AgentInput : MonoBehaviour, IAgentInput
{
    #region Input
    [field: SerializeField, Foldout("Movement Event")]
    public UnityEvent<float> OnMovementInput { get; set; }
    [field: SerializeField, Foldout("Movement Event")]
    public UnityEvent<float> OnJumpInput { get; set; }
    [field: SerializeField, Foldout("Movement Event")]
    public UnityEvent<float> OnDashInput { get; set; }

    // 마우스를 누른 시간을 체크 후 약공격인지 강곡격인지 판별 후 넘겨줌(불 값을로 넘겨주기)
    [field: SerializeField, Foldout("Attack Event")]
    public UnityEvent<bool> OnMeleeAttack { get; set; } // 근거리 공격
    [field: SerializeField, Foldout("Attack Event")]
    public UnityEvent<bool> OnRangeAttack { get; set; } // 원거리 공격

    [Foldout("Skill Event")]
    public UnityEvent OnLeftSkill;
    [Foldout("Skill Event")]
    public UnityEvent OnRightSkill;
    [Foldout("Skill Event")]
    public UnityEvent OnWeaponSkill;
    #endregion

    #region 콤보 공격 체크 변수
    private float meleeAttackTimer = 0f;
    private float rangeAttackTimer = 0f;

    private readonly float meleeStrongAttackTime = 0.5f;
    private readonly float rangeStrongAttackTime = 0.5f;

    private bool isAttack = false;
    public bool IsAttack => isAttack;



    private float meleeAttackCoolTimer = 0f;
    private float rangeAttackCoolTimer = 0f;

    private float meleeAttackCoolTime = 0.2f;
    private float rangeAttackCoolTime = 1f;
    #endregion

    #region 대쉬 체크 변수
    [SerializeField]
    private float dashCoolTime = 3f;
    private float dashTimer = 0f;

    private float dashTime = 0f;
    #endregion

    #region 점프 체크 변수
    private float jumpTime = 0f;
    #endregion

    private Player player;

    private int attackCnt = 0;

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
                    jumpTime += Time.deltaTime;
                }
                else if (Input.GetKey(KeyCode.Space))
                {
                    jumpTime += Time.deltaTime;

                    if (jumpTime >= 0.5f)
                    {
                        Jump(jumpTime);
                        jumpTime = 0;
                    }
                }
                else if (Input.GetKeyUp(KeyCode.Space))
                {
                    Jump(jumpTime);
                    jumpTime = 0;
                }

                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    Dash(1f);
                }
                //else if (Input.GetKey(KeyCode.LeftShift))
                //{
                //    dashTime += Time.deltaTime;

                //    if(dashTime >= 0.7f)
                //    {
                //        Dash(dashTime);
                //        dashTime = 0f;
                //    }
                //}
                //else if (Input.GetKeyUp(KeyCode.LeftShift))
                //{
                //    Dash(dashTime);
                //    dashTime = 0f;
                //}

                if (Input.GetKeyDown(KeyCode.Q))
                {
                    OnLeftSkill?.Invoke();
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    OnRightSkill?.Invoke();
                }
                if (Input.GetKeyDown(KeyCode.R))
                {
                    OnWeaponSkill?.Invoke();
                }
            }
            else if (isAttack == true)
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
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (attackCnt < 3)
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
                            ++attackCnt;
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
        }
        else
        {
            if (meleeAttackTimer >= 1f)
            {
                attackCnt = 0;
                meleeAttackTimer = 0;
            }
            else
            {
                meleeAttackTimer += Time.deltaTime;
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

    public void Jump(float value)
    {
        OnJumpInput?.Invoke(value);
    }

    public void Dash(float dashTime)
    {
        if (dashTimer >= dashCoolTime)
        {
            OnDashInput?.Invoke(dashTime);
            dashTimer = 0f;
            this.dashTime = 0f;
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
