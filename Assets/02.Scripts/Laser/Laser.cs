using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Laser : PoolableMono
{
    [SerializeField]
    private Vector2 hitOffset = new Vector2(8, 0.2f);
    [SerializeField]
    private LayerMask hitlayer;
    [SerializeField]
    private int damage = 3;

    private Player player;

    private void Start()
    {
        player = Define.Player.GetComponent<Player>();
    }

    public override void Reset()
    {
        Vector2 scale = transform.localScale;
        scale.y = 1;
        transform.localScale = scale;
    }

    public void Shoot(Action action = null)
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOScaleY(2, 0.2f).OnComplete(() => Damage()));
        seq.AppendCallback(() => PoolManager.Instance.Push(this)).SetDelay(0.3f);
        seq.AppendCallback(() => action?.Invoke()).SetDelay(0.3f);
    }

    public void Damage()
    {
        Collider2D[] collList = Physics2D.OverlapBoxAll(transform.position, hitOffset, 0, hitlayer);

        StartCoroutine(LookAtTargets(collList));
    }

    private IEnumerator LookAtTargets(Collider2D[] list)
    {
        WaitForSeconds waitTIme = new WaitForSeconds(0.5f);
        foreach(Collider2D col in list)
        {
            CameraController.Instance.CameraTargetChange(col.transform);
            yield return waitTIme;
        }

        foreach (Collider2D col in list)
        {
            IHittable hit = col.GetComponent<IHittable>();
            if (hit != null)
            {
                hit.Damage(player.GetAttackDamage() * damage, player.gameObject, false, 0, player.isCritical());
            }
        }

        CameraController.Instance.CameraTargetChange(Define.Player.transform);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, hitOffset);
        Gizmos.color = Color.white;
    }
#endif
}
