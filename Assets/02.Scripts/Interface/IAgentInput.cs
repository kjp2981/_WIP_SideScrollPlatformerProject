using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IAgentInput
{
    public UnityEvent<Vector2> OnMovementInput { get; set; }


}
