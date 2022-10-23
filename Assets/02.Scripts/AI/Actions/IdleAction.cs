using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using Random = UnityEngine.Random;

public class IdleAction : AIAction
{
    private AIState rommingState;

    [SerializeField]
    private float transitionTime = 1.5f;
    private float timer;

    private void Start()
    {
        rommingState = transform.parent.Find("RommingState").GetComponent<AIState>();
    }

    public override void OnStateEnter()
    {
        timer = 0f;
    }

    public override void OnStateLeave()
    {
        timer = 0f;
    }

    public override void TakeAction()
    {
        _aiActionData.arrived = false;

        timer += Time.deltaTime;

        if(timer >= transitionTime)
        {
            _enemyBrain.ChangeState(rommingState);
        }

        _aiMovementData.direction = 0;
        _enemyBrain.Move(_aiMovementData.direction, _aiMovementData.pointOfInterest);
    }
}
