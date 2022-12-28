using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSaveData
{
    public List<WeaponStatusDataSO> weaponDataDic = new List<WeaponStatusDataSO>();
    public List<WeaponStatusDataSO> weaponList = new List<WeaponStatusDataSO>();

    //public SkillDataSO[] skillDataList = new SkillDataSO[2] { null, null };
    public List<SkillDataSO> skillList = new List<SkillDataSO>(); // 왜 2개로 만들어 놯지..?

    public int hp;

    // 스탯 정보도 저장해야하나? 해야할 듯

    public int level = 1;
    public int currentExp = 0;
}
