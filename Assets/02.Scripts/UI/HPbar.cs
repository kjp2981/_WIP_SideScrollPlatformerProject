using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HPbar : MonoBehaviour
{
    [SerializeField]
    private GameObject _hpbar;

    public void SetHp(float percentHp)
    {
        _hpbar.transform.DOScaleX(percentHp < 0 ? 0 : percentHp, 0.3f);
    }
}
