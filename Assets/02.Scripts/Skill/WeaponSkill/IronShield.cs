using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IronShield : PoolableMono, IHittable
{
    public bool IsEnemy => false;

    public bool Death => false;

    public bool isDamage { get; private set; } = false;

    public Vector2 HitPos => transform.position;
    public UnityEvent OnHit { get; set; }
    public UnityEvent OnDie { get; set; }

    private bool isUse = false;
    private float lifeTimer = 0f;

    [SerializeField]
    private float life = 5f;

    public void Damage(int damage, GameObject damageFactor, bool isKnockback = false, float knockPower = 0.2F, bool isCritlcal = false)
    {
        
    }

    private void Update()
    {
        if (isUse)
        {
            lifeTimer += Time.deltaTime;
        }

        //if(lifeTimer >= life)
        //{
        //    DestroyShield();
        //}
    }

    private void DestroyShield()
    {
        BloodParticle destroyParticle = PoolManager.Instance.Pop("DestroyParticle") as BloodParticle;
        destroyParticle.transform.position = this.transform.position;

        PoolManager.Instance.Push(this);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // 팅소리와 함께 이펙트 생성
        if (collision.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            BloodParticle landParticle = PoolManager.Instance.Pop("LandParticle") as BloodParticle;
            landParticle.transform.position = this.transform.position - new Vector3(0, 0.5f, 0);
            isUse = true;
        }
    }

    public override void Reset()
    {

    }
}
