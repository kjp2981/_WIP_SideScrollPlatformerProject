using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EM : PoolableMono
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void Reset()
    {
        Vector3 scale = transform.localScale;
        scale.x = 0;
    }

    private void OnEnable()
    {
        Sequence seq = DOTween.Sequence(); // Y축으로 변경해야하나?
        seq.Append(transform.DOScaleX(0.7f, 0.2f));
        seq.Append(transform.DOScaleX(0.5f, 0.2f));
        seq.AppendCallback(() => animator.Play("EM"));
    }

    public void Pooling()
    {
        StartCoroutine(PoolingCoroutine());
    }

    private IEnumerator PoolingCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOScaleX(0.7f, 0.2f));
        seq.Append(transform.DOScaleX(0, 0.2f));
        seq.AppendCallback(() => PoolManager.Instance.Push(this));
    }

    public void FlipX(bool isFlip)
    {
        spriteRenderer.flipX = isFlip;
    }
}
