using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IronShield : PoolableMono, IHittable
{
    private SpriteRenderer spriteRenderer;
    private Material material;

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

    [SerializeField]
    private int hp = 10;

    public void Damage(int damage, GameObject damageFactor, bool isKnockback = false, float knockPower = 0.2F, bool isCritlcal = false)
    {
        hp -= 1;

        if (hp <= 0)
        {
            DestroyShield();
        }
        else
        {
            StartCoroutine(HitEffect());
        }
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        material = spriteRenderer.material;
    }

    private void Update()
    {
        if (isUse)
        {
            lifeTimer += Time.deltaTime;
        }

        if (lifeTimer >= life)
        {
            DestroyShield();
        }
    }

    IEnumerator HitEffect()
    {
        material.SetColor("_Color", new Color(1, 1, 1));
        yield return new WaitForSeconds(0.1f);
        material.SetColor("_Color", new Color(0, 0, 0));
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
        isUse = false;
    }
}
