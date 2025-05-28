using System.Collections;
using UnityEngine;

public class SwordBullet : BulletBase
{
    private static bool healApplied = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            var target = collision.GetComponent<Monster>();
            if (target != null)
            {
                target.TakeDamage(DMG, bulletColor);

                if (bulletColor == ColorType.Blue)
                {
                    // 플레이어의 위치에서 몬스터 방향 벡터
                    Vector2 playerPos = GameManager.instance.player.transform.position;
                    Vector2 monsterPos = target.transform.position;

                    // 플레이어 -> 몬스터 방향
                    Vector2 dir = monsterPos - playerPos;
                    // 반대로 넉백: 몬스터를 플레이어와 "멀어지는" 쪽으로 밀어냄
                    target.ApplyKnockback(dir, 0.8f, 0.5f); // 방향, 힘, 넉백이 작용하는 시간
                }

                if (bulletColor == ColorType.Green && !healApplied)
                    {
                        GameManager.instance.player.HPChange(1f);
                        healApplied = true;
                    }
            }
        }
    }

        override public void Shot()
    {
        return;
    }

    void DestroySelf()
    {
        healApplied = false;
        Destroy(gameObject);
    }
}
