using UnityEngine;

public class RepositionEnemy : MonoBehaviour
{
    private Collider2D coll;
    private const float TILE_SIZE = 40f;

    void Awake()
    {
        coll = GetComponent<Collider2D>();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area")) return;

        // 적 오브젝트에만 적용되도록 (예: "Enemy" 태그)
        if (!CompareTag("Enemy")) return;

        Vector2 playerPos = GameManager.instance.player.transform.position;
        Vector2 myPos = transform.position;

        // 플레이어 위치 기준으로 방향 계산
        Vector2 diff = myPos - playerPos;
        Vector2 moveDir = Vector2.zero;

        if (Mathf.Abs(diff.x) > Mathf.Abs(diff.y))
        {
            moveDir = Vector2.right * (diff.x < 0 ? -1 : 1);
        }
        else
        {
            moveDir = Vector2.up * (diff.y < 0 ? -1 : 1);
        }

        transform.Translate(-moveDir * TILE_SIZE); // 방향 반대로 이동 (플레이어쪽으로)

        // 위치 스냅 보정
        Vector3 pos = transform.position;
        pos.x = Mathf.Round(pos.x);
        pos.y = Mathf.Round(pos.y);
        transform.position = pos;
    }
}
