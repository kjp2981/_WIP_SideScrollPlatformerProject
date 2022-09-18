using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : PoolableMono
{
    [SerializeField]
    private int damage = 3;

    public override void Reset()
    {

    }

    public void OnEnable()
    {
        Collider2D col = Physics2D.OverlapBox(transform.position, Vector3.one, 0, 1 << LayerMask.NameToLayer("Enemy"));
        if(col != null)
        {
            if (col.CompareTag("Enemy"))
            {
                IHittable hit = col.GetComponent<IHittable>();
                if(hit != null)
                {
                    hit.Damage(damage, this.gameObject, true, 0.2f);
                }
            }
        }
    }

    public void Pooling()
    {
        PoolManager.Instance.Push(this);
    }
}
