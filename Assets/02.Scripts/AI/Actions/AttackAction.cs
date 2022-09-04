using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : AIAction
{
    public override void OnStateEnter()
    {
        Debug.Log("Enter the AttackState");
    }

    public override void OnStateLeave()
    {
        Debug.Log("Leave the AttackState");
    }

    public override void TakeAction()
    {
        _aiActionData.arrived = false;
        _aiActionData.attack = true;

        _aiMovementData.direction = 0f;

        if(_aiActionData.attack == true)
        {
            _aiMovementData.pointOfInterest = _enemyBrain.target.transform.position.x < transform.position.x ? -1 : 1;
            _enemyBrain.Attack(); // 공격하면서 약간시 움직임 수정해야함
        }

        _enemyBrain.Move(_aiMovementData.direction, _aiMovementData.pointOfInterest);
    }
}
