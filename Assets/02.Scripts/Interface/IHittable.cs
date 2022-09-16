using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IHittable
{
    public bool IsEnemy { get; }
    public Vector2 HitPos { get; }

    public UnityEvent OnHit { get; set; }

    public void Damage(int damage, GameObject damageFactor, bool isKnockback = false, float knockPower = 0.2f);
}
