using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class EM : PoolableMono
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private readonly int hashClose = Animator.StringToHash("close");

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void Reset()
    {

    }

    private void OnEnable()
    {

    }

    public void Pooling()
    {
        PoolManager.Instance.Push(this);
    }

    private IEnumerator PoolingCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetTrigger(hashClose);
    }

    public void FlipX(bool isFlip)
    {
        spriteRenderer.flipX = isFlip;
    }
}
