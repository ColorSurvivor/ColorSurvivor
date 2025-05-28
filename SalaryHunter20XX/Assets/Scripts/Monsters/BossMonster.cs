using UnityEngine;
using System.Collections;

public class BossMonster : Entity
{
    public MonsterStat statData;
    public ColorType fixedColor = ColorType.None;
    public ColorType[] possibleColors = { ColorType.Red, ColorType.Green, ColorType.Blue };

    public float multiplier = 2f;
    public float colorChangeInterval = 0.1f;

    protected float contactDMG;
    protected Transform target;
    protected bool isDead = false;

    Coroutine colorChangeCoroutine;

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

    protected virtual void Init()
    {
        InitStats();
        fixedColor = possibleColors[Random.Range(0, possibleColors.Length)];
        ApplyColorVisual(fixedColor);
        StartColorChange();
        CurHP = MaxHP;
    }

    protected virtual void InitStats()
    {
        MaxHP = statData.baseMaxHP * multiplier * 3f;
        ATK = statData.baseATK * multiplier * 1.5f;
        contactDMG = statData.baseContactDMG * multiplier * 1.5f;
    }

    public void SetPlayerData(Transform tgt)
    {
        target = tgt;
    }

    public float GetContactDMG()
    {
        return contactDMG;
    }

    protected virtual void FixedUpdate()
    {
        if (isDead || target == null) return;

        Vector2 direction = (target.position - transform.position).normalized;
        SR.flipX = direction.x < 0;
        DoMove(direction * GetSPD());
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().HPChange(GetContactDMG() * -1);
        }
    }

    public override void TakeDamage(int amount, ColorType attackColor)
    {
        if (isDead) return;

        float colormultiplier = ColorBaseDamage(attackColor);
        int finalDamage = Mathf.RoundToInt(amount * colormultiplier);
        CurHP -= finalDamage;

        Debug.Log($"[보스 피격] 색상: {fixedColor}, 공격색: {attackColor}, 배율: {colormultiplier}, 데미지: {finalDamage}, 체력: {CurHP}");

        if (CurHP <= 0)
            Die();
        else
            PlayHurtAnimation();
    }

    protected float ColorBaseDamage(ColorType attackColor)
    {
        if (fixedColor == ColorType.None || attackColor == fixedColor)
            return GameConstants.SameColorBonusMultiplier;

        return GameConstants.DifferentColorPenaltyMultiplier;
    }

    void StartColorChange()
    {
        if (colorChangeCoroutine != null)
            StopCoroutine(colorChangeCoroutine);
        colorChangeCoroutine = StartCoroutine(ColorChangeRoutine());
    }

    protected virtual void Die()
    {
        isDead = true;
        if (colorChangeCoroutine != null)
        {
            StopCoroutine(colorChangeCoroutine);
        }
        Ani.ResetTrigger("Hurt");
        Ani.Play("Death", 0, 0f);
        StartCoroutine(DieCoroutine());
    }

    protected virtual void PlayHurtAnimation()
    {
        Ani.ResetTrigger("Hurt");
        Ani.Play("Hurt", 0, 0f);
    }

    protected virtual IEnumerator DieCoroutine()
    {
        RD.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(0.5f);
        GameManager.instance.CallGameClearCanvas();

        gameObject.SetActive(false);
    }

    IEnumerator ColorChangeRoutine()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(colorChangeInterval);

            // 다음 색상 인덱스를 순환식으로 계산
            int currentIndex = System.Array.IndexOf(possibleColors, fixedColor);
            int nextIndex = (currentIndex + 1) % possibleColors.Length;
            fixedColor = possibleColors[nextIndex];

            ApplyColorVisual(fixedColor);
            Debug.Log($"[보스 색상 변경] 새로운 색상: {fixedColor}");
        }
    }


    protected void ApplyColorVisual(ColorType color)
    {
        switch (color)
        {
            case ColorType.Red:
                SR.color = new Color(1f, 0.25f, 0.25f); break;
            case ColorType.Green:
                SR.color = new Color(0.4f, 1f, 0.4f); break;
            case ColorType.Blue:
                SR.color = new Color(0.4f, 0.4f, 1f); break;
            default:
                SR.color = Color.white; break;
        }
    }
}