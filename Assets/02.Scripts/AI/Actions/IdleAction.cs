using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAction : AIAction
{
    public enum State
    {
        Idle,
        Left,
        Right
    }

    private State nextState = State.Idle;
    private float transitionTime = 3f;
    private float timer;

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

        timer += Time.deltaTime;
        if (timer >= transitionTime)
        {
            nextState = (State)Random.Range(0, 4);
            timer = 0f;
            transitionTime = Random.Range(2f, 4f);
        }

        switch (nextState)
        {
            case State.Idle:
                _aiMovementData.direction = 0;
                //_aiMovementData.pointOfInterest = _enemyBrain.target.transform.position.x;
                break;
            case State.Left:
                _aiMovementData.direction = -1;
                _aiMovementData.pointOfInterest = -1;
                break;
            case State.Right:
                _aiMovementData.direction = 1;
                _aiMovementData.pointOfInterest = 1;
                break;
        }
        _enemyBrain.Move(_aiMovementData.direction, _aiMovementData.pointOfInterest);
    }
}
