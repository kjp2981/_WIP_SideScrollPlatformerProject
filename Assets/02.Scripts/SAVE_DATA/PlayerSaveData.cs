using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSaveData
{
    public List<WeaponStatusDataSO> weaponDataDic = new List<WeaponStatusDataSO>();
    public List<WeaponStatusDataSO> weaponList = new List<WeaponStatusDataSO>();

    //public SkillDataSO[] skillDataList = new SkillDataSO[2] { null, null };
    public List<SkillDataSO> skillList = new List<SkillDataSO>(); // �� 2���� ����� �Q��..?

    public int hp;

    // ���� ������ �����ؾ��ϳ�? �ؾ��� ��

    public int level = 1;
    public int currentExp = 0;
}
