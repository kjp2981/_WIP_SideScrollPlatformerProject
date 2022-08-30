using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class DistanceDecision : AIDecision
{
    [SerializeField]
    private float distance;

    public override bool MakeADecision()
    {
        if(Mathf.Abs(transform.position.x - Define.Player.transform.position.x) < distance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, distance);
        Gizmos.color = Color.white;
    }
#endif
}
