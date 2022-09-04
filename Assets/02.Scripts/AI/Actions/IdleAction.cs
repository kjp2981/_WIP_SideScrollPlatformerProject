using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAction : AIAction
{
    public override void OnStateEnter()
    {
        Debug.Log("Enter the IdleState");
    }

    public override void OnStateLeave()
    {
        Debug.Log("Leave the IdleState");
    }

    public override void TakeAction()
    {
        _aiActionData.arrived = false;

        _aiMovementData.direction = 0;
        //_aiMovementData.pointOfInterest = _enemyBrain.target.transform.position.x;
        _enemyBrain.Move(_aiMovementData.direction, _aiMovementData.pointOfInterest);
    }
}
