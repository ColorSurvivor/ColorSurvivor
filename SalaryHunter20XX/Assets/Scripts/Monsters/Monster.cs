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
    float contactDMGCooldown = 0.3f; // 충돌 피해 간격 (초)
    float lastContactTime = -999f;

    protected bool isDead = false;

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

        Vector2 direction = (target.position - transform.position).normalized;

        if (direction.x < 0)//플레이어 방향 바라보기
            SR.flipX = true;
        else
            SR.flipX = false;

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
        AudioManager.instance.PlayMonsterHurt();
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
        isDead = true;
        AudioManager.instance.PlayMonsterDead();
        Ani.ResetTrigger("Hurt");
        Ani.Play("Death", 0, 0f);
        GetComponent<Collider2D>().isTrigger = true;
        StartCoroutine(DieCoroutine());
    }

    protected virtual void PlayHurtAnimation()
    {
        Ani.ResetTrigger("Hurt");
        Ani.Play("Hurt", 0, 0f);
    }

    protected virtual IEnumerator DieCoroutine()
    {
        Debug.Log("몬스터 사망!");

        RD.linearVelocity = Vector2.zero;

        yield return new WaitForSeconds(0.5f);
        EXP expOBJ = Instantiate(expPrefab,transform.position,transform.rotation);
        expOBJ.SetFinalEXP();
        gameObject.SetActive(false);
    }
    
    protected virtual void Init()
    {
        InitStats();
        InitColor();
        CurHP = MaxHP;
    }

    protected virtual void InitStats()
    {
        MaxHP = statData.baseMaxHP * multiplier;
        ATK = statData.baseATK * multiplier;
        contactDMG = statData.baseContactDMG * multiplier;
    }

    float GetColorAppearanceChance()
    {
        float t = GameManager.instance.curGameTime;

        if (t < 120f)
            return 0f;

        if (t < 360f) // 3~6분: 0 → 20%
            return Mathf.Lerp(0f, 0.2f, (t - 120f) / 240f);

        if (t < 600f) // 7~10분: 20% → 40%
            return Mathf.Lerp(0.2f, 0.4f, (t - 360f) / 240f);

        if (t < 900f) // 11~15분: 40% → 75%
            return Mathf.Lerp(0.4f, 0.75f, (t - 600f) / 300f);

        return 0.75f; // 15분 이후 고정
    }

    void InitColor()
    {
        float chance = GetColorAppearanceChance(); //시간에 따른 확률을 가져옴
        float roll = Random.Range(0f, 1f); 

        if (roll > chance) //확률보다 크면 무색.
        {
            SetColor(ColorType.None); // 무색
            return;
        }

        int randomValue = Random.Range(0, 3); //확률보다 작으면 색을 가짐.
        ColorType randomColor = (ColorType)randomValue;
        SetColor(randomColor);
    }

    protected void SetColor(ColorType color) //todo: 다른 곳에서 호출 하는 데가 없으니 initcolor 안으로 이동제안
    {
        monsterColor = color;
        ApplyColorVisual(color);
    }

    protected void ApplyColorVisual(ColorType color)
    {
        switch (color)
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
}

[System.Serializable]
public class MonsterStat
{
    public float baseMaxHP;
    public float baseATK;
    public float baseContactDMG;
}
