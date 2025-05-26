using System.Collections;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    protected int DMG; //탄막의 데미지
    protected int penetration;
    protected float SPD = 10; //탄막의 속도
    protected Rigidbody2D RD;
    protected Vector2 GoVec; //날아갈 방향
    public ColorType bulletColor;

    void Start()
    {
        RD = GetComponent<Rigidbody2D>();
    }

    virtual public void Shot()
    {
        RD = GetComponent<Rigidbody2D>();
        RD.linearVelocity = GoVec.normalized * SPD;
        StartCoroutine(Timer());
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // if (collision.CompareTag("Enemy"))
        // {
        //     var target = collision.GetComponent<Entity>();
        //     if (target != null)
        //     {
        //         target.TakeDamage(DMG, bulletColor);
        //     }
        //     if (penetration <= 0) Destroy(gameObject); //관통이 남아있는지 검사.
        //     else penetration--;
        // }
        if (collision.CompareTag("Decorations")) Destroy(gameObject); //장식물에 부딪히면 삭제
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }

    public void InitBullet(int damage, float bulletSPD, int PNT, Vector2 VEC, ColorType bulletcolor)
    {
        DMG = damage;
        SPD = bulletSPD;
        penetration = PNT;
        GoVec = VEC;
        bulletColor = bulletcolor;
        Shot();
    }
}
