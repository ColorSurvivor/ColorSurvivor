using System.Collections;
using UnityEngine;

public class ExampleBullet : MonoBehaviour
{
    int DMG = 5; //탄막의 데미지
    int SPD = 10; //탄막의 속도
    Rigidbody2D RD;
    public Vector2 GoVec; //날아갈 방향

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        RD = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public void Shot() //발사
    {
        RD = GetComponent<Rigidbody2D>();
        RD.linearVelocity = GoVec.normalized*SPD; //날아갈 방향 * 속도
        StartCoroutine(Timer()); //자괴
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag =="Enemy") //적에게 맞았을시
        {
            collision.GetComponent<Entity>().HPChange(-DMG); //데미지 주고 자괴
            Destroy(gameObject);
        }
    }
    IEnumerator Timer() //너무 멀리 날아가면 자괴
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
