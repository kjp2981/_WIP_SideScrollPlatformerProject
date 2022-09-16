using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DashEffect : PoolableMono
{
    private SpriteRenderer spriteRenderer;
    public SpriteRenderer SpriteRenderer
    {
        get
        {
            if(spriteRenderer == null)
            {
                spriteRenderer = GetComponent<SpriteRenderer>();
            }
            return spriteRenderer;
        }
    }

    [SerializeField]
    private Color overrideColor = Color.white;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void Reset()
    {
        
    }

    public void OnEnable()
    {
        spriteRenderer.color = overrideColor;
        spriteRenderer.DOFade(0, 0.3f).OnComplete(() => {
            spriteRenderer.color = Color.white;
            PoolManager.Instance.Push(this);
        });
    }
}
