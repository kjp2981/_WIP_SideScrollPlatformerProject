using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using NaughtyAttributes;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance = null;

    #region HP bar
    [SerializeField, Foldout("HPbar")]
    private GameObject playerOutHpbar;
    [SerializeField, Foldout("HPbar")]
    private GameObject playerInHpbar;
    #endregion

    #region CoolTImeImage
    [SerializeField, Foldout("CoolTimeImage")]
    private Image leftCoolTimeImage;
    [SerializeField, Foldout("CoolTimeImage")]
    private Image rightCoolTimeImage;
    #endregion

    #region Skill Panel
    [SerializeField, Foldout("Skill Panel")]
    private GameObject skillDescriptionPanel;
    [SerializeField, Foldout("Skill Panel")]
    private Sprite defaultImage;

    private Image skillImage;
    private Text skillName;
    private Text skillDescription;
    #endregion

    #region Skill UI
    [SerializeField, Foldout("Skill Image")]
    private Image leftSkill, rightSkill;
    #endregion

    private SkillCollection skillCollection;
    [SerializeField]
    private TInventory<SkillDataSO> skillInventory;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        skillCollection = FindObjectOfType<SkillCollection>();

        skillImage = skillDescriptionPanel.transform.Find("SkillImage").GetComponent<Image>();
        skillName = skillDescriptionPanel.transform.Find("SkillName").GetComponent<Text>();
        skillDescription = skillDescriptionPanel.transform.Find("Description").GetComponent<Text>();

        UpdateSkillImage(true);
        UpdateSkillImage(false);
    }

    #region �÷��̾� HP��
    public void PlayerOutHpbar(int currentHp, int maxHp)
    {
        Vector3 scale = playerOutHpbar.transform.localScale;
        scale.x = (float)currentHp / (float)maxHp;
        scale.x = Mathf.Clamp(scale.x, 0f, 1f);
        playerOutHpbar.transform.localScale = scale;

        transform.DOKill();
        StopAllCoroutines();
        StartCoroutine(PlayerInHpbar(currentHp, maxHp));
    }

    private IEnumerator PlayerInHpbar(int currentHp, int maxHp)
    {
        yield return new WaitForSeconds(0.5f);

        float hp = (float)currentHp / (float)maxHp;
        hp = Mathf.Clamp(hp, 0f, 1f);
        playerInHpbar.transform.DOScaleX(hp, 0.3f);
    }
    #endregion

    #region ��ų ��Ÿ�� ǥ��
    public void SkillCoolTime()
    {
        if (skillCollection.LeftSkill != null)
        {
            leftCoolTimeImage.fillAmount = skillCollection.LeftSkillCoolTime / skillCollection.LeftSkill.coolTime;
        }
        if (skillCollection.RightSkill != null)
        {
            rightCoolTimeImage.fillAmount = skillCollection.RightSkillCoolTime / skillCollection.RightSkill.coolTime;
        }
    }
    #endregion

    #region ��ų ���� ����
    public void SkillDescriptionPanel(SkillDataSO skill)
    {
        if (skill != null)
        {
            skillImage.sprite = skill.image;
            skillName.text = skill.name;
            skillDescription.text = skill.description;
        }
        else
        {
            skillImage.sprite = defaultImage;
            skillName.text = "";
            skillDescription.text = "";
        }
    }
    #endregion

    #region ��� ��ų UI ������Ʈ
    public void UpdateSkillImage(bool isLeft)
    {
        if(isLeft == true)
            leftSkill.sprite = skillCollection.LeftSkill.image;
        else
            rightSkill.sprite = skillCollection.RightSkill.image;
    }
    #endregion

    public void SetSkill(bool isLeft)
    {
        skillCollection.SetSkill(skillInventory.SelectSlot, isLeft);
    }
}
