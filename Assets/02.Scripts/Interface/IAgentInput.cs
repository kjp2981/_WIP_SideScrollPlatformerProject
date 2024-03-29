using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IAgentInput
{
    public UnityEvent<float> OnMovementInput { get; set; }

    public UnityEvent<float> OnDashInput { get; set; }

    public UnityEvent<float> OnJumpInput { get; set; }

    public UnityEvent<bool> OnMeleeAttack { get; set; }

    public UnityEvent<bool> OnRangeAttack { get; set; }
}
