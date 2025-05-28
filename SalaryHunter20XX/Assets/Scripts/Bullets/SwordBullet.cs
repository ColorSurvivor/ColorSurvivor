using System.Collections;
using UnityEngine;

public class SwordBullet : BulletBase
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
        }
    }

        override public void Shot()
    {
        return;
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
