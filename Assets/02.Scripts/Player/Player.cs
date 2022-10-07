using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using NaughtyAttributes;

public class Player : MonoBehaviour, IHittable, IKnockback, IAvoidable
{
    [SerializeField]
    private StatusDataSO status;

    public StatusDataSO Status => status;

    public bool IsEnemy => false;

    public Vector2 HitPos { get; private set; }

    [field : SerializeField] public UnityEvent OnHit { get; set; }
    [field : SerializeField] public UnityEvent OnDie { get; set; }

    public UnityEvent OnDash = null;

    #region HP 구현부
    [ShowNonSerializedField]
    private int hp;
    public int HP
    {
        get => hp;
        private set
        {
            hp = value;
            UIManager.Instance.PlayerOutHpbar(HP, status.hp);
        }
    }

    public bool Death { get; private set; } = false;
    #endregion

    private AgentMovement movement;

    private void Awake()
    {
        movement = GetComponent<AgentMovement>();
    }

    private void Start()
    {
        HP = status.hp;
    }

    public void Damage(int damage, GameObject damageFactor, bool isKnockback = false, float knockPower = 0.2f, bool isCritical = false)
    {
        if (Death == true) return;

        if (movement.IsDash == true)
        {
            Avoid();
        }
        else
        {
            if (isKnockback == true)
            {
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

            HP -= damage;

            if (HP <= 0)
            {
                Death = true;

                OnDie?.Invoke(); // 여기에 사맘 이펙ㅌ 넣기 예) 스탑, 슬로우 모션, 쉐이크
            }
            else
            {
                // 피 이펙트 넣기
                OnHit?.Invoke();
            }
        }
    }

    public void Knockback(float direction, float power, float duration)
    {
        transform.DOMoveX(direction * power, duration).SetRelative();
    }

    public void Avoid()
    {
        // 회피가 되면 이펙트(붕괴3rd에 Q.T.E 같은거?) 보여주고 다음 공격 추가피해주기 이건 할 수 있으면 하기
        // 최선 사항 : 회피 클자 뛰우기, 카메라 쉐이크 같은 이펙트?
        // 카메라 쉐이크, 타임 슬로우, 화려한 이펙트(되면)
        OnDash?.Invoke();
        Debug.Log("Avoid!");
    }
}
