using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.Events;

public class AgentAttack : MonoBehaviour
{
    private BoxCollider2D hitCollider;
    [SerializeField, Layer]
    private int hitLayer;

    public void MeleeAttack(bool isWeak)
    {
        if(isWeak == true)
        {
            Debug.Log("근거리 약공격");
        }
        else
        {
            Debug.Log("근거리 강공격");
        }
    }

    public void RangeAttack(bool isWeak)
    {
        if (isWeak == true)
        {
            Debug.Log("원거리 약공격");
        }
        else
        {
            Debug.Log("원거리 강공격");
        }
    }
}
