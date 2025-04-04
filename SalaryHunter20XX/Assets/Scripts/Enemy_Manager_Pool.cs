using System.Collections.Generic;
using UnityEngine;

public class Enemy_Manager_Pool : MonoBehaviour
{
    public Transform player_pos; // 플레이어 위치를 참조할 Transform
    public GameObject[] EnemyType; // 다양한 적 프리팹 배열
    public int poolSize = 20; // 오브젝트 풀의 초기 크기
    public float spawnTime, currentTime; // 스폰 간격 및 현재 시간 누적용 변수

    // 스폰에 사용될 타원의 반지름 (작은 타원)
    float radiusX = 20f;
    float radiusY = 12f;

    // 재배치 조건 판단에 사용될 더 큰 타원의 반지름
    float repositionRadiusX = 24f;
    float repositionRadiusY = 15f;

    // 적 오브젝트 풀 리스트
    private List<GameObject> enemyPool = new List<GameObject>();

    void Start()
    {
        CreatePool(); // 시작 시 오브젝트 풀 생성
    }

    void Update()
    {
        currentTime += Time.deltaTime; // 프레임마다 시간 누적

        // 스폰 시간 도달 시 적 생성
        if (currentTime >= spawnTime)
        {
            Spawn();
            currentTime = 0f; // 타이머 초기화
        }

        // 너무 멀리 벗어난 적들을 재배치
        RepositionFarEnemies();
    }

    // 적 오브젝트 풀 생성 (비활성화 상태로 저장)
    void CreatePool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            int rand = Random.Range(0, EnemyType.Length); // 랜덤한 적 타입 선택
            GameObject enemy = Instantiate(EnemyType[rand], Vector3.zero, Quaternion.identity); // 적 생성
            enemy.transform.SetParent(transform); // Enemy_Manager 오브젝트의 자식으로 등록
            enemy.SetActive(false); // 비활성화하여 대기 상태로 전환
            enemyPool.Add(enemy); // 풀에 추가
        }
    }

    // 비활성화된 적을 풀에서 꺼내 재사용
    GameObject GetEnemyFromPool()
    {
        foreach (GameObject e in enemyPool)
        {
            if (!e.activeInHierarchy) return e;
        }
        return null; // 남는 오브젝트가 없으면 null 반환
    }

    // 새로운 적을 타원 외곽에 스폰
    void Spawn()
    {
        GameObject enemy = GetEnemyFromPool();
        if (enemy == null) return;

        Vector2 center = player_pos.position; // 타원의 중심은 플레이어 위치
        float angle = Random.Range(0f, 2f * Mathf.PI); // 0~360도 사이 랜덤 각도

        // 타원 외곽상의 위치 계산
        float x = center.x + radiusX * Mathf.Cos(angle);
        float y = center.y + radiusY * Mathf.Sin(angle);

        enemy.transform.position = new Vector2(x, y); // 위치 설정
        enemy.GetComponent<Monster>().SetPlayer(player_pos); // 적에게 플레이어 위치 전달
        enemy.SetActive(true); // 활성화하여 게임에 등장
    }

    // 너무 멀리 벗어난 적을 다시 스폰 범위 안으로 재배치
    void RepositionFarEnemies()
    {
        Vector2 playerPos = player_pos.position;
        Vector2 inputVec = GameManager.instance.player.inputVec.normalized; // 플레이어 이동 방향

        if (inputVec == Vector2.zero) return; // 멈춰 있으면 처리하지 않음

        foreach (GameObject enemy in enemyPool)
        {
            if (!enemy.activeInHierarchy) continue; // 비활성화된 적은 제외

            Vector2 enemyPos = enemy.transform.position;
            Vector2 offset = enemyPos - playerPos;

            // 현재 위치가 큰 타원 범위를 벗어났는지 검사
            float ellipseValue = (offset.x * offset.x) / (repositionRadiusX * repositionRadiusX) +
                                 (offset.y * offset.y) / (repositionRadiusY * repositionRadiusY);

            if (ellipseValue > 1.0f) // 타원 바깥에 있을 경우
            {
                float baseAngle = Mathf.Atan2(inputVec.y, inputVec.x); // 이동 방향 각도
                float angleOffset = Random.Range(-1.05f, 1.05f); // ±60도 분산
                float finalAngle = baseAngle + angleOffset; // 최종 배치 방향

                // 기존 타원의 경계 위에 다시 위치시킴
                float newX = playerPos.x + radiusX * Mathf.Cos(finalAngle);
                float newY = playerPos.y + radiusY * Mathf.Sin(finalAngle);

                enemy.transform.position = new Vector2(newX, newY); // 위치 재지정
            }
        }
    }
}
