using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : AgentAttack
{
    private Enemy enemy;

    protected override void Start()
    {
        spriteRenderer = transform.Find("VisualSprite").GetComponent<SpriteRenderer>();

        enemy = GetComponent<Enemy>();
    }

    public override void MeleeAttack(bool isWeak)
    {
        offset = spriteRenderer.transform.localScale.x == -1 ? new Vector2(0.5f, 0) : new Vector2(-0.5f, 0);
        ray = Physics2D.BoxCast(transform.position + offset, Vector2.one, 0f, Vector2.zero, 0f, hitLayer);

        if (isWeak == true)
        {
            msCombo = 0;

            ++mwCombo;

            if (ray.collider != null)
            {
                if (ray.collider.CompareTag("Player"))
                {
                    IHittable hit = ray.collider.GetComponent<IHittable>();
                    hit.Damage(enemy.Status.meleeAttack, this.gameObject);
                }
            }
        }
        else
        {
            mwCombo = 0;

            ++msCombo;
            if (ray.collider != null)
            {
                if (ray.collider.CompareTag("Player"))
                {
                    Debug.Log($"근거리 강공격, combo : {mwCombo}");
                    IHittable hit = ray.collider.GetComponent<IHittable>();
                    hit.Damage(Mathf.CeilToInt(enemy.Status.meleeAttack * 1.5f), this.gameObject);
                }
            }
        }
    }

    private void Update()
    {
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
