using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IHittable
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
    #endregion

    private void Start()
    {
        HP = status.hp;
    }

    public void Damage(int damage, GameObject damageFactor)
    {

    }
}
