using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : PoolableMono
{
    private bool isLeft = false;
    public bool IsLeft { get => isLeft; set => isLeft = value; }

    [SerializeField]
    private int fireballDamageOffset = 5;
    [SerializeField]
    private int explosionDamageOffset = 8;
    [SerializeField]
    private float flyPower = 2f;
    [SerializeField]
    private float flyTime = 3f;
    [SerializeField]
    private float explosionDistance = 1f;

    private Vector3 direction = Vector3.left;
    private SpriteRenderer spriteRenderer = null;
    bool flipX = false;

    private Player player;

    public override void Reset()
    {
        
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = Define.Player.GetComponent<Player>();
    }

    private void OnEnable()
    {
        direction = isLeft == true ? Vector3.left : Vector3.right;
        flipX = isLeft == true ? true : false;

        spriteRenderer.flipX = flipX;

        StartCoroutine(FireballDestroy());
    }

    private void Update()
    {
        transform.Translate(direction * flyPower * Time.deltaTime);
    }

    private void Explosion()
    {
        StopAllCoroutines();

        Collider2D[] col = Physics2D.OverlapCircleAll(transform.position, explosionDistance, 1 << LayerMask.NameToLayer("Enemy"));
        Slash explosion = PoolManager.Instance.Pop("Explosion") as Slash;
        Quaternion rot = Quaternion.Euler(0, 0, Random.Range(0, 361));
        explosion.transform.SetPositionAndRotation(this.transform.position, rot);

        if (col.Length > 0)
        {
            foreach (Collider2D hitCol in col)
            {
                IHittable hit = hitCol.GetComponent<IHittable>();
                if (hit == null) continue;
                if (hitCol.CompareTag("Enemy") == false) continue;
                bool isCritical = player.isCritical();
                hit.Damage(player.GetAttackDamage(true, true, isCritical) * explosionDamageOffset, this.gameObject, true, 0.7f, isCritical);
            }
        }
        PoolManager.Instance.Push(this);
    }

    private IEnumerator FireballDestroy()
    {
        yield return new WaitForSeconds(flyTime);
        //StartCoroutine(Explosion());
        Explosion();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            IHittable hit = collision.GetComponent<IHittable>();
            bool isCritical = player.isCritical();
            hit.Damage(player.GetAttackDamage(false, false, isCritical) * fireballDamageOffset, this.gameObject, true, 0.3f, isCritical);
            //StartCoroutine(Explosion());
            Explosion();
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
