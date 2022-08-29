using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScripts : MonoBehaviour, IHittable
{
    public bool IsEnemy => true;

    public Vector2 HitPos { get; private set; }

    public void Damage(int damage, GameObject damageFactor)
    {
        Debug.Log($"Damage : {damage}, damageFactor : {damageFactor}");
    }
}
