using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.Events;
using static Define;

public class AgentAttack : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private int combo = 0;
    public int Combo => combo;

    private float comboTimer = 0f;
    private float comboTime = 3f;

    private RaycastHit2D ray;
    private Vector3 offset = new Vector3(-0.5f, -0.5f);
    [SerializeField, Layer]
    private int hitLayer;

    private void Start()
    {
        spriteRenderer = Player.transform.Find("VisualSprite").GetComponent<SpriteRenderer>();
    }

    public void MeleeAttack(bool isWeak)
    {
        if(isWeak == true)
        {
            ++combo;
            Debug.Log($"근거리 약공격, combo : {combo}");
            offset = spriteRenderer.flipX == true ? new Vector2(0.5f, -0.5f) : new Vector2(-0.5f, -0.5f);
            ray = Physics2D.BoxCast(transform.position + offset, Vector2.one, 0f, Vector2.zero, 0f, 1 << hitLayer);
            if(ray.collider != null)
            {
                Debug.Log($"Name : {ray.collider.name}, Eenmy!");
            }
        }
        else
        {
            ++combo;
            Debug.Log($"근거리 강공격, combo : {combo}");
        }
    }

    public void RangeAttack(bool isWeak)
    {
        if (isWeak == true)
        {
            Debug.Log("원거리 약공격");
        }
        else
        {
            Debug.Log("원거리 강공격");
        }
    }

    private void Update()
    {
        if(combo != 0)
        {
            comboTimer += Time.deltaTime;

            if(comboTimer >= comboTime || combo >= 3)
            {
                combo = 0;
                comboTimer = 0f;
            }
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + offset, Vector2.one);
        Gizmos.color = Color.white;
    }
#endif
}
