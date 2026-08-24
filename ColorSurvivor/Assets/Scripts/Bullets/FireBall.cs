using System.Collections;
using UnityEngine;

public class FireBall : BulletBase
{
    Animator Ani;
    bool canExplode = true;

    void Awake()
    {
        Ani = GetComponent<Animator>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Decorations"))
        {
            explode();
        }
    }
    override protected IEnumerator Timer()
    {
        yield return new WaitForSeconds(1.5f);
        explode();
    }

    void explode()
    {
        if (canExplode)
        {
            Ani.SetTrigger("explode");
            canExplode = false;

            bool healApplied = false;

            Collider2D[] collidersInRange = Physics2D.OverlapCircleAll(transform.position, 1.1f);

            foreach (Collider2D collider in collidersInRange)
            {
                if (collider.CompareTag("Enemy"))
                {
                    collider.GetComponent<Entity>().TakeDamage(DMG, bulletColor);

                    if (bulletColor == ColorType.Green && !healApplied)
                    {
                        GameManager.instance.player.HPChange(1f);
                        healApplied = true;
                    }
                    if (OnGGGSkill)
                        collider.GetComponent<Monster>().ApplySlow(1f,0.75f);
                }
            }
            RD.linearVelocity = Vector2.zero;
        }
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
