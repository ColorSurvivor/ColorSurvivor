using UnityEngine;

public class SkeletonArcher : Monster
{
    public GameObject bullet; // 발사할 총알 프리팹
    float atkRange = 3.5f;
    float atkTime = 1.5f;
    float currentAtkTimer = 1.5f;

    void Update()
    {
        if (isDead || target == null) return;

        currentAtkTimer += Time.deltaTime;

        // 공격 쿨타임 체크 및 발사
        if (Vector2.Distance(transform.position, target.position) <= atkRange)
        {
            if (currentAtkTimer >= atkTime)
            {
                Vector2 fireDir = (target.position - transform.position).normalized;

                GameObject spawnBullet = Instantiate(bullet, transform.position, Quaternion.identity);
                spawnBullet.GetComponent<MonsterBullet>().bulletDMG = GetATK();

                float angle = Mathf.Atan2(fireDir.y, fireDir.x) * Mathf.Rad2Deg;
                spawnBullet.transform.rotation = Quaternion.Euler(0f, 0f, angle);

                spawnBullet.GetComponent<Rigidbody2D>().linearVelocity = fireDir * 5f;

                currentAtkTimer = 0f;
            }
        }
    }

    protected override void FixedUpdate()
    {
        if (isDead || target == null) return;

        Vector2 direction = (target.position - transform.position).normalized;
        SR.flipX = direction.x < 0;

        // 일정 거리 이상일 때만 이동
        if (Vector2.Distance(transform.position, target.position) > 3.5f)
        {
            DoMove(direction * GetSPD());
        }
        else
        {
            DoMove(Vector2.zero); // 멈춤
        }
    }
}
