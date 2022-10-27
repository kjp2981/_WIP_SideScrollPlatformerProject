using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotBetweenObstacleDecision : AIDecision
{
    RaycastHit2D ray;

    [SerializeField]
    private LayerMask obstacleLayer;
    public override bool MakeADecision()
    {
        Vector2 dir = _enemyBrain.target.transform.position - this.transform.position;
        float distance = Vector2.Distance(_enemyBrain.target.transform.position, this.transform.position);
        ray = Physics2D.Raycast(transform.position, dir, distance, obstacleLayer);

        return ray.collider == null;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, _enemyBrain.target.transform.position);
    }
#endif
}
