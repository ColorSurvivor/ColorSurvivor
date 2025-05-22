using UnityEngine;
using System.Collections;

public class EliteMonsterUnit : Entity
{
    public MonsterStat statData;
    public GameObject PassivePrefab;
    public GameObject[] passiveDropTable;
    public ColorType fixedColor = ColorType.None;
    public float multiplier = 2f;

    protected float contactDMG;
    protected Transform target;
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

    protected virtual void Init()
    {
        InitStats();
        ApplyColorVisual(fixedColor);
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

        Debug.Log($"[엘리트 피격] 색상: {fixedColor}, 공격색: {attackColor}, 배율: {colormultiplier}, 데미지: {finalDamage}, 체력: {CurHP}");

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

    protected virtual void Die()
    {
        isDead = true;
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

        Instantiate(PassivePrefab, transform.position, Quaternion.identity);
        DropPassive();
        gameObject.SetActive(false);
    }

    void DropPassive()
    {
        if (passiveDropTable == null || passiveDropTable.Length == 0) return;

        int rand = Random.Range(0, passiveDropTable.Length);
        Instantiate(passiveDropTable[rand], transform.position, Quaternion.identity);
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
