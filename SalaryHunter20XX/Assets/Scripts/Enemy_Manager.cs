using UnityEngine;

public class Enemy_Manager : MonoBehaviour
{
    public Transform player_pos;
    public GameObject[] EnemyType; //적의 종류를 받은 배열 
    public float spawnTime, currentTime; //적이 생성되는 간격과 생성을 위한 변수 

    Vector2 center = Vector2.zero;  // 타원의 중심
    float radiusX = 6.5f;             // 가로 반지름 (a)
    float radiusY = 3.5f;             // 세로 반지름 (b)
    void Start()
    {
        currentTime = 0f;
    }

    void Update()
    {
        if(currentTime >= spawnTime)
        {
            Spawn();
        }
        else
        {
            currentTime += Time.deltaTime;
        }
    }
    void Spawn()
    {
        int randomIndex = Random.Range(0, 2); //두가지 적 중 임의의 적 선택.

        float angle = Random.Range(0f, 2f * Mathf.PI); // 0 ~ 2π 사이 각도

        // 타원 위 좌표 계산
        float x = center.x + radiusX * Mathf.Cos(angle);
        float y = center.y + radiusY * Mathf.Sin(angle);
        Vector2 spawnPos = new Vector2(x, y);
        GameObject EnemyObj = Instantiate(EnemyType[randomIndex], spawnPos, Quaternion.identity);
        EnemyObj.transform.SetParent(transform);
        EnemyObj.GetComponent<Monster>().SetPlayer(player_pos);

        currentTime = 0f; //적 생성 후 초기화 
    }
}
