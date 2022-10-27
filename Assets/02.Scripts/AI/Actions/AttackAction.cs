using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : AIAction
{
    public override void OnStateEnter()
    {
        _aiActionData.attack = true;
        _aiActionData.arrived = false;
    }

    public override void OnStateLeave()
    {
        
    }

    public override void TakeAction()
    {
        _aiMovementData.direction = 0f;

        if(_aiActionData.attack == true)
        {
            _aiMovementData.pointOfInterest = _enemyBrain.target.transform.position.x < transform.position.x ? -1 : 1;
            _enemyBrain.Attack(); // �����ϸ鼭 �ణ�� ������ �����ؾ���
            _enemyBrain.Move(_aiMovementData.direction, _aiMovementData.pointOfInterest);
        }

        //_enemyBrain.Move(_aiMovementData.direction, _aiMovementData.pointOfInterest);
    }
}
