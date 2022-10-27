using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IronShield : PoolableMono, IHittable
{
    public bool IsEnemy => false;

    public bool Death => false;

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

        if(lifeTimer >= life)
        {
            // ���� ����Ʈ

            PoolManager.Instance.Push(this);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // �üҸ��� �Բ� ����Ʈ ����
        if (collision.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            BloodParticle landParticle = PoolManager.Instance.Pop("LandParticle") as BloodParticle;
            landParticle.transform.position = this.transform.position - new Vector3(0, 0.5f, 0);
            isUse = true;
        }
    }

    public override void Reset()
    {
        throw new System.NotImplementedException();
    }
}
