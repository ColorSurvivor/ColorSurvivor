using System.Collections;
using UnityEngine;

public class Arrow : BulletBase
{
    private int healHitCount = 0; // 이 화살로 몇 번 회복했는지 추적

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            var target = collision.GetComponent<Entity>();
            if (target != null)
            {
                target.TakeDamage(DMG, bulletColor);

                // 초록색 화살이면 플레이어 회복
                if (bulletColor == ColorType.Green)
                {
                    float healAmount = GetHealAmountByHitCount(healHitCount);

                    if (healAmount > 0)
                    {
                        GameManager.instance.player.HPChange(healAmount); // 조정된 회복량을 적용
                    }
                    healHitCount++;
                }
            }
            if (OnGGGSkill)
                target.GetComponent<Monster>().ApplySlow(1f,0.75f);
            if (penetration <= 0) Destroy(gameObject); //관통이 남아있는지 검사.
            else penetration--;
        }
        if (collision.CompareTag("Decorations")) Destroy(gameObject); //장식물에 부딪히면 삭제
    }

    override protected IEnumerator Timer()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    // 관통 횟수에 따라 회복량이 감소
    float GetHealAmountByHitCount(int hitCount)
    {
        switch (hitCount)
        {
            case 0: return 1f;    // 첫 적은 1
            case 1: return 0.5f;  // 두 번째는 0.5
            case 2: return 0.3f;  // 세 번째는 0.3
            default: return 0.1f; // 그 이상은 0.1 (혹은 0으로 해도 됨)
        }
    }
}
