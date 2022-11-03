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
    private SpriteRenderer spriteRenderer;

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

    [SerializeField]
    private ParticleSystem dashParticle;

    private ICrowdControl iCC;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = this.transform.Find("VisualSprite").GetComponent<SpriteRenderer>();

        moveDirection = spriteRenderer.transform.localScale.x == 1 ? new Vector2(-1, 0) : new Vector2(1, 0);

        iCC = GetComponent<ICrowdControl>();
    }

    public void Jump(float value)
    {
        if(IsGround() == true)
        {
            //rigid.AddForce(Vector2.up * (jumpPower * value), ForceMode2D.Impulse);
            if(value < 0.5f)
            {
                // 낮은 점프
                rigid.AddForce(Vector2.up * jumpPower * 0.7f, ForceMode2D.Impulse);
            }
            else
            {
                // 높은 점프
                rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            }
        }
    }

    public void Movement(float xInput)
    {
        if (iCC.isCC == true) return;

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
        if (iCC.isCC == true) return;

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
        if (iCC.isCC == true) return;

        currentVelocity = 0f;
        Vector2 pos = rigid.velocity;
        pos.x = 0f;
        rigid.velocity = pos;
    }

    public void RigidStop(RigidbodyType2D type = RigidbodyType2D.Dynamic)
    {
        rigid.bodyType = type;
    }

    private void FixedUpdate()
    {
        if (iCC.isCC == true) return;

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
