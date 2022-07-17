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
            Debug.Log("�ٰŸ� �����");
        }
        else
        {
            Debug.Log("�ٰŸ� ������");
        }
    }

    public void RangeAttack(bool isWeak)
    {
        if (isWeak == true)
        {
            Debug.Log("���Ÿ� �����");
        }
        else
        {
            Debug.Log("���Ÿ� ������");
        }
    }
}
