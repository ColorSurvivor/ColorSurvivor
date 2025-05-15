using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    private float baseDamage;
    private ColorType color;
    private float speed;
    private Vector2 direction;
    private Rigidbody2D RD;

    public void Init(float dmg, ColorType clr, float spd)
    {
        baseDamage = dmg;
        color = clr;
        speed = spd;
    }

    void Start()
    {
        RD = GetComponent<Rigidbody2D>();
        RD.linearVelocity = direction * speed;
        StartCoroutine(SelfDestruct());
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Monster enemy = collision.GetComponent<Monster>();
            if (enemy != null)
            {
                float finalDamage = baseDamage;

                if (color == enemy.monsterColor)
                    finalDamage *= GameConstants.SameColorBonusMultiplier;
                else
                    finalDamage *= GameConstants.DifferentColorPenaltyMultiplier;

                enemy.TakeDamage(Mathf.RoundToInt(finalDamage));
            }

            Destroy(gameObject);
        }
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
