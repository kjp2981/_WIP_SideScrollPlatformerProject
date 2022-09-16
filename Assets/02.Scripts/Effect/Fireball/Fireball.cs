using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : PoolableMono
{
    private bool isLeft = false;
    public bool IsLeft { get => isLeft; set => isLeft = value; }

    [SerializeField]
    private int damage = 5;
    [SerializeField]
    private float flyPower = 2f;
    [SerializeField]
    private float flyTime = 3f;
    [SerializeField]
    private float explosionDistance = 1f;

    public override void Reset()
    {
        
    }

    private void OnEnable()
    {
        StartCoroutine(FireballDestroy());
    }

    private void Update()
    {
        transform.Translate((isLeft == true ? Vector3.left : Vector3.right) * flyPower * Time.deltaTime);
    }

    private IEnumerator FireballDestroy()
    {
        yield return new WaitForSeconds(flyTime);
        PoolManager.Instance.Push(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            IHittable hit = collision.GetComponent<IHittable>();
            hit.Damage(damage, Define.Player);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, explosionDistance);
        Gizmos.color = Color.white;
    }
#endif
}
