using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using NaughtyAttributes;

public class Status
{
    public int hp;
    public int meleeAttack;
    public int rangeAttack;
    public int defence;
    public int criticalRate;
    public float criticalDamage;
}

public class Player : MonoBehaviour, IHittable, IKnockback, IAvoidable
{
    [SerializeField]
    private StatusDataSO status;

    public Status realStatus
    {
        get
        {
            Status status = new Status();
            int hpOffset = 0;
            int defenceOffset = 0;
            int meleeAttackOffset = 0;
            int rangeAttackOffset = 0;
            int criticalRateOffset = 0;
            int criticalDamageOffset = 0;
            for (int i = 0; i < 6; i++)
            {
                if (weaponInfo.WeaponDataDic.ContainsKey((WeaponType)i) == true)
                {
                    if (weaponInfo.WeaponDataDic[(WeaponType)i] != null)
                    {
                        switch (weaponInfo.WeaponDataDic[(WeaponType)i].abilityType)
                        {
                            case AbilityType.HP:
                                hpOffset += weaponInfo.WeaponDataDic[(WeaponType)i].addValue;
                                break;
                            case AbilityType.MeleeDamage:
                                meleeAttackOffset += weaponInfo.WeaponDataDic[(WeaponType)i].addValue;
                                break;
                            case AbilityType.RangeDamage:
                                rangeAttackOffset += weaponInfo.WeaponDataDic[(WeaponType)i].addValue;
                                break;
                            case AbilityType.Defence:
                                defenceOffset += weaponInfo.WeaponDataDic[(WeaponType)i].addValue;
                                break;
                            case AbilityType.CriticalRate:
                                criticalRateOffset += weaponInfo.WeaponDataDic[(WeaponType)i].addValue;
                                break;
                            case AbilityType.CriticalDamage:
                                criticalDamageOffset += weaponInfo.WeaponDataDic[(WeaponType)i].addValue;
                                break;
                        }
                    }
                }
            }
            status.hp = this.status.hp + hpOffset;
            status.meleeAttack = this.status.meleeAttack + meleeAttackOffset;
            status.rangeAttack = this.status.rangeAttack + rangeAttackOffset;
            status.defence = this.status.defence + defenceOffset;
            status.criticalRate = this.status.criticalRate + criticalRateOffset;
            status.criticalDamage = this.status.criticalDamage + criticalDamageOffset;

            return status;
        }
    }

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
            UIManager.Instance.PlayerOutHpbar(HP, realStatus.hp);
        }
    }

    public bool Death { get; private set; } = false;
    #endregion

    private AgentMovement movement;
    [SerializeField]
    private WeaponInfo weaponInfo;

    private void Awake()
    {
        movement = GetComponent<AgentMovement>();
    }

    private void Start()
    {
        HP = realStatus.hp;
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

    public bool isCritical()
    {
        int critical = Random.Range(0, 100);
        if (critical <= realStatus.criticalRate)
            return true;
        else
            return false;
    }

    public int GetAttackDamage(bool isMelee = true, bool isStrong = false, bool isCritical = false)
    {
        int damage = 0;
        if(isMelee == true)
        {
            damage = realStatus.meleeAttack;
        }
        else
        {
            damage = realStatus.rangeAttack;
        }

        if(isStrong == true)
        {
            damage = Mathf.CeilToInt(damage * 1.5f);
        }

        if(isCritical == true)
        {
            damage = Mathf.CeilToInt(damage * realStatus.criticalDamage);
        }

        return damage;
    }
}
