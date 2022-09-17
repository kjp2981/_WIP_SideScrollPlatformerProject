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

    #region HP ������
    [ShowNonSerializedField]
    private int hp;
    public int HP
    {
        get => hp;
        private set
        {
            hp = value;
            // hp ������ ó���ϱ�
            // hp�� ���� ��.
            UIManager.Instance.PlayerOutHpbar(HP, status.hp);
        }
    }

    private bool death = false;
    public bool Death => death;
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

    public void Damage(int damage, GameObject damageFactor, bool isKnockback = false, float knockPower = 0.2f)
    {
        if (death == true) return;

        if (movement.IsDash == true)
        {
            Avoid();
        }
        else
        {
            HP -= damage;

            if (HP <= 0)
            {
                death = true;
                OnDie?.Invoke(); // ���⿡ �績 ���夼 �ֱ� ��) ��ž, ���ο� ���, ����ũ
            }
            else
            {
                // �� ����Ʈ �ֱ�
                OnHit?.Invoke();

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
            }
        }
    }

    public void Knockback(float direction, float power, float duration)
    {
        transform.DOMoveX(direction * power, duration).SetRelative();
    }

    public void Avoid()
    {
        // ȸ�ǰ� �Ǹ� ����Ʈ(�ر�3rd�� Q.T.E ������?) �����ְ� ���� ���� �߰������ֱ� �̰� �� �� ������ �ϱ�
        // �ּ� ���� : ȸ�� Ŭ�� �ٿ��, ī�޶� ����ũ ���� ����Ʈ?
        // ī�޶� ����ũ, Ÿ�� ���ο�, ȭ���� ����Ʈ(�Ǹ�)
        Debug.Log("Avoid!");
    }
}
