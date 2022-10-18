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

    public void SetDamageText(int damage, bool isCritical = false, int fontSize = 4, Color? color = null)
    {
        if(text == null)
            text = GetComponent<TextMeshPro>();

        text.SetText(damage.ToString());

        text.fontSize = fontSize;

        if (color.HasValue)
        {
            text.color = color.Value;
        }
        else
        {
            if (isCritical == true)
            {
                text.color = Color.red;
            }
            else
            {
                text.color = Color.white;
            }
        }

        TextEffect();
    }

    public void TextEffect()
    {
        transform.DOKill();

        //transform.DOJump(Vector3.right, 1, 1, 0.3f).SetRelative().OnComplete(() =>
        //{
        //    PoolManager.Instance.Push(this);
        //});

        Sequence seq = DOTween.Sequence();
        seq.Append(text.transform.DOMoveY(1.5f, 0.5f).SetRelative());
        seq.Join(text.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.5f));
        seq.Insert(0.3f, text.DOFade(0, 0.2f));
        seq.AppendCallback(() => PoolManager.Instance.Push(this));
    }
}
