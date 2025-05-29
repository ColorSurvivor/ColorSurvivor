using UnityEngine;

public class EliteMonster : MonoBehaviour
{
    public static EliteMonster instance;
    public ColorType currentColor = ColorType.None;

    [Header("각 시간대별 엘리트 프리팹")]
    public GameObject wave1Elite;
    public GameObject wave2Elite;
    public GameObject wave3Elite;
    public GameObject bossPrefab;

    [Header("스폰 위치 정보")]
    public Transform playerTransform;

    public float radiusX = 22f;
    public float radiusY = 14f;

    void Awake()
    {
        instance = this;
    }

    public void SpawnElite(int waveIndex)
    {
        GameObject prefabToSpawn = null;

        switch (waveIndex)
        {
            case 1:
                prefabToSpawn = wave1Elite;
                break;
            case 2:
                prefabToSpawn = wave2Elite;
                break;
            case 3:
                prefabToSpawn = wave3Elite;
                break;
            default:
                Debug.LogWarning("잘못된 waveIndex!");
                return;
        }

        Vector2 spawnPos = GetSpawnPosition();
        GameObject eliteObj = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);

        var elite = eliteObj.GetComponent<EliteMonsterUnit>();
        if (elite != null)
        {
            elite.monsterColor = currentColor;
            elite.SetPlayerData(playerTransform);
            elite.ApplyColorVisual();
        }
    }

    public void SpawnBoss()
    {
        Vector2 spawnPos = GetSpawnPosition();
        GameObject bossObj = Instantiate(bossPrefab, spawnPos, Quaternion.identity);

        var boss = bossObj.GetComponent<BossMonster>();
        if (boss != null)
        {
            boss.SetPlayerData(playerTransform);
        }
    }

    Vector2 GetSpawnPosition()
    {
        Vector2 center = playerTransform.position;
        float angle = Random.Range(0f, 2f * Mathf.PI);
        float x = center.x + radiusX * Mathf.Cos(angle);
        float y = center.y + radiusY * Mathf.Sin(angle);
        return new Vector2(x, y);
    }
}
