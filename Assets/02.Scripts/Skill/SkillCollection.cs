using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCollection : MonoBehaviour
{
    // 에기에 스킬 함수를 작성한다
    // 유의사항 : 스킬 SO에 적은 이름을 함수이름으로 해야한다. GetMethod로 불러오기 때문!

    private AgentAttack playerAttack;
    private AgentAnimation playerAnimation;
    private SpriteRenderer playerSpriteRenderer;

    #region Skill
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
    #endregion

    private float leftSkillCoolTime = 0f;
    public float LeftSkillCoolTime => leftSkillCoolTime;
    private float rightSkillCoolTime = 0f;
    public float RightSkillCoolTime => rightSkillCoolTime;

    private void Awake()
    {
        playerAttack = Define.Player.GetComponent<AgentAttack>();
        playerSpriteRenderer = Define.Player.transform.Find("VisualSprite").GetComponent<SpriteRenderer>();
        playerAnimation = Define.Player.transform.Find("VisualSprite").GetComponent<AgentAnimation>();

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

    public void SetSkill(SkillDataSO skill, bool isLeft)
    {
        if (skill == null) return;

        if(isLeft == true)
        {
            if(rightSkill == skill)
            {
                rightSkill = leftSkill;
                leftSkill = skill;
                UIManager.Instance.UpdateSkillImage();
                UIManager.Instance.SkillCoolTime();
                return;
            }
            leftSkill = skill;
            UIManager.Instance.UpdateSkillImage(true);
            UIManager.Instance.SkillCoolTime();
        }
        else
        {
            if(leftSkill == skill)
            {
                leftSkill = rightSkill;
                rightSkill = skill;
                UIManager.Instance.UpdateSkillImage();
                UIManager.Instance.SkillCoolTime();
                return;
            }
            rightSkill = skill;
            UIManager.Instance.UpdateSkillImage(false);
            UIManager.Instance.SkillCoolTime();
        }
    }

    #region 스킬 사용 함수들
    public void UseSkill(SkillDataSO skill)
    {
        if (Time.timeScale == 0) return;

        if (skill != null)
        {
            this.GetType().GetMethod(skill.name).Invoke(this, null);
        }
    }

    public void UseLeftSkill()
    {
        if (Time.timeScale == 0) return;

        if (leftSkill != null && leftSkillCoolTime <= 0)
        {
            UseSkill(leftSkill);
            leftSkillCoolTime = leftSkill.coolTime;
        }
    }

    public void UseRightSkill()
    {
        if (Time.timeScale == 0) return;

        if (rightSkill != null && rightSkillCoolTime <= 0)
        {
            UseSkill(rightSkill);
            rightSkillCoolTime = rightSkill.coolTime;
        }
    }
    #endregion

    #region 스킬들
    public void Fireball()
    {
        Fireball fireball = PoolManager.Instance.Pop("Fireball") as Fireball;
        fireball.IsLeft = playerSpriteRenderer.transform.localScale.x == 1 ? true : false;
        //fireball.IsLeft = playerAnimation.IsFlipX ? true : false;
        fireball.transform.position = playerAttack.ArrowPos.position;
    }

    public void Slash()
    {
        Debug.Log("베기!");
    }

    public void ArrowRain()
    {
        Debug.Log("화살비가 내려와~");
    }
    #endregion
}
