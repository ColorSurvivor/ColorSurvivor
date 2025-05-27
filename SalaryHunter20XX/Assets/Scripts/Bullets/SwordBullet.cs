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
    public void InitBullet(int damage, float bulletSPD, int PNT, ColorType bulletcolor)
    {
        DMG = damage;
        SPD = bulletSPD;
        penetration = PNT;
        bulletColor = bulletcolor;
    }
    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
