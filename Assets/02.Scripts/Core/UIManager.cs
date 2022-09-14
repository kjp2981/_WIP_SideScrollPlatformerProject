using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using NaughtyAttributes;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance = null;

    [SerializeField, Foldout("HPbar")]
    private GameObject playerOutHpbar;
    [SerializeField, Foldout("HPbar")]
    private GameObject playerInHpbar;

    [SerializeField, Foldout("CoolTimeImage")]
    private Image leftCoolTimeImage;
    [SerializeField, Foldout("CoolTimeImage")]
    private Image rightCoolTimeImage;

    [SerializeField]
    private GameObject skillDescriptionPanel;

    private Image skillImage;
    private Text skillName;
    private Text skillDescription;

    private SkillCollection skillCollection;

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
        skillImage.sprite = skill.image;
        skillName.text = skill.name;
        skillDescription.text = skill.description;
    }
    #endregion
}
