using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Agent/Status")]
public class StatusDataSO : ScriptableObject
{
    public int hp; // ü��
    public int meleeAttack; // �ٰŸ� ���ݷ�
    public int rangeAttack; //���Ÿ� ���ݷ�
    public int defence; // ����
    //public int knockbackDefenc; // �˹� ���׷�
    [Range(1, 100)]
    public int criticalRate; // ũ��Ƽ�� Ȯ��
    [Min(1f)]
    public float critlcalDamage;
}
