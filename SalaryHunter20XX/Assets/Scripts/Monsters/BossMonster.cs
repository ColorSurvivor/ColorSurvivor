using UnityEngine;
using System.Collections;

public class BossMonster : Monster
{
    public ColorType[] possibleColors = { ColorType.Red, ColorType.Green, ColorType.Blue };
    public float colorChangeInterval;

    Coroutine colorChangeCoroutine;

    protected override void Init()
    {
        InitStats();
        monsterColor = possibleColors[Random.Range(0, possibleColors.Length)];
        ApplyColorVisual();
        StartColorChange();
        CurHP = MaxHP;
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

        Debug.Log($"[보스 피격] 색상: {monsterColor}, 공격색: {attackColor}, 배율: {colormultiplier}, 데미지: {finalDamage}, 체력: {CurHP}");

        if (CurHP <= 0)
            Die();
        else
            PlayHurtAnimation();
    }

    void StartColorChange()
    {
        if (colorChangeCoroutine != null)
            StopCoroutine(colorChangeCoroutine);
        colorChangeCoroutine = StartCoroutine(ColorChangeRoutine());
    }

    protected override void Die()
    {
        isDead = true;
        AudioManager.instance.PlayMonsterDead();
        if (colorChangeCoroutine != null)
        {
            StopCoroutine(colorChangeCoroutine);
        }
        Ani.ResetTrigger("Hurt");
        Ani.Play("Death", 0, 0f);
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(DieCoroutine());
    }

    protected override IEnumerator DieCoroutine()
    {
        RD.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(0.5f);
        GameManager.instance.CallGameClearCanvas();
    }

    IEnumerator ColorChangeRoutine()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(colorChangeInterval);

            // 다음 색상 인덱스를 순환식으로 계산
            int currentIndex = System.Array.IndexOf(possibleColors, monsterColor);
            int nextIndex = (currentIndex + 1) % possibleColors.Length;
            monsterColor = possibleColors[nextIndex];

            ApplyColorVisual();
            Debug.Log($"[보스 색상 변경] 새로운 색상: {monsterColor}");
        }
    }
}