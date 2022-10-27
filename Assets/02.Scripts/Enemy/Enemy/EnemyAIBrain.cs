using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;
using DG.Tweening;
using static Define;

public class EnemyAIBrain : MonoBehaviour, IAgentInput
{
    [field : SerializeField, Foldout("Movement Event")]
    public UnityEvent<float> OnMovementInput { get; set; }
    [field: SerializeField, Foldout("Movement Event")]
    public UnityEvent<float> OnJumpInput { get; set; }
    [field : SerializeField, Foldout("Movement Event")]
    public UnityEvent<float> OnDashInput { get; set; }
    [field: SerializeField, Foldout("Attack Event")]
    public UnityEvent<bool> OnMeleeAttack { get; set; }
    [field: SerializeField, Foldout("Attack Event")]
    public UnityEvent<bool> OnRangeAttack { get; set; }

    public UnityEvent<float> OnLook;

    [SerializeField] protected AIState _currentState;
    private protected AIState _beforeState;

    public Transform target;
    public Transform basePosition = null;

    private AIActionData _aiActionData;
    public AIActionData AIActionData => _aiActionData;

    private void OnEnable()
    {

    }

    protected virtual void Awake()
    {
        _aiActionData = transform.Find("AI").GetComponent<AIActionData>();
    }

    public void SetAttackState(bool state)
    {
        _aiActionData.attack = state;
    }

    private void Start()
    {
        target = Define.Player.transform;
    }

    public void Attack()
    {
        OnMeleeAttack?.Invoke(true);
    }

    public void Move(float moveDirection, float targetPosition)
    {
        OnMovementInput?.Invoke(moveDirection);
        if(_aiActionData.attack == false)
        {
            OnLook?.Invoke(targetPosition);
        }
    }

    public void ChangeState(AIState state)
    {
        if (_currentState == state) return;

        _beforeState = _currentState;
        _beforeState.GetComponent<AIAction>().OnStateLeave();
        _currentState = state; //���� ����
        _currentState.GetComponent<AIAction>().OnStateEnter();
    }

    public AIState GetState()
    {
        return _currentState;
    }

    public AIState GetBeforeState()
    {
        return _beforeState;
    }

    public Vector3 GetPos()
    {
        return this.transform.position;
    }

    protected virtual void Update()
    {
        if (target == null)
        {
            OnMovementInput?.Invoke(0); //Ÿ�� ������ �����
        }
        else
        {
            _currentState.UpdateState();
        }
    }
}
