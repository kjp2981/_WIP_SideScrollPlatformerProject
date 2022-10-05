using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Tornado : PoolableMono
{
    [SerializeField]
    private int damage = 10;

    private float offset = 3f;
    public float Offset => offset;

    private Animator animator;
    

    public override void Reset()
    {
        
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        Sequence seq = DOTween.Sequence();
        for (int i = 0; i < 2; i++)
        {
            seq.Append(transform.DOMoveX(offset * 2, 1f).SetRelative().SetEase(Ease.Linear).SetDelay(0.1f));
            seq.Append(transform.DOMoveX(-offset * 2, 1f).SetRelative().SetEase(Ease.Linear).SetDelay(0.1f));
        }
        seq.AppendCallback(() => animator.Play("Destroy"));
    }

    public void Pooling()
    {
        PoolManager.Instance.Push(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            IHittable hittable = collision.GetComponent<IHittable>();
            hittable?.Damage(damage, this.gameObject, false, 0, false);
        }
    }
}
