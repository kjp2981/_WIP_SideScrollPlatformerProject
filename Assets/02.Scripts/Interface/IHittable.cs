using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum DamageEffect
{
    Slash,
    Blood
}

public interface IHittable
{
    public bool IsEnemy { get; }
    public bool Death { get; }
    public Vector2 HitPos { get; }

    public UnityEvent OnHit { get; set; }

    public UnityEvent OnDie { get; set; }

    public void Damage(int damage, GameObject damageFactor, bool isKnockback = false, float knockPower = 0.2f, bool isCritlcal = false);
}
