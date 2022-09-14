using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCollection : MonoBehaviour
{
    // ���⿡ ��ų �Լ��� �ۼ��Ѵ�
    // ���ǻ��� : ��ų SO�� ���� �̸��� �Լ��̸����� �ؾ��Ѵ�. GetMethod�� �ҷ����� ����!

    [SerializeField]
    private SkillDataSO leftSkill;
    public SkillDataSO LeftSkill
    {
        get => leftSkill;
        set
        {
            leftSkill = value;
            leftSkillCoolTime = leftSkill.coolTime;
        }
    }
    [SerializeField]
    private SkillDataSO rightSkill;
    public SkillDataSO RightSkill
    {
        get => rightSkill;
        set
        {
            rightSkill = value;
            rightSkillCoolTime = rightSkill.coolTime;
        }
    }

    private float leftSkillCoolTime = 0f;
    public float LeftSkillCoolTime => leftSkillCoolTime;
    private float rightSkillCoolTime = 0f;
    public float RightSkillCoolTime => rightSkillCoolTime;

    private void Awake()
    {
        if(leftSkill != null)
        {
            leftSkillCoolTime = leftSkill.coolTime;
        }

        if (rightSkill != null)
        {
            rightSkillCoolTime = rightSkill.coolTime;
        }
    }

    private void Update()
    {
        if(leftSkillCoolTime > 0)
        {
            leftSkillCoolTime -= Time.deltaTime;
            UIManager.Instance.SkillCoolTime();
        }

        if (rightSkillCoolTime > 0)
        {
            rightSkillCoolTime -= Time.deltaTime;
            UIManager.Instance.SkillCoolTime();
        }
    }

    #region ��ų ��� �Լ���
    public void UseSkill(SkillDataSO skill)
    {
        if (skill != null)
        {
            this.GetType().GetMethod(skill.name).Invoke(this, null);
        }
    }

    public void UseLeftSkill()
    {
        if (leftSkill != null && leftSkillCoolTime <= 0)
        {
            UseSkill(leftSkill);
            leftSkillCoolTime = leftSkill.coolTime;
        }
    }

    public void UseRightSkill()
    {
        if (rightSkill != null && rightSkillCoolTime <= 0)
        {
            UseSkill(rightSkill);
            rightSkillCoolTime = rightSkill.coolTime;
        }
    }
    #endregion

    #region ��ų��
    public void Fireball()
    {
        Debug.Log("ũ��! ������ ���̾�� �߻���.");

    }
    #endregion
}
