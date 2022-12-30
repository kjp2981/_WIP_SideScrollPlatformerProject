using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public enum SkillType
{
    beginner, // �ʱ� ���谡
    intermediate, // �߱� ���谡
    senior, // ��� ���谡
    Iron, // ���̾� ����
    steel, // ��ö ����
    Gold, // Ȳ�� ����
    Emerald, // ���޶���
    specialAlloy, // Ư�� �ձ�
    Poison, // �� ����
    Dragon, // �巡�� ����
    Snack, // ����
    Alien, // �ܰ�
    Empire, // ����
    Berserker, // ������
}

[CreateAssetMenu(menuName = "SO/Agent/SkillData"), System.Serializable]
public class SkillDataSO : ScriptableObject
{
    public new string name;
    [ShowAssetPreview(32, 32)]
    public Sprite image;
    [ResizableTextArea]
    public string description;

    public float attack; // ����
    public float range; // ����
    public float coolTime;

    public SkillType type; // 2��Ʈ, 4��Ʈ ȿ�� �߰��ϱ�
}
