using UnityEngine;

public class Reposition : MonoBehaviour
{
    private Collider2D coll;
    private const float TILE_SIZE = 72f; // 타일이 72x72 크기이므로 이동 거리도 72 단위로 설정

    void Awake()
    {
        // 시작 시 Collider2D 컴포넌트를 캐싱해둠
        coll = GetComponent<Collider2D>();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        // Area 트리거에서 나간 경우에만 동작 (불필요한 리포지션 방지)
        if (!collision.CompareTag("Area")) return;

        // 태그가 "Tile"인 오브젝트만 재배치 대상
        if (!CompareTag("Tile")) return;

        // 플레이어와 현재 타일의 위치 벡터
        Vector2 playerPos = GameManager.instance.player.transform.position;
        Vector2 myPos = transform.position;

        // 플레이어의 현재 입력 방향 (WASD 등)
        Vector2 playerDir = GameManager.instance.player.inputVec;

        // 플레이어가 멈춰있다면 타일 재배치를 하지 않음
        // 오류 방지용 코드
        if (playerDir == Vector2.zero) return;

        // 플레이어와 타일 사이의 거리 차 계산 (가로/세로 중 어디로 더 멀어졌는지 판단용)
        float diffX = Mathf.Abs(playerPos.x - myPos.x);
        float diffY = Mathf.Abs(playerPos.y - myPos.y);

        // moveDir 초기화
        Vector2 moveDir = Vector2.zero;

        // 가로로 더 많이 벗어났다면 X축 방향으로 이동
        if (diffX > diffY)
        {
            moveDir = Vector2.right * (playerDir.x < 0 ? -1 : 1);
        }

        // 세로로 더 많이 벗어났다면 Y축 방향으로 이동
        else if (diffY > diffX)
        {
            moveDir = Vector2.up * (playerDir.y < 0 ? -1 : 1);
        }

        // diffX == diffY일 경우: 대각선 판정 → 기본적으로 X축 우선 처리
        else
        {
            moveDir = Vector2.right * (playerDir.x < 0 ? -1 : 1);
        }

        // 실제 이동 실행 (TILE_SIZE만큼 지정 방향으로 이동)
        transform.Translate(moveDir * TILE_SIZE);

        // 스냅 보정: 미세한 소수점 오차를 없애고 정확한 격자 정렬 유지
        // float 자료형이기에 위치 오류를 방지하기 위한 코드
        Vector3 pos = transform.position;
        pos.x = Mathf.Round(pos.x); // X 위치를 정수로 보정
        pos.y = Mathf.Round(pos.y); // Y 위치를 정수로 보정
        transform.position = pos;   // 보정된 위치를 최종 적용
    }
}
