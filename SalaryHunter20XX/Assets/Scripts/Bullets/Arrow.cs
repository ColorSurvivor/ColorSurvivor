using UnityEngine;

public class Arrow : BulletBase
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            var target = collision.GetComponent<Entity>();
            if (target != null)
            {
                target.TakeDamage(DMG, bulletColor);
            }
            if (penetration <= 0) Destroy(gameObject); //관통이 남아있는지 검사.
            else penetration--;
        }
        if (collision.CompareTag("Decorations")) Destroy(gameObject); //장식물에 부딪히면 삭제
    }
}
