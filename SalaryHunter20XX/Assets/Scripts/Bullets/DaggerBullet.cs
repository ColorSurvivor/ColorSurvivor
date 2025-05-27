using System.Collections;
using UnityEngine;

public class DaggerBullet : BulletBase
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
            Destroy(gameObject);
        }
        if (collision.CompareTag("Decorations")) Destroy(gameObject); //장식물에 부딪히면 삭제
    }
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
    override public void Shot()
    {
        RD = GetComponent<Rigidbody2D>();
        RD.linearVelocity = (new Vector2(Mathf.Cos(RD.rotation * Mathf.Deg2Rad), Mathf.Sin(RD.rotation * Mathf.Deg2Rad)).normalized) * SPD;
        StartCoroutine(Timer());
        
    }
}

