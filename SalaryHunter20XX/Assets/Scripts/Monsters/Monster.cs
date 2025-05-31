using UnityEngine;
using System.Collections;

public class Monster : Entity
{
    public ColorType monsterColor = ColorType.None;
    public MonsterStat statData; //몬스터의 스탯을 인스펙터에서 지정.
    public EXP expPrefab;
    public float multiplier; //시간이 지남에 따라 레벨이 상승하고 레벨에 따라 스탯이 증가.
    protected float contactDMG;
    protected Transform target; //플레이어의 위치를 저장.
    protected Coroutine slowCoroutine;
    protected float baseSPD;
    float contactDMGCooldown = 0.3f; // 충돌 피해 간격 (초)
    float lastContactTime = -999f;

    protected bool isDead = false;
    protected bool isKnockback = false;
    protected float knockbackTimer = 0f;

    protected override void Awake()
    {
        base.Awake();
        Ani = GetComponent<Animator>();
    }

    void OnEnable()
    {
        isDead = false;
        Init();
    }
    public float GetContactDMG()
    {
        return contactDMG;
    }

    public float ColorBaseDamage(ColorType attackColor)
    {
        if (monsterColor == ColorType.None)
        {
            return GameConstants.SameColorBonusMultiplier;
        }

        if (attackColor == monsterColor)
        {
            return GameConstants.SameColorBonusMultiplier;
        }

        return GameConstants.DifferentColorPenaltyMultiplier;
    }

    protected virtual void FixedUpdate()
    {
        if (isDead || target == null) return; //죽은 상태이거나 플레이어 데이터가 없는 경우에는 이동 X(오류 상황)

        if (isKnockback)
        {
            knockbackTimer -= Time.fixedDeltaTime;
            if (knockbackTimer <= 0f)
            {
                isKnockback = false;
            }
            return;
        }

        Vector2 direction = (target.position - transform.position).normalized;
        SR.flipX = direction.x < 0;
        DoMove(direction * GetSPD());

    }
    public void SetPlayerData(Transform tgt)
    {
        target = tgt;
    }

    void OnCollisionStay2D(Collision2D collision) //모든 적은 플레이어와 충돌 시 플레이어에게 피해. 나중에 STAY로 바꿀 것.
    {
        if (isDead) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            if (Time.time - lastContactTime >= contactDMGCooldown)
            {
                collision.gameObject.GetComponent<Player>().HPChange(GetContactDMG() * -1); //TODO
                lastContactTime = Time.time;
            }
        }
    }

    public override void TakeDamage(int amount, ColorType attackColor)
    {
        if (isDead) return;

        float colormultiplier = ColorBaseDamage(attackColor);
        int finalDamage = Mathf.RoundToInt(amount * colormultiplier);
        CurHP -= finalDamage;

        Debug.Log($"피격! 몬스터색: {monsterColor}, 공격색: {attackColor}, 배율: {colormultiplier}, 실제 데미지: {finalDamage}, 현재 체력: {CurHP}");

        if (CurHP <= 0)
        {
            Die();
        }
        else
        {
            PlayHurtAnimation();
        }
    }

    protected virtual void Die()
    {
        if (slowCoroutine != null)
        {
            StopCoroutine(slowCoroutine);
            SPD = GetSPD();
            slowCoroutine = null;
        }

        isDead = true;
        AudioManager.instance.PlayMonsterDead();
        Ani.ResetTrigger("Hurt");
        Ani.Play("Death", 0, 0f);
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(DieCoroutine());
    }

    protected virtual void PlayHurtAnimation()
    {
        Ani.ResetTrigger("Hurt");
        AudioManager.instance.PlayMonsterHurt();
        Ani.Play("Hurt", 0, 0f);
    }

    protected virtual IEnumerator DieCoroutine()
    {
        RD.linearVelocity = Vector2.zero;

        yield return new WaitForSeconds(0.5f);
        EXP expOBJ = Instantiate(expPrefab, transform.position, transform.rotation);
        expOBJ.SetFinalEXP();
        gameObject.SetActive(false);
    }

    protected virtual void Init()
    {
        InitStats();
        ApplyColorVisual();
        CurHP = MaxHP;
    }

    protected virtual void InitStats()
    {
        MaxHP = statData.baseMaxHP * multiplier;
        ATK = statData.baseATK * multiplier;
        contactDMG = statData.baseContactDMG * multiplier;
        baseSPD = GetSPD();
        SPD = baseSPD;
    }

    public void ApplyColorVisual()
    {
        switch (monsterColor)
        {
            case ColorType.Red:
                SR.color = new Color(1f, 0.25f, 0.25f);
                break;
            case ColorType.Green:
                SR.color = new Color(0.4f, 1f, 0.4f);
                break;
            case ColorType.Blue:
                SR.color = new Color(0.4f, 0.4f, 1f);
                break;
            default:
                SR.color = Color.white;
                break;
        }
    }

    public void ApplySlow(float duration, float slowRate)
    {
        if (slowCoroutine != null)
            StopCoroutine(slowCoroutine);

        SPD = baseSPD;
        slowCoroutine = StartCoroutine(SlowCoroutine(duration, slowRate));
    }

    public void ApplyKnockback(Vector2 direction, float force, float duration)
    {
        RD.linearVelocity = Vector2.zero;
        RD.AddForce(direction.normalized * force, ForceMode2D.Impulse);

        isKnockback = true;
        knockbackTimer = duration;
    }

    private IEnumerator SlowCoroutine(float duration, float slowRate)
    {
        SPD = baseSPD * slowRate;

        yield return new WaitForSeconds(duration);

        SPD = baseSPD;
        slowCoroutine = null;
    }

    void OnDisable()
    {
        if (slowCoroutine != null)
        {
            StopCoroutine(slowCoroutine);
            SPD = GetSPD();
            slowCoroutine = null;
        }
    }
}

[System.Serializable]
public class MonsterStat
{
    public int code;
    public float baseMaxHP;
    public float baseATK;
    public float baseContactDMG;
}
