using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IAgentInput
{
    public UnityEvent<float> OnMovementInput { get; set; }

    public UnityEvent OnMeleeAttack { get; set; }

    public UnityEvent OnRangeAttack { get; set; }
}
