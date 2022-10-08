using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Tornado : PoolableMono
{
    [SerializeField]
    private int damageOffset = 10;
    [SerializeField]
    private float offset = 3f;
    public float Offset => offset;

    private Animator animator;
    private Player player;
    

    public override void Reset()
    {
        
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = Define.Player.GetComponent<Player>();
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
            bool isCritical = player.isCritical();
            hittable?.Damage(player.GetAttackDamage() * damageOffset, this.gameObject, true, 0.1f, isCritical);
        }
    }
}
