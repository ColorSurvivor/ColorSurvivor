using UnityEngine;

public class MonsterBullet : MonoBehaviour
{
    public float bulletDMG;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var target = collision.GetComponent<Player>();
            if (target != null)
            {
                collision.gameObject.GetComponent<Player>().HPChange(-bulletDMG);
                Destroy(gameObject);
            }
        }
        else if (collision.CompareTag("Decorations")) Destroy(gameObject); //장식물에 부딪히면 삭제
    }
}
