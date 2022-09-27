using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;

public class Enemy : PoolableMono, IHittable, IKnockback
{
    [SerializeField]
    private StatusDataSO status;

    public StatusDataSO Status => status;

    public bool IsEnemy => true;

    public Vector2 HitPos { get; private set; }

    #region HP 구현부
    [ShowNonSerializedField]
    private int hp;
    public int HP
    {
        get => hp;
        private set
        {
            hp = value;
            // hp 변경후 처리하기
            // hp바 변경 등.
            _hpbar.SetHp((float)HP / status.hp);
        }
    }

    private HPbar _hpbar;

    [field : SerializeField] public UnityEvent OnHit { get; set; }
    [field : SerializeField] public UnityEvent OnDie { get; set; }

    public bool Death { get; private set; } = false;

    private EnemyAIBrain _enemyAIBrain = null;
    #endregion

    private EnemySpawner parentSpawner;
    public EnemySpawner ParentSpawner
    {
        get => parentSpawner;
        set => parentSpawner = value;
    }

    private Material material;
    private Animator animator;

    private void Awake()
    {
        material = transform.Find("VisualSprite").GetComponent<SpriteRenderer>().material;
        animator = transform.Find("VisualSprite").GetComponent<Animator>();
        _enemyAIBrain = GetComponent<EnemyAIBrain>();
        _hpbar = transform.Find("HPbar").GetComponent<HPbar>();

        HP = status.hp;
    }

    public void Damage(int damage, GameObject damageFactor, bool isKnockback = false, float knockPower = 0.2f, bool isCritical = false)
    {
        if (Death == true) return;

        Slash slash = PoolManager.Instance.Pop("HitEffect") as Slash;
        float rot = Random.Range(0, 360);
        Vector3 offset = Random.insideUnitCircle * 0.5f;
        slash.transform.SetPositionAndRotation(transform.position + offset, Quaternion.Euler(0, 0, rot));

        BloodParticle bloodParticle = PoolManager.Instance.Pop("BloodParticle") as BloodParticle;
        bloodParticle.transform.position = this.transform.position;
        float value = damageFactor.transform.position.x > this.transform.position.x ? -1 : 1;
        bloodParticle.SetLocalScaleX(value);

        DamageText text = PoolManager.Instance.Pop("DamageText") as DamageText;
        text.transform.position = this.transform.position;
        if (isCritical == true)
        {
            text.SetDamageText(damage, isCritical, 5);
        }
        else
        {
            text.SetDamageText(damage, isCritical, 4);
        }

        if (isKnockback == true)
        {
            if (transform.position == damageFactor.transform.position)
            {
                Knockback(Random.Range(0, 2) == 1 ? 1 : -1, knockPower, 0.3f);
            }
            else
            {
                if (transform.position.x < damageFactor.transform.position.x)
                {
                    Knockback(-1, knockPower, 0.3f);
                }
                else
                {
                    Knockback(1, knockPower, 0.3f);
                }
            }
        }

        HP -= damage;

        if (HP <= 0)
        {
            Death = true;

            OnDie?.Invoke();

            _enemyAIBrain.target = null;

            parentSpawner.RemoveMonster(this.gameObject.name);
        }
        else
        {
            OnHit?.Invoke();
        }
    }

    public void Knockback(float direction, float power, float duration)
    {
        transform.DOMoveX(direction * power, duration).SetRelative();
    }

    public void DissolveEffect()
    {
        Sequence seq = DOTween.Sequence();
        material.DOFloat(0f, "_Fade", 0.5f).SetDelay(animator.GetCurrentAnimatorStateInfo(0).length).OnComplete(() => PoolManager.Instance.Push(this));
    }

    public override void Reset()
    {
        if(material == null)
            material = transform.Find("VisualSprite").GetComponent<SpriteRenderer>().material;
        material.SetFloat("_Fade", 1f);
        HP = status.hp;
        Death = false;
        _enemyAIBrain.target = Define.Player.transform;
    }
}
