using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using DG.Tweening;
using static Define;
using System;

[System.Serializable]
public class SkillInfo
{
    [SerializeField]
    private SkillDataSO _skill;
    [SerializeField]
    private float _skillCoolTime;

    public SkillDataSO Skill
    {
        get => _skill;
        set
        {
            if (_skill == value) return;
            _skill = value;
            _skillCoolTime = _skill.coolTime;
        }
    }

    public float CoolTime
    {
        get => _skillCoolTime;
        set
        {
            if (_skill == null) return;
            _skillCoolTime = value;
        }
    }

    public SkillInfo(SkillDataSO skill)
    {
        _skill = skill;
        _skillCoolTime = _skill.coolTime;
    }
}

public class SkillCollection : MonoBehaviour
{
    // 에기에 스킬 함수를 작성한다
    // 유의사항 : 스킬 SO에 적은 이름을 함수이름으로 해야한다. GetMethod로 불러오기 때문!

    private Player player;
    private AgentAttack playerAttack;
    private AgentAnimation playerAnimation;
    private SpriteRenderer playerSpriteRenderer;
    private AgentMovement playerMovement;

    [SerializeField]
    private WeaponInfo weaponInfo;

    private List<SkillInfo> _skilList = new List<SkillInfo>();
    public List<SkillInfo> SkillList => _skilList;

    [SerializeField]
    private List<SkillInfo> _weaponList = new List<SkillInfo>();
    public List<SkillInfo> WeaponList => _weaponList;
    [SerializeField]
    private GameObject weaponSlot;


    private void Awake()
    {
        player = GetComponentInParent<Player>();
        playerAttack = GetComponentInParent<AgentAttack>();
        playerSpriteRenderer = transform.parent.Find("VisualSprite").GetComponent<SpriteRenderer>();
        playerAnimation = transform.parent.Find("VisualSprite").GetComponent<AgentAnimation>();
        playerMovement = player.GetComponent<AgentMovement>();

        foreach (SkillInfo s in _skilList)
        {
            if (s.Skill != null)
            {
                s.CoolTime = s.Skill.coolTime;
            }
        }

        foreach (SkillInfo w in _weaponList)
        {
            if (w.Skill != null)
            {
                w.CoolTime = w.Skill.coolTime;
            }
        }
    }

    private void Update()
    {
        foreach (SkillInfo s in _skilList)
        {
            s.CoolTime -= Time.deltaTime;
        }

        foreach (SkillInfo w in _weaponList)
        {
            w.CoolTime -= Time.deltaTime;
        } 

        UIManager.Instance.SkillCoolTime();
    }

    public void SetWeaponSkill(SkillDataSO skill, int index)
    {
        if (weaponInfo.WeaponDataDic[WeaponType.Auxiliary] != null)
        {
            _weaponList[index].Skill = weaponInfo.WeaponDataDic[WeaponType.Auxiliary].skill;

            UIManager.Instance.UpdateWeaponSkillImage();

            weaponSlot.SetActive(true);
        }
    }

    public void UnSetWeaponSkill(int index)
    {
        if (weaponInfo.WeaponDataDic[WeaponType.Auxiliary] == null)
        {
            _weaponList[index] = null;

            UIManager.Instance.UpdateWeaponSkillImage();

            weaponSlot.SetActive(false);
        }
    }

    public void SetSkill(SkillDataSO skill, int index)
    {
        if (skill == null) return;
        if (_skilList[index].Skill == skill) return;

        _skilList[index] = new SkillInfo(skill);
        //if (isLeft == true)
        //{
        //    if (rightSkill == skill)
        //    {
        //        rightSkill = leftSkill;
        //        leftSkill = skill;
        //        UIManager.Instance.UpdateSkillImage();
        //        UIManager.Instance.SkillCoolTime();
        //        return;
        //    }
        //    leftSkill = skill;
        //    UIManager.Instance.UpdateSkillImage(true);
        //    UIManager.Instance.SkillCoolTime();
        //}
        //else
        //{
        //    if (leftSkill == skill)
        //    {
        //        leftSkill = rightSkill;
        //        rightSkill = skill;
        //        UIManager.Instance.UpdateSkillImage();
        //        UIManager.Instance.SkillCoolTime();
        //        return;
        //    }
        //    rightSkill = skill;
        //    UIManager.Instance.UpdateSkillImage(false);
        //    UIManager.Instance.SkillCoolTime();
        //}
    }

    #region 스킬 사용 함수들
    public void UseSkill(SkillDataSO skill)
    {
        if (Time.timeScale == 0) return;

        if (skill != null)
        {
            this.GetType().GetMethod(skill.name).Invoke(this, null);
        }
    }

    public void UseSkill(int index)
    {
        if (Time.timeScale == 0) return;
        if (index >= _skilList.Count) return;
        if (_skilList[index].Skill == null) return;
        if (_skilList[index].CoolTime > 0) return;

        UseSkill(_skilList[index].Skill);
        _skilList[index].CoolTime = _skilList[index].Skill.coolTime;
    }

    public void UseWeaponSkill(int index)
    {
        if (Time.timeScale == 0) return;
        if (index >= _weaponList.Count) return;
        if (_weaponList[index].Skill == null) return;
        if (_weaponList[index].CoolTime > 0) return;

        UseSkill(_weaponList[index].Skill);
        _weaponList[index].CoolTime = _weaponList[index].Skill.coolTime;
    }
    #endregion

    #region 일반 스킬들

    /// <summary>
    /// 파이어볼
    /// </summary>
    public void Fireball()
    {
        Fireball fireball = PoolManager.Instance.Pop("Fireball") as Fireball;
        //fireball.IsLeft = playerSpriteRenderer.transform.localScale.x == 1 ? true : false;
        if (playerSpriteRenderer.transform.localScale.x == 1)
        {
            fireball.IsLeft = true;
        }
        if (playerSpriteRenderer.transform.localScale.x == -1)
        {
            fireball.IsLeft = false;
        }
        //fireball.IsLeft = playerAnimation.IsFlipX ? true : false;
        fireball.transform.position = playerAttack.ArrowPos.position;
    }

    /// <summary>
    /// 슬래쉬
    /// </summary>
    public void Slash()
    {
        Slash slash = PoolManager.Instance.Pop("Slash360") as Slash;
        slash.transform.position = this.transform.position;
        Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, slashRadius, 1 << LayerMask.NameToLayer("Enemy"));
        foreach (Collider2D hitCol in hit)
        {
            if (!hitCol.CompareTag("Enemy")) continue;
            IHittable hittable = hitCol.GetComponent<IHittable>();
            if (hittable == null) continue;
            bool isCritical = player.isCritical();
            hittable.Damage(player.GetAttackDamage(true, true, isCritical) * slashDamageOffset, this.transform.parent.gameObject, true, 0.4f, isCritical);
        }
    }

    /// <summary>
    /// 화살비?(엠버 궁)
    /// </summary>
    public void ArrowRain()
    {
        Vector3 offset = playerSpriteRenderer.transform.localScale.x == 1 ? new Vector3(5, 0, 0) : new Vector3(-5, 0, 0);
        // 풀링하기
    }

    /// <summary>
    /// 익스플로션(폭발)
    /// </summary>
    public void Explosion() // 미완성
    {
        Collider2D[] col = Physics2D.OverlapCircleAll(transform.position, explosionDistance, 1 << LayerMask.NameToLayer("Enemy"));
        foreach (Collider2D hitCol in col)
        {
            if (hitCol.CompareTag("Enemy") == false) continue;
            IHittable hit = hitCol.GetComponent<IHittable>();
            if (hit == null) continue;
            bool isCritical = player.isCritical();
            hit.Damage(player.GetAttackDamage(true, true, isCritical) * explosionDamageOffset, this.transform.parent.gameObject, true, 1f, isCritical);
        }
    }

    /// <summary>
    /// 창 찍기
    /// </summary>
    public void Spear() // 변경해야함
    {
        // 변경 후보
        // 1. 3개의 유도 창
        // 2. 상하좌우 대각선에서 유저 중심에서 밖으로 창을 찌르기
        // 3. 플레이어가 바라보는 방향으러 찌르기
        // 4. 대쉬하면서 뒤에 창 쫘롸락(종려 강공격)

        Collider2D[] col = Physics2D.OverlapCircleAll(transform.position, spearDistance, 1 << LayerMask.NameToLayer("Enemy"));
        int num = 0;
        if (col.Length > 0)
        {
            foreach (Collider2D hitCol in col)
            {
                if (num >= 3) break; // 새개만 할까 아니면 범위내에 들어온 적 모두 할까?

                if (hitCol.CompareTag("Enemy") == false) continue;
                IHittable hit = hitCol.GetComponent<IHittable>();
                if (hit == null) continue;
                Spear spaer = PoolManager.Instance.Pop("Spear") as Spear;
                spaer.transform.position = hitCol.transform.position;
                num++;
            }
        }
        else
        {
            // 범위 내에 적이 없을 땐 어떻할까?
        }
    }

    /// <summary>
    /// 불 토네이도
    /// </summary>
    public void FireTornado()
    {
        Tornado tornado = PoolManager.Instance.Pop("FireTornado") as Tornado;
        Vector3 pos = transform.position;
        pos.x -= tornado.Offset;
        pos.y += .35f;
        tornado.transform.position = pos;
    }

    /// <summary>
    /// 불의 벽
    /// </summary>
    public void FireWall()
    {
        StartCoroutine(FireWallCoroutine());
    }

    private IEnumerator FireWallCoroutine()
    {
        Vector3 pos = transform.position;
        bool offset = playerSpriteRenderer.transform.localScale.x == 1 ? true : false;
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.1f);
            FireWall fireWall = PoolManager.Instance.Pop("FireWall") as FireWall;
            fireWall.transform.position = pos + new Vector3(offset == true ? -i - 1 : i + 1, 0, 0);
        }
    }
    #endregion

    #region 무기 스킬들
    public void IronShield()
    {
        // 토사체를 막는 방패 소환
        IronShield ironShield = PoolManager.Instance.Pop("IronShield") as IronShield;
        ironShield.transform.position = transform.position + (playerSpriteRenderer.transform.localScale.x == 1 ? Vector3.left : Vector3.right) + Vector3.up;
    }

    public void WoodenShield()
    {
        // 나무 방패 던지기
    }

    public void ImperialRifle()
    {
        // 플레이어 완전히 정지 => rigid 스태틱으로 바꾸기?
        playerMovement.RigidStop(RigidbodyType2D.Static); // 이거 이따구로 짜지 말기
        player.SetCC(CC.Ability);
        player.SetIsCC(true);
        CameraController.Instance.CameraZoom(4);

        Slash imperialRifle = PoolManager.Instance.Pop("ImperialRifle") as Slash;
        Vector3 localScale = playerSpriteRenderer.transform.localScale;
        localScale.x *= -1;
        imperialRifle.transform.localScale = localScale;
        imperialRifle.transform.SetPositionAndRotation(transform.position + new Vector3(0, -0.3f, 0), Quaternion.Euler(0, 0, playerSpriteRenderer.transform.localRotation.x * -60));
        imperialRifle.transform.DORotate(Vector3.zero, 0.2f, RotateMode.Fast).SetDelay(0.3f).OnComplete(() =>
        {
            Laser laser = PoolManager.Instance.Pop("Laser") as Laser;
            laser.transform.position = transform.position + (playerSpriteRenderer.transform.localScale.x == 1 ? new Vector3(-5, -.3f, 0) : new Vector3(5, -.3f, 0));

            Action action = null;
            action += () => PoolManager.Instance.Push(imperialRifle);
            action += () => CameraController.Instance.CameraZoom();
            action += () => playerMovement.RigidStop();
            action += () => player.SetCC(CC.None);
            action += () => player.SetIsCC(false);
            laser.Shoot(action);
        });
    }
    #endregion

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spearDistance);
        Gizmos.color = Color.white;

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, slashRadius);
        Gizmos.color = Color.white;
    }
#endif
}
