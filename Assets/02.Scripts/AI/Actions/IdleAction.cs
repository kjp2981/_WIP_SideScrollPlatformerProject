using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using Random = UnityEngine.Random;

public class IdleAction : AIAction
{
    public enum State
    {
        Idle,
        Left,
        Right
    }

    private State nextState = State.Idle;
    [SerializeField]
    private float transitionTime = 1.5f;
    private float timer;

    private bool isMove = false;

    public override void OnStateEnter()
    {
        
    }

    public override void OnStateLeave()
    {
        
    }

    public override void TakeAction()
    {
        _aiActionData.arrived = false;

        if(Mathf.Abs(transform.position.x - _enemyBrain.InitPos.x) > 5) // 돌아오긴하다 아래 else문의 작동을 방해한다.
        {
            if (isMove == true) return;
            _aiMovementData.direction = transform.position.x < _enemyBrain.InitPos.x ? 1 : -1;
            _aiMovementData.pointOfInterest = transform.position.x < _enemyBrain.InitPos.x ? 1 : -1;
            _enemyBrain.Move(_aiMovementData.direction, _aiMovementData.pointOfInterest);
        }
        else
        {
            isMove = true;
            timer += Time.deltaTime;
            if (timer >= transitionTime)
            {
                List<State> list = new List<State>();
                if(list.Count <= 0)
                {
                    for(int i = 0; i < 3; i++)
                    {
                        list.Add((State)i);
                    }

                    for (int i = 0; i < 10; i++)
                    {
                        int left, right;
                        left = Random.Range(0, list.Count);
                        right = Random.Range(0, list.Count);

                        State temp = list[left];
                        list[left] = list[right];
                        list[right] = temp;
                    }
                }

                nextState = list[0];
                list.RemoveAt(0);
                timer = 0f;
            }

            switch (nextState)
            {
                case State.Idle:
                    _aiMovementData.direction = 0;
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
            isMove = false;
        }
    }
}
