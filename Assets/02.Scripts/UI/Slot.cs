using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] Image image;

    private SkillDataSO skill;
    public SkillDataSO Skill
    {
        get => skill;
        set
        {
            skill = value;
            if(skill == null)
            {
                image.sprite = skill.image;
                image.color = Color.white;
            }
            else
            {
                image.color = new Color(1, 1, 1, 0);
            }
        }
    }
}
