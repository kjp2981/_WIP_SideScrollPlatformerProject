using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    private int level;

    private int currentExp;
    private int nextExp;

    private void Start()
    {
        currentExp = 0;
        nextExp = 100;
    }

    public void AddExp(int exp)
    {
        currentExp += exp;
        if(currentExp >= nextExp)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        if(currentExp == nextExp)
        {
            level += 1;
            currentExp = 0;
        }
        else
        {
            int temp = currentExp - nextExp;
            currentExp = 0;
            level += 1;
            currentExp += temp;
        }

        // 뭔가 혜택을 주면되지 않을까?
        // 1. HP 회복 ?
        // 2. 
    }
}
