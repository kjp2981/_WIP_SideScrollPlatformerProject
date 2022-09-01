using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IHittable, IKnockback
{
    [SerializeField]
    private StatusDataSO status;

    public StatusDataSO Status => status;

    public bool IsEnemy => true;

    public Vector2 HitPos { get; private set; }

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

    [field : SerializeField] public UnityEvent OnHit { get; set; }
    #endregion

    private void Start()
    {
        HP = status.hp;
    }

    public void Damage(int damage, GameObject damageFactor)
    {
        HP -= damage;
        // 피격 이펙트 넣기 예) 피

        Slash slash = PoolManager.Instance.Pop("Slash") as Slash;
        float rot = Random.Range(0, 360);
        slash.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0, 0, rot));

        OnHit?.Invoke();

        if (transform.position == damageFactor.transform.position)
        {
            Knockback(Random.Range(0, 2) == 1 ? 1 : -1, 0.2f, 0.3f);
        }
        else
        {
            if (transform.position.x < damageFactor.transform.position.x)
            {
                Knockback(-1, 0.2f, 0.3f);
            }
            else
            {
                Knockback(1, 0.2f, 0.3f);
            }
        }

        Debug.Log("맞았다!");
    }

    public void Knockback(float direction, float power, float duration)
    {
        transform.DOMoveX(direction * power, duration).SetRelative();
    }
}
