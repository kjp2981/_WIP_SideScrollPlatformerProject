using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.Events;

public class AgentAttack : MonoBehaviour
{
    private BoxCollider2D hitCollider;
    [SerializeField, Layer]
    private int hitLayer;

    private int combo = 0;
    public int Combo => combo;

    private float comboTimer = 0f;
    private float comboTime = 3f;

    public void MeleeAttack(bool isWeak)
    {
        if(isWeak == true)
        {
            ++combo;
            Debug.Log($"근거리 약공격, combo : {combo}");
        }
        else
        {
            ++combo;
            Debug.Log($"근거리 강공격, combo : {combo}");
        }
    }

    public void RangeAttack(bool isWeak)
    {
        if (isWeak == true)
        {
            Debug.Log("원거리 약공격");
        }
        else
        {
            Debug.Log("원거리 강공격");
        }
    }

    private void Update()
    {
        if(combo != 0)
        {
            comboTimer += Time.deltaTime;

            if(comboTimer >= comboTime || combo >= 3)
            {
                combo = 0;
                comboTimer = 0f;
            }
        }
    }
}
