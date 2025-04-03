using UnityEngine;

public class Enemy_Manager : MonoBehaviour
{
    public Transform player_pos;
    public GameObject[] EnemyType; //적의 종류를 받은 배열 
    public float spawnTime, currentTime; //적이 생성되는 간격과 생성을 위한 변수 
    void Start()
    {
        currentTime = 0f;
    }

    void Update()
    {
        if(currentTime >= spawnTime)
        {
            int randi = Random.Range(0,2);

            Vector3 spawnPos = new Vector3(10, 10, 0);
            GameObject EnemyObj = Instantiate(EnemyType[randi], spawnPos, Quaternion.identity);
            EnemyObj.transform.SetParent(transform);
            EnemyObj.GetComponent<Monster>().SetPlayer(player_pos);

            currentTime = 0f; //적 생성 후 초기화 
        }
        else
        {
            currentTime += Time.deltaTime;
        }
    }
}
