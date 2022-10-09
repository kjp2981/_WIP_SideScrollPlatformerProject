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

    #region Weapon Panel
    [SerializeField, Foldout("WeaponPanel")]
    private GameObject weaponPanel;

    private Image weaponImage;
    private Text weaponName;
    private Text weaponAbility;
    private GameObject weaponUseBtn;
    private GameObject weaponUnUseBtn;

    [SerializeField, Foldout("WeaponPanel")]
    private WeaponInfo weaponInfo;
    #endregion

    #region Skill UI
    [SerializeField, Foldout("Skill Image")]
    private Image leftSkill, rightSkill;
    [SerializeField, Foldout("Skill Image")]
    private Image leftUISkill, rightUISkill;
    #endregion

    private SkillCollection skillCollection;
    #region Inventory
    [SerializeField, Foldout("Inventory")]
    private GameObject inventoryController;
    [SerializeField, Foldout("Inventory")]
    private SkillInventory skillInventory;
    [SerializeField, Foldout("Inventory")]
    private WeaponInventory weaponInventory;
    #endregion

    private Dictionary<AbilityType, string> abilityDic = new Dictionary<AbilityType, string>();

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

        weaponImage = weaponPanel.transform.Find("WeaponImage").GetComponent<Image>();
        weaponName = weaponPanel.transform.Find("WeaponName").GetComponent<Text>();
        weaponAbility = weaponPanel.transform.Find("Description").GetComponent<Text>();
        weaponUseBtn = weaponPanel.transform.Find("UseButton").gameObject;
        weaponUnUseBtn = weaponPanel.transform.Find("UnUseButton").gameObject;

        UpdateSkillImage(isLeft: true);
        UpdateSkillImage(isLeft: false);

        abilityDic.Add(AbilityType.HP, "�����");
        abilityDic.Add(AbilityType.MeleeDamage, "�ٰŸ� ���ݷ�");
        abilityDic.Add(AbilityType.RangeDamage, "���Ÿ� ���ݷ�");
        abilityDic.Add(AbilityType.Defence, "����");
        abilityDic.Add(AbilityType.CriticalRate, "ġ��Ÿ Ȯ��");
        abilityDic.Add(AbilityType.CriticalDamage, "ġ��Ÿ ������");
    }

    public void Start()
    {
        SetInventoryActive(false);
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
    public void SetActiveSkillPanel(bool active)
    {
        skillDescriptionPanel.SetActive(active);
    }

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
    public void UpdateSkillImage()
    {
        UpdateSkillImage(true);
        UpdateSkillImage(false);
    }

    public void UpdateSkillImage(bool isLeft)
    {
        if (isLeft == true)
        {
            if (skillCollection.LeftSkill == null)
            {
                leftSkill.sprite = defaultImage;
            }
            else
            {
                leftSkill.sprite = skillCollection.LeftSkill.image;
            }
            leftUISkill.sprite = leftSkill.sprite;
        }
        else
        {
            if (skillCollection.RightSkill == null)
            {
                rightSkill.sprite = defaultImage;
            }
            else
            {
                rightSkill.sprite = skillCollection.RightSkill.image;
            }
            rightUISkill.sprite = rightSkill.sprite;
        }
    }
    #endregion

    public void SetSkill(bool isLeft)
    {
        skillCollection.SetSkill(skillInventory.SelectSlot, isLeft);
    }

    public void SetInventoryActive(bool isActive)
    {
        inventoryController.SetActive(isActive);
    }

    #region ��� ���� ����
    public void SetActiveWeaponDescriptionPanel(bool active)
    {
        weaponPanel.SetActive(active);
    }

    public void WeaponDescriptionPanel(WeaponStatusDataSO weapon)
    {
        if(weapon != null)
        {
            weaponImage.color = new Color(1, 1, 1, 1);
            weaponImage.sprite = weapon.image;
            weaponName.text = weapon.name;
            weaponAbility.text = abilityDic[weapon.abilityType] + " +" + weapon.addValue;

            if (weaponInventory.ReturnActiveSelectUseText() == true)//���� ������ �� ���¶��
            {
                weaponUseBtn.SetActive(false); //������ư ����
                weaponUnUseBtn.SetActive(true); //������ư Ŭ��
            }
            else
            {
                weaponUnUseBtn.SetActive(false);
                weaponUseBtn.SetActive(true);
            }
        }
        else
        {
            weaponImage.color = new Color(1, 1, 1, 0.3f);
            weaponImage.sprite = defaultImage;
            weaponName.text = "";
            weaponAbility.text = "";
        }
    }
    #endregion
}
