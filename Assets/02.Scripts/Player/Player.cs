using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IHittable
{
    [SerializeField]
    private StatusDataSO status;

    public StatusDataSO Status => status;

    public bool IsEnemy => false;

    public Vector2 HitPos { get; private set; }

    public void Damage(int damage, GameObject damageFactor)
    {
        Debug.Log("데미지 입었다!");
    }
}
