using UnityEngine;

public class EliteMonster : MonoBehaviour
{
    public static EliteMonster instance;

    public GameObject elite3minPrefab;
    public GameObject elite7minPrefab;
    public GameObject elite11minPrefab;

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
        }

        if (prefabToSpawn == null)
        {
            Debug.LogWarning("프리팹 지정되지 않음");
            return;
        }

        Vector2 pos = GetSpawnPosition();
        GameObject obj = Instantiate(prefabToSpawn, pos, Quaternion.identity);

        Monster m = obj.GetComponent<Monster>();
        if (m != null)
        {
            m.multiplier = 2f; // 예: 일반 몬스터보다 2배 강하게
            m.SetPlayerData(GameManager.instance.player.transform);
        }

        Vector2 GetSpawnPosition()
        {
            Vector2 center = GameManager.instance.player.transform.position;
            float angle = Random.Range(0f, 2f * Mathf.PI);
            float radiusX = 22f;
            float radiusY = 14f;

            float x = center.x + radiusX * Mathf.Cos(angle);
            float y = center.y + radiusY * Mathf.Sin(angle);
            return new Vector2(x, y);
        }
    }
}
