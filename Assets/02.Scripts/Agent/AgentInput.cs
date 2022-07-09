using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AgentInput : MonoBehaviour, IAgentInput
{
    [field : SerializeField]
    public UnityEvent<float> OnMovementInput { get; set; }
    [field : SerializeField]
    public UnityEvent OnMeleeAttack { get; set; }
    [field : SerializeField]
    public UnityEvent OnRangeAttack { get; set; }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        OnMovementInput?.Invoke(Input.GetAxisRaw("Horizontal"));
    }
}
