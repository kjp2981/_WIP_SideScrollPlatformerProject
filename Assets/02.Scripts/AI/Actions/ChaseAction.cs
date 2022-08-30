using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAction : AIAction
{
    public override void TakeAction()
    {
        _aiActionData.attack = false;
        if(_aiActionData.arrived == false)
        {
            _aiActionData.arrived = true;
        }

        _aiMovementData.direction = _enemyBrain.target.transform.position.x < transform.position.x ? -1 : 1;
        _aiMovementData.pointOfInterest = _enemyBrain.target.transform.position.x < transform.position.x ? -1 : 1;
        _enemyBrain.Move(_aiMovementData.direction, _aiMovementData.pointOfInterest);
    }
}
