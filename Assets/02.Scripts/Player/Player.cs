using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class Player : MonoBehaviour, IHittable, IKnockback
{
    [SerializeField]
    private StatusDataSO status;

    public StatusDataSO Status => status;

    public bool IsEnemy => false;

    public Vector2 HitPos { get; private set; }

    [field : SerializeField] public UnityEvent OnHit { get; set; }
    public UnityEvent OnDie;

    #region HP 구현부
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

    private bool death = false;
    public bool Death => death;
    #endregion

    private void Start()
    {
        HP = status.hp;
    }

    public void Damage(int damage, GameObject damageFactor)
    {
        HP -= damage;

        if (HP <= 0)
        {
            death = true;
            OnDie?.Invoke(); // 여기에 사맘 이펙ㅌ 넣기 예) 스탑, 슬로우 모션, 쉐이크
        }
        else
        {
            // 피 이펙트 넣기
            OnHit?.Invoke();

            if (transform.position == damageFactor.transform.position)
            {
                Knockback(Random.Range(0, 2) == 1 ? 1 : -1, 0.7f, 0.3f);
            }
            else
            {
                if (transform.position.x < damageFactor.transform.position.x)
                {
                    Knockback(-1, 0.7f, 0.3f);
                }
                else
                {
                    Knockback(1, 0.7f, 0.3f);
                }
            }
        }
    }

    public void Knockback(float direction, float power, float duration)
    {
        transform.DOMoveX(direction * power, duration).SetRelative();
    }
}
