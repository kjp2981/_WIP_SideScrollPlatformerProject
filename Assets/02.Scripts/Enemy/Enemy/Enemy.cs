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
        }
    }

    [field : SerializeField] public UnityEvent OnHit { get; set; }
    [field : SerializeField] public UnityEvent OnDie { get; set; }

    private bool death = false;
    #endregion

    private void Start()
    {
        HP = status.hp;
    }

    public void Damage(int damage, GameObject damageFactor, bool isKnockback = false, float knockPower = 0.2f)
    {
        if (death == true) return;


        HP -= damage;

        if (HP <= 0)
        {
            OnDie?.Invoke();
        }
        else
        {
            // 피격 이펙트 넣기 예) 피

            #region 슬래쉬 이펙트
            Slash slash = PoolManager.Instance.Pop("Slash") as Slash;
            float rot = Random.Range(0, 360);
            slash.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, rot));
            #endregion

            OnHit?.Invoke();

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
        }
    }

    public void Knockback(float direction, float power, float duration)
    {
        transform.DOMoveX(direction * power, duration).SetRelative();
    }

    public override void Reset()
    {
        HP = status.hp;
        death = false;
    }
}
