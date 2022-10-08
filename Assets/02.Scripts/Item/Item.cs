using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class Item : PoolableMono
{
    public abstract void UseItem();

    [SerializeField]
    protected float speed = 2f;

    protected bool isArrival = false;

    public override void Reset()
    {
        isArrival = false;
    }

    public virtual void Scattering()
    {
        Vector3 offset = Random.insideUnitCircle * 2;
        transform.DOMove(offset, 0.3f).SetEase(Ease.OutQuad).SetRelative().OnComplete(() =>
        {
            StartCoroutine(ChasePlayer());
        });
    }

    protected virtual IEnumerator ChasePlayer()
    {
        yield return new WaitForSeconds(0.2f);

        Vector3 dir = Vector2.zero;
        while (isArrival == false)
        {
            dir = Define.Player.transform.position - this.transform.position;
            dir.Normalize();

            transform.position += dir * speed * Time.deltaTime;

            if (Vector3.Distance(transform.position, Define.Player.transform.position) < 0.3f)
            {
                isArrival = true;
            }
            yield return null;
        }

        UseItem();
    }
}