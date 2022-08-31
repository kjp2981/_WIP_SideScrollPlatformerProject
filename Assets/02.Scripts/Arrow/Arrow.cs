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

    private void Awake()
    {
        player = Define.Player.GetComponent<Player>();
    }

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
            hit.Damage(player.Status.rangeAttack, this.gameObject);
        }

        if ((collision.CompareTag("Player") == false) || (collision.CompareTag("Jump") == false) || (collision.CompareTag("Movement") == false))
        {
            PoolManager.Instance.Push(this);
        }
    }
}
