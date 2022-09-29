using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Agent/Status")]
public class StatusDataSO : ScriptableObject
{
    public int hp; // 체력
    public int meleeAttack; // 근거리 공격력
    public int rangeAttack; //원거리 공격력
    public int defence; // 방어력
    //public int knockbackDefenc; // 넉백 저항력
    [Range(1, 100)]
    public int criticalRate; // 크리티컬 확률
    [Min(1f)]
    public float critlcalDamage;
}
