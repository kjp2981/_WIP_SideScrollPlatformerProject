using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static Define;

public enum DashType
{
    Front,
    Back
}

public class AgentMovement : MonoBehaviour
{
    private Rigidbody2D rigid;

    [SerializeField]
    private MovementDataSO movementData = null;

    public UnityEvent<float> OnVelocityChange;
    public UnityEvent<float> OnJumpAnimation;
    public UnityEvent<bool> OnDashAnimation;

    [SerializeField]
    private float jumpPower = 1f;
    [SerializeField]
    private float dashPower = 2f;
    [SerializeField]
    private float dashTime = 0.5f;

    private float currentVelocity = 3f;
    private Vector2 moveDirection = Vector2.zero;

    private bool isDash = false;
    public bool IsDash => isDash;

    private DashType dashType = DashType.Front;

    #region 무지설 플링 대쉬 이펙트의 흔적
    private SpriteRenderer spriteRenderer;
    private float dashEffectTimer = 0f;
    [SerializeField, Tooltip("대쉬 이펙트가 생성되는 주기")]
    private float dashEffectTime = 0.3f;
    #endregion

    [SerializeField]
    private ParticleSystem dashParticle;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = this.transform.Find("VisualSprite").GetComponent<SpriteRenderer>();
        dashEffectTimer = dashEffectTime;

        moveDirection = spriteRenderer.transform.localScale.x == 1 ? new Vector2(-1, 0) : new Vector2(1, 0);
    }

    public void Jump(float value)
    {
        if(IsGround() == true)
            rigid.AddForce(Vector2.up * (jumpPower * value), ForceMode2D.Impulse);
    }

    public void Movement(float xInput)
    {
        if(xInput != 0)
        {
            if(xInput != moveDirection.x)
            {
                currentVelocity = 0f;
            }
            moveDirection.x = xInput;
        }
        currentVelocity = CulculateSpeed(xInput);
    }

    public void Dash(float dash)
    {
        if(dash <= 0.35f)
        {
            dashType = DashType.Back;
            // bacldash
            StartCoroutine(BackdashCoroutine());
        }
        else
        {
            dashType = DashType.Front;
            StartCoroutine(DashCoroutine(dashTime));
        }
    }

    private IEnumerator BackdashCoroutine()
    {
        isDash = true;
        // back dash
        yield return new WaitForSeconds(0.2f);
        StopPlayer();
        isDash = false;
    }

    private IEnumerator DashCoroutine(float time)
    {
        isDash = true;
        dashParticle.gameObject.SetActive(true);
        if(currentVelocity < 0.1f)
        {
            rigid.AddForce((spriteRenderer.transform.localScale.x == 1 ? Vector2.left : Vector2.right) * dashPower * 1.5f, ForceMode2D.Impulse);
        }
        OnDashAnimation?.Invoke(isDash);
        yield return new WaitForSeconds(time);
        isDash = false;
        OnDashAnimation?.Invoke(isDash);
    }

    private float CulculateSpeed(float xInput)
    {
        if(xInput != 0)
        {
            currentVelocity += movementData.acceleration * Time.deltaTime;
        }
        else
        {
            currentVelocity -= movementData.deAcceleration * Time.deltaTime;
        }

        return Mathf.Clamp(currentVelocity, 0, movementData.maxSpeed);
    }

    public bool IsGround()
    {
        return rigid.velocity.y == 0f;
    }

    public void StopPlayer()
    {
        currentVelocity = 0f;
        Vector2 pos = rigid.velocity;
        pos.x = 0f;
        rigid.velocity = pos;
    }

    private void FixedUpdate()
    {
        OnVelocityChange?.Invoke(moveDirection.x);

        Vector2 velocity = rigid.velocity;
        if(isDash == true)
        {
            switch (dashType)
            {
                case DashType.Front:
                    velocity.x = moveDirection.x * dashPower;
                    break;
                case DashType.Back:
                    velocity.x = -moveDirection.x * dashPower;
                    break;
            }
        }
        else
        {
            velocity.x = moveDirection.x * currentVelocity;
        }
        
        
        rigid.velocity = velocity;

        if (IsGround() == true)
        {
            OnJumpAnimation?.Invoke(rigid.velocity.y);
        }
    }
}
