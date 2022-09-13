using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(menuName = "SO/Agent/SkillData")]
public class SkillDataSO : ScriptableObject
{
    public new string name;
    public Sprite image;
    [ResizableTextArea]
    public string description;

    public int coolTime;
}
