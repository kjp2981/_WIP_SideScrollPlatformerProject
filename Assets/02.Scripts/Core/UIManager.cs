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

    #region CoolTimeImage
    [SerializeField, Foldout("CoolTimeImage")]
    private Image[] _skillCoolTimeImage;
    [SerializeField, Foldout("CoolTimeImage")]
    private Image[] _weaponCoolTimeImage;
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
    //[SerializeField, Foldout("Skill Image")]
    //private Image leftSkill, rightSkill;
    //[SerializeField, Foldout("Skill Image")]
    //private Image leftUISkill, rightUISkill;
    //[SerializeField, Foldout("Skill Image")]
    //private Image weaponSkill;

    [SerializeField, Foldout("Skill Image")]
    private Image[] _skillImage;
    [SerializeField, Foldout("Skill Image")]
    private Image[] _skillUIImage;
    [SerializeField, Foldout("Skill Image")]
    private Image[] _weaponImage;
    [SerializeField, Foldout("Skill Image")]
    private Image[] _weaponUIImage;
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

    [SerializeField]
    private GameObject settingPanel;

    #region Sound Slider
    [SerializeField, Foldout("Sound Slider Fill")]
    private GameObject bgmSliderFill;
    [SerializeField, Foldout("Sound Slider Fill")]
    private GameObject sfxSliderFill;
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

        UpdateSkillImage();

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
        for (int i = 0; i < skillCollection.SkillList.Count; i++)
        {
            if (skillCollection.SkillList[i].Skill != null)
            {
                _skillCoolTimeImage[i].fillAmount = skillCollection.SkillList[i].CoolTime / skillCollection.SkillList[i].Skill.coolTime;
            }
        }

        for (int i = 0; i < skillCollection.WeaponList.Count; i++)
        {
            if (skillCollection.WeaponList[i].Skill != null)
            {
                _weaponCoolTimeImage[i].fillAmount = skillCollection.WeaponList[i].CoolTime / skillCollection.WeaponList[i].Skill.coolTime;
            }
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
        for(int i = 0; i < skillCollection.SkillList.Count; i++)
        {
            UpdateSkillImage(i);
        }

        for (int i = 0; i < skillCollection.WeaponList.Count; i++)
        {
            UpdateWeaponSkillImage(i);
        }
    }

    public void UpdateSkillImage(int index)
    {
        if (skillCollection.SkillList[index] == null || skillCollection.SkillList[index].Skill == null)
        {
            _skillImage[index].sprite = defaultImage;
        }
        else
        {
            _skillImage[index].sprite = skillCollection.SkillList[index].Skill.image;
        }
        _skillUIImage[index].sprite = _skillImage[index].sprite;
    }

    public void UpdateWeaponSkillImage(int index)
    {
        if (skillCollection.WeaponList[index] == null || skillCollection.WeaponList[index].Skill == null)
        {
            _weaponImage[index].sprite = defaultImage;
        }
        else
        {
            _weaponImage[index].sprite = skillCollection.WeaponList[index].Skill.image;
        }
        _weaponUIImage[index].sprite = _weaponImage[index].sprite;
    }
    #endregion

    public void SetSkill(int index)
    {
        skillCollection.SetSkill(skillInventory.SelectSlot, index);
    }


    public GameObject SetInventoryActive(bool? isActive = null)
    {
        if (isActive.HasValue)
        {
            inventoryController.SetActive(isActive.Value);
        }
        return inventoryController;
    }

    public GameObject SetSettingPanelActive(bool isActive)
    {
        settingPanel.SetActive(isActive);
        return settingPanel;
    }

    public void BgmSliderValue(float value)
    {
        if (value > 0)
        {
            bgmSliderFill.SetActive(true);
        }
        else
        {
            bgmSliderFill.SetActive(false);
        }
    }

    public void SfxSliderValue(float value)
    {
        if (value > 0)
        {
            sfxSliderFill.SetActive(true);
        }
        else
        {
            sfxSliderFill.SetActive(false);
        }
    }

    #region ��� ���� ����
    public void SetActiveWeaponDescriptionPanel(bool active)
    {
        weaponPanel.SetActive(active);
    }

    public void WeaponDescriptionPanel(WeaponStatusDataSO weapon)
    {
        if (weapon != null)
        {
            weaponImage.color = new Color(1, 1, 1, 1);
            weaponImage.sprite = weapon.image;
            weaponName.text = weapon.name;
            weaponAbility.text = abilityDic[weapon.abilityType] + " +" + weapon.addValue;

            if (weaponInfo.WeaponDataDic.ContainsKey(weapon.weaponType) == true)
            {
                if (weaponInfo.WeaponDataDic[weapon.weaponType] == null)
                {
                    weaponUseBtn.SetActive(true);
                    weaponUnUseBtn.SetActive(false);
                }
                else
                {
                    if (weaponInfo.WeaponDataDic[weapon.weaponType] == weapon)
                    {
                        weaponUseBtn.SetActive(false);
                        weaponUnUseBtn.SetActive(true);
                    }
                    else
                    {
                        weaponUseBtn.SetActive(true);
                        weaponUnUseBtn.SetActive(false);
                    }
                }
            }
            else
            {
                weaponUseBtn.SetActive(true);
                weaponUnUseBtn.SetActive(false);
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
