using UnityEngine;
using System.Collections;

public class EliteMonsterUnit : Monster
{
    protected override void InitStats()
    {
        MaxHP = statData.baseMaxHP * multiplier;
        ATK = statData.baseATK * multiplier;
        contactDMG = statData.baseContactDMG * multiplier;
    }

    protected override void FixedUpdate()
    {
        if (isDead || target == null) return;

        Vector2 direction = (target.position - transform.position).normalized;
        SR.flipX = direction.x < 0;
        DoMove(direction * GetSPD());
    }

    public override void TakeDamage(int amount, ColorType attackColor)
    {
        if (isDead) return;

        float colormultiplier = ColorBaseDamage(attackColor);
        int finalDamage = Mathf.RoundToInt(amount * colormultiplier);
        CurHP -= finalDamage;

        Debug.Log($"[엘리트 피격] 색상: {monsterColor}, 공격색: {attackColor}, 배율: {colormultiplier}, 데미지: {finalDamage}, 체력: {CurHP}");

        if (CurHP <= 0)
            Die();
        else
            PlayHurtAnimation();
    }

    protected override IEnumerator DieCoroutine()
    {
        RD.linearVelocity = Vector2.zero;
        GameManager.instance.canFlowGameTime = true;

        yield return new WaitForSeconds(0.5f);
        EXP expOBJ = Instantiate(expPrefab, transform.position, transform.rotation);
        expOBJ.SetFinalEXP();
        yield return new WaitForSeconds(0.1f);

        GetComponent<Collider2D>().isTrigger = false;
        Destroy(gameObject);
    }
}
