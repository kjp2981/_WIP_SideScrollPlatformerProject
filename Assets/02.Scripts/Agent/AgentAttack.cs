using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.Events;
using static Define;

public class AgentAttack : MonoBehaviour
{
    protected SpriteRenderer spriteRenderer;
    public UnityEvent OnAttackEffect = null;

    #region 콤보 체크 관련 변수
    protected int mwCombo = 0;
    public int MWCombo => mwCombo;

    protected int msCombo = 0;
    public int MSCombo => msCombo;

    private int rwCombo = 0;
    public int RWCombo => rwCombo;

    private int rsCombo = 0;
    public int RSCombo => rsCombo;

    protected float mwcomboTimer = 0f;
    protected float mscomboTimer = 0f;
    private float rwcomboTimer = 0f;
    private float rscomboTimer = 0f;
    protected float comboTime = 3f;
    #endregion

    protected RaycastHit2D ray;
    protected Vector3 offset = new Vector3(-0.5f, -0.5f);
    [SerializeField]
    protected Vector2 attackRange = new Vector2(1, 1);
    [SerializeField]
    protected LayerMask hitLayer;
    [SerializeField]
    private Transform arrowPos;
    public Transform ArrowPos => arrowPos;

    private Player player;

    protected virtual void Start()
    {
        spriteRenderer = Define.Player.transform.Find("VisualSprite").GetComponent<SpriteRenderer>();
        player = Define.Player.GetComponent<Player>();
    }

    public virtual void MeleeAttack(bool isWeak)
    {
        offset = spriteRenderer.transform.localScale.x == -1 ? new Vector2(0.5f, -0.5f) : new Vector2(-0.5f, -0.5f);
        ray = Physics2D.BoxCast(transform.position + offset, Vector2.one, 0f, Vector2.zero, 0f, hitLayer);

        if (isWeak == true)
        {
            msCombo = 0;
            rwCombo = 0;
            rsCombo = 0;

            ++mwCombo;
            
            if(ray.collider != null)
            {
                if (ray.collider.CompareTag("Enemy"))
                {
                    IHittable hit = ray.collider.GetComponent<IHittable>();
                    bool isCritical = player.isCritical();
                    hit.Damage(player.GetAttackDamage(true, false, isCritical), this.gameObject, true, 0.2f, isCritical);
                    OnAttackEffect?.Invoke();
                }
            }
        }
        else
        {
            mwCombo = 0;
            rwCombo = 0;
            rsCombo = 0;

            ++msCombo;
            if (ray.collider != null)
            {
                if (ray.collider.CompareTag("Enemy"))
                {
                    IHittable hit = ray.collider.GetComponent<IHittable>();
                    bool isCritical = player.isCritical();
                    hit.Damage(player.GetAttackDamage(true, true, isCritical), this.gameObject, true, 0.5f, isCritical);
                }
            }
        }
    }

    public void RangeAttack(bool isWeak)
    {
        if (isWeak == true)
        {
            mwCombo = 0;
            msCombo = 0;
            rsCombo = 0;

            ++rwCombo;
            // 애니메이션 후 화살 생성
            // 플레이어 방향에 맞춰 화살 뒤집기
            StartCoroutine(CreateArrow(0.2f, spriteRenderer.transform.localScale.x == 1 ? false : true));
        }
        else
        {
            mwCombo = 0;
            msCombo = 0;
            rwCombo = 0;

            ++rsCombo;
            // 애니메이션 후 화살 생성
            // 플레이어 방향에 맞춰 화살 뒤집기
            StartCoroutine(CreateArrow(0.5f, spriteRenderer.transform.localScale.x == 1 ? false : true));
        }
    }

    public IEnumerator CreateArrow(float delay, bool isFlip)
    {
        yield return new WaitForSeconds(delay);

        Arrow arrow = PoolManager.Instance.Pop("Arrow") as Arrow;
        if(isFlip == true) // 오른쪽으로 뒤집기
        {
            Vector3 vec = arrow.transform.localScale;
            vec.x = -1;
            arrow.transform.localScale = vec;
        }
        else
        {
            Vector3 vec = arrow.transform.localScale;
            vec.x = 1;
            arrow.transform.localScale = vec;
        }
        arrow.transform.position = arrowPos.position;
    }

    private void Update()
    {
        if(mwCombo != 0)
        {
            mwcomboTimer += Time.deltaTime;

            if(mwcomboTimer >= comboTime || mwCombo >= 3)
            {
                mwCombo = 0;
                mwcomboTimer = 0f;
            }
        }

        if (msCombo != 0)
        {
            mscomboTimer += Time.deltaTime;

            if (mscomboTimer >= comboTime + 2 || msCombo >= 3)
            {
                msCombo = 0;
                mscomboTimer = 0f;
            }
        }

        if (rwCombo != 0)
        {
            rwcomboTimer += Time.deltaTime;

            if (rwcomboTimer >= comboTime || rwCombo >= 3)
            {
                rwCombo = 0;
                rwcomboTimer = 0f;
            }
        }

        if (rsCombo != 0)
        {
            rscomboTimer += Time.deltaTime;

            if (rscomboTimer >= comboTime || rsCombo >= 3)
            {
                rsCombo = 0;
                rscomboTimer = 0f;
            }
        }
    }

    public void TimeDelay()
    {
        TimeManager.Instance.ModifyTimeScale(0.6f, 0, () => TimeManager.Instance.ModifyTimeScale(1f, 0.1f));
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + offset, attackRange);
        Gizmos.color = Color.white;
    }
#endif
}
