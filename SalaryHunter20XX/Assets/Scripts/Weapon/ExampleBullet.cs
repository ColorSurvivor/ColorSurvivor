using System.Collections;
using UnityEngine;

public class ExampleBullet : MonoBehaviour
{
    public int DMG = 5; //탄막의 데미지
    int SPD = 10; //탄막의 속도
    Rigidbody2D RD;
    public Vector2 GoVec; //날아갈 방향
    public ColorType bulletColor = ColorType.Red; // 임시 설정

    void Start()
    {
        RD = GetComponent<Rigidbody2D>();
    }

    public void Shot()
    {
        RD = GetComponent<Rigidbody2D>();
        RD.linearVelocity = GoVec.normalized * SPD;
        StartCoroutine(Timer());
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            var target = collision.GetComponent<Entity>();
            if (target != null)
            {
                if (collision.GetComponent<Monster>().monsterColor == bulletColor)
                    DMG = (int)(DMG*1.5f);
                else
                    DMG = (int)(DMG*0.75f);
                target.TakeDamage(DMG, bulletColor);
            }

            Destroy(gameObject);
        }
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
