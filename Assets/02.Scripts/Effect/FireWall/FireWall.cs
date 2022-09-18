using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWall : PoolableMono
{
    private Animator animator;

    private readonly int hashFireEnd = Animator.StringToHash("FireEnd");

    [SerializeField]
    private float fireTime = 5f;

    private float timer = 0f;
    [SerializeField]
    private float damageRate = 1f;
    [SerializeField]
    private int damage = 2;

    private bool isFire = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        timer = damageRate;
        StartCoroutine(FireCoroutine());
    }

    private void Update()
    {
        if(isFire == true)
        {
            timer += Time.deltaTime;

            if(timer >= damageRate)
            {
                Collider2D[] col = Physics2D.OverlapBoxAll(transform.position - new Vector3(0, 0.5f, 0), Vector3.one, 0f, 1 << LayerMask.NameToLayer("Enemy"));
                foreach(Collider2D hitCol in col)
                {
                    if (hitCol.CompareTag("Enemy") == false) continue;
                    IHittable hit = hitCol.GetComponent<IHittable>();
                    if (hit == null) continue;
                    hit.Damage(damage, this.gameObject);
                }
                timer = 0;
            }
        }
    }

    private IEnumerator FireCoroutine()
    {
        isFire = true;
        yield return new WaitForSeconds(fireTime);
        isFire = false;
        animator.SetTrigger(hashFireEnd);
    }

    public void Pooling()
    {
        PoolManager.Instance.Push(this);
    }

    public override void Reset()
    {
        timer = damageRate;
    }


#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position - new Vector3(0, 0.5f, 0), Vector3.one);
        Gizmos.color = Color.white;
    }

#endif
}
