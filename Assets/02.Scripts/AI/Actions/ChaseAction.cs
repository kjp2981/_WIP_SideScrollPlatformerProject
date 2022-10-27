using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAction : AIAction
{
    EM em = null;
    public override void OnStateEnter()
    {
        //if (em != null) return;

        int chileCnt = this.transform.childCount;
        if (chileCnt >= 3) return;
        em = PoolManager.Instance.Pop("EM") as EM;
        Vector3 offset = _enemyBrain.target.transform.position.x < transform.position.x ? new Vector3(-0.5f, 0.8f, 0) : new Vector3(0.5f, 0.8f, 0);
        bool flip = _enemyBrain.target.transform.position.x < transform.position.x ? false : true;
        em.transform.position = transform.position + offset;
        em.transform.parent = this.transform;
        em.FlipX(flip);
    }

    public override void OnStateLeave()
    {
        
    }

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
