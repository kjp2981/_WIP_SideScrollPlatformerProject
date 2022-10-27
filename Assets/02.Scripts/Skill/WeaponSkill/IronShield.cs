using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IronShield : MonoBehaviour, IHittable
{
    public bool IsEnemy => false;

    public bool Death => false;

    public Vector2 HitPos => transform.position;
    public UnityEvent OnHit { get; set; }
    public UnityEvent OnDie { get; set; }

    public void Damage(int damage, GameObject damageFactor, bool isKnockback = false, float knockPower = 0.2F, bool isCritlcal = false)
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // 팅소리와 함께 이펙트 생성
        if (collision.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            BloodParticle landParticle = PoolManager.Instance.Pop("LandParticle") as BloodParticle;
            landParticle.transform.position = this.transform.position - new Vector3(0, 0.5f, 0);
        }
    }
}
