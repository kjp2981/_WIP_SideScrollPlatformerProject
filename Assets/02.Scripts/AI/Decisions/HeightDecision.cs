using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class HeightDecision : AIDecision
{
    [SerializeField]
    private float height;

    public override bool MakeADecision()
    {
        if(Mathf.Abs(transform.position.y - Define.Player.transform.position.y) < height)
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
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, height);
        Gizmos.color = Color.white;
    }
#endif
}
