using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

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
        scale.y = 0;
        transform.localScale = scale;
    }

    private void OnEnable()
    {


    }

    public void StartTween(Action action = null)
    {
        Sequence seq = DOTween.Sequence();

        // Y Axis
        seq.Append(transform.DOScaleY(0.7f, 0.2f));
        seq.Append(transform.DOScaleY(0.5f, 0.2f));
        seq.AppendCallback(() => animator.Play("EM"));
        seq.AppendCallback(() => action?.Invoke());
    }

    public void Pooling()
    {
        StartCoroutine(PoolingCoroutine());
    }

    private IEnumerator PoolingCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        Sequence seq = DOTween.Sequence();

        // Y Axis
        seq.Append(transform.DOScaleY(0.7f, 0.2f));
        seq.Append(transform.DOScaleY(0, 0.2f));
        seq.AppendCallback(() => PoolManager.Instance.Push(this));
    }

    public void FlipX(bool isFlip)
    {
        spriteRenderer.flipX = isFlip;
    }
}
