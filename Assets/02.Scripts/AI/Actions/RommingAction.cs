using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using static Define;

public class RommingAction : AIAction
{
    private AIState idleState;

    private List<Vector3> rommingPosList = new List<Vector3>();
    private Vector3 nextPos;
    private int nextPosIdx;

    [SerializeField, MinMaxSlider(0, 5)]
    private Vector2 rommingDistance;

    private void Start()
    {
        idleState = transform.parent.Find("IdleState").GetComponent<AIState>();

        nextPosIdx = 0;
    }

    private void OnEnable()
    {
        if (rommingPosList.Count > 0) return;

        rommingPosList.Add(transform.position - Vector3.left * GetRandomOffset());
        rommingPosList.Add(transform.position - Vector3.right * GetRandomOffset());
    }

    private int GetRandomOffset()
    {
        return Random.Range((int)rommingDistance.x, (int)rommingDistance.y + 1);
    }

    public override void OnStateEnter()
    {
        if(rommingPosList.Count < 1)
        {
            rommingPosList.Add(transform.position - Vector3.left * GetRandomOffset());
            rommingPosList.Add(transform.position - Vector3.right * GetRandomOffset());
        }

        nextPos = rommingPosList[nextPosIdx];

        nextPosIdx = (nextPosIdx + 1) % rommingPosList.Count;
    }

    public override void OnStateLeave()
    {
        
    }

    public override void TakeAction()
    {
        _aiMovementData.direction = nextPos.x < transform.position.x ? -1 : 1;
        _aiMovementData.pointOfInterest = nextPos.x < transform.position.x ? -1 : 1;

        _enemyBrain.Move(_aiMovementData.direction, _aiMovementData.pointOfInterest);

        if (_enemyBrain.GetPos().y >= nextPos.y - 0.5f && _enemyBrain.GetPos().y <= nextPos.y + 0.5f)
        {
            if (_enemyBrain.GetPos().x >= nextPos.x - 0.5f && _enemyBrain.GetPos().x <= nextPos.x + 0.5f)
            {
                _enemyBrain.ChangeState(idleState);
            }
        }
    }
}