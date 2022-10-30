using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyMeleeAttack : AgentAttack
{
    private Enemy enemy;
    [SerializeField]
    private float attackDelay = 1f;
    private float attackTimer = 0f;
    [SerializeField]
    private float damageDalay = 0.3f;

    public UnityEvent AttackAnim;

    protected override void Start()
    {
        spriteRenderer = transform.Find("VisualSprite").GetComponent<SpriteRenderer>();

        enemy = GetComponent<Enemy>();
    }

    public override void MeleeAttack(bool isWeak)
    {
        if (enemy.Death == true) return;

        offset = spriteRenderer.transform.localScale.x == -1 ? new Vector2(attackRange.x * 0.5f, 0) : new Vector2(attackRange.x * 0.5f * -1, 0);
        ray = Physics2D.BoxCast(transform.position + offset, attackRange, 0f, Vector2.zero, 0f, hitLayer);

        if (isWeak == true)
        {
            if (attackTimer >= attackDelay)
            {
                msCombo = 0;

                ++mwCombo;

                AttackAnim?.Invoke();
                StartCoroutine(Attack());

                attackTimer = 0;
            }
        }
        else
        {
            if (attackTimer >= attackDelay)
            {
                mwCombo = 0;

                ++msCombo;

                AttackAnim?.Invoke();
                StartCoroutine(Attack());

                attackTimer = 0f;
            }
        }
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(damageDalay);
        if (ray.collider != null)
        {
            if (ray.collider.CompareTag("Player"))
            {
                IHittable hit = ray.collider.GetComponent<IHittable>();
                bool isCritical = Random.Range(1, 101) <= enemy.Status.criticalRate;
                hit.Damage(Mathf.CeilToInt(isCritical ? enemy.Status.meleeAttack * enemy.Status.criticalDamage : enemy.Status.meleeAttack), this.gameObject, isCritlcal: isCritical);
            }
        }
    }

    private void Update()
    {
        attackTimer += Time.deltaTime;
        if (mwCombo != 0)
        {
            mwcomboTimer += Time.deltaTime;

            if (mwcomboTimer >= comboTime || mwCombo >= 3)
            {
                mwCombo = 0;
                mwcomboTimer = 0f;
            }
        }

        if (msCombo != 0)
        {
            mscomboTimer += Time.deltaTime;

            if (mscomboTimer >= comboTime || msCombo >= 3)
            {
                msCombo = 0;
                mscomboTimer = 0f;
            }
        }
    }
}
