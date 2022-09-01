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

    #region HP ������
    private int hp;
    public int HP
    {
        get => hp;
        private set
        {
            hp = value;
            // hp ������ ó���ϱ�
            // hp�� ���� ��.
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
        // �ǰ� ����Ʈ �ֱ� ��) ��

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

        Debug.Log("�¾Ҵ�!");
    }

    public void Knockback(float direction, float power, float duration)
    {
        transform.DOMoveX(direction * power, duration).SetRelative();
    }
}
