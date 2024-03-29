using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Arrow : PoolableMono
{
    private Player player;

    [SerializeField]
    private float arrowSpeed;
    [SerializeField]
    private float destroyDelay;

    private void OnEnable()
    {
        StartCoroutine(DestroyArrow(destroyDelay));
    }

    private void Update()
    {
        if(transform.localScale.x == -1)
        {
            transform.Translate(Vector3.right * arrowSpeed * Time.deltaTime);
        }
        else if(transform.localScale.x == 1)
        {
            transform.Translate(Vector3.left * arrowSpeed * Time.deltaTime);
        }
    }

    private IEnumerator DestroyArrow(float delay)
    {
        yield return new WaitForSeconds(delay);

        PoolManager.Instance.Push(this);
    }

    public override void Reset()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            IHittable hit = collision.GetComponent<IHittable>();
            if(player == null)
            {
                player = Define.Player.GetComponent<Player>();
            }
            bool isCritical = player.isCritical();
            hit.Damage(player.GetAttackDamage(false, false, isCritical), this.gameObject, true, 0.1f, isCritical);
        }

        if ((collision.CompareTag("Player") == false) && (collision.CompareTag("Jump") == false) && (collision.CompareTag("Movement") == false))
        {
            BloodParticle splashParticle = PoolManager.Instance.Pop("SplashParticle") as BloodParticle;
            float value = transform.position.x < collision.transform.position.x ? 1f : -1f;
            splashParticle.SetLocalScaleX(value);
            splashParticle.transform.position = collision.transform.position + (value == 1 ? Vector3.left : Vector3.right);

            IHittable hittable = collision.GetComponent<IHittable>();
            hittable?.Damage(1, this.gameObject);

            PoolManager.Instance.Push(this);
        }
    }
}
