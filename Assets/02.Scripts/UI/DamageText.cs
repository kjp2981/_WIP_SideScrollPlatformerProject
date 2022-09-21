using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class DamageText : PoolableMono
{
    private TextMeshPro text;

    private void Start()
    {
        text = GetComponent<TextMeshPro>();
    }

    public override void Reset()
    {
        transform.DOKill();
    }

    public void SetDamageText(int damage, bool isCritical = false, int fontSize = 4)
    {
        text.SetText(damage.ToString());

        text.fontSize = fontSize;
        if(isCritical == true)
        {
            text.color = Color.red;
        }
        else
        {
            text.color = Color.white;
        }
    }

    public void TextEffect()
    {
        transform.DOKill();

        transform.DOJump(Vector3.right, 1, 1, 0.3f).SetRelative().OnComplete(() =>
        {
            PoolManager.Instance.Push(this);
        });
    }
}
