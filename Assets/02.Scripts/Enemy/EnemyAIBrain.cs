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
    public UnityEvent OnJumpInput { get; set; }
    [field : SerializeField, Foldout("Movement Event")]
    public UnityEvent OnDashInput { get; set; }
    [field: SerializeField, Foldout("Attack Event")]
    public UnityEvent<bool> OnMeleeAttack { get; set; }
    [field: SerializeField, Foldout("Attack Event")]
    public UnityEvent<bool> OnRangeAttack { get; set; }

    public UnityEvent<float> OnLook;

    [SerializeField] protected AIState _currentState;

    public Transform target;
    public Transform basePosition = null;

    private AIActionData _aiActionData;
    public AIActionData AIActionData => _aiActionData;

    private Transform initTransform;
    public Transform InitTransform => initTransform;

    private void OnEnable()
    {
        initTransform = this.transform;
        Debug.Log(initTransform.position);
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
        OnLook?.Invoke(targetPosition);
    }

    public void ChangeState(AIState state)
    {
        _currentState = state; //»óÅÂ º¯°æ
    }

    public AIState GetState()
    {
        return _currentState;
    }

    protected virtual void Update()
    {
        if (target == null)
        {
            OnMovementInput?.Invoke(0); //Å¸°Ù ¾øÀ¸¸é ¸ØÃç¶ó
        }
        else
        {
            _currentState.UpdateState();
        }
    }
}
