using UnityEngine;

public class EliteMonster : MonoBehaviour
{
    public static EliteMonster instance;

    [Header("각 시간대별 엘리트 프리팹")]
    public GameObject elite3minPrefab;
    public GameObject elite7minPrefab;
    public GameObject elite11minPrefab;

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
                prefabToSpawn = elite3minPrefab;
                break;
            case 2:
                prefabToSpawn = elite7minPrefab;
                break;
            case 3:
                prefabToSpawn = elite11minPrefab;
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
            elite.SetPlayerData(playerTransform);
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
