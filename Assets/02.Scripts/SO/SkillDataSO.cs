using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public enum SkillType
{
    beginner, // 초급 모험가
    intermediate, // 중급 모험가
    senior, // 상급 모험가
    Iron, // 아이언 무기
    steel, // 강철 무기
    Gold, // 황금 무기
    Emerald, // 에메랄드
    specialAlloy, // 특수 합금
    Poison, // 독 무기
    Dragon, // 드래곤 무기
    Snack, // 과자
    Alien, // 외계
    Empire, // 제국
    Berserker, // 광전사
}

[CreateAssetMenu(menuName = "SO/Agent/SkillData"), System.Serializable]
public class SkillDataSO : ScriptableObject
{
    public new string name;
    [ShowAssetPreview(32, 32)]
    public Sprite image;
    [ResizableTextArea]
    public string description;

    public float attack; // 위력
    public float range; // 범위
    public float coolTime;

    public SkillType type; // 2세트, 4세트 효과 추가하기
}
