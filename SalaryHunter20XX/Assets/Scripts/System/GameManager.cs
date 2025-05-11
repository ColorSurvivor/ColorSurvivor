using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player player;
    public EnemyManagerPool pool;

    public float curGameTime; //현재 게임 시간
    public float maxGameTime = 900f; // 최대게임진행시간

    private bool elite3minSpawned = false;
    private bool elite7minSpawned = false;
    private bool elite11minSpawned = false;
    private bool bossSpawned = false;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        curGameTime += Time.deltaTime;

        // 3분 엘리트
        if (!elite3minSpawned && curGameTime >= 180f)
        {
            EliteMonster.instance.SpawnElite(1);
            elite3minSpawned = true;
        }

        // 7분 엘리트
        if (!elite7minSpawned && curGameTime >= 420f)
        {
            EliteMonster.instance.SpawnElite(2);
            elite7minSpawned = true;
        }

        // 11분 엘리트
        if (!elite11minSpawned && curGameTime >= 660f)
        {
            EliteMonster.instance.SpawnElite(3);
            elite11minSpawned = true;
        }

        // 15분 보스
        if (!bossSpawned && curGameTime >= 900f)
        {
            // SpawnBoss();
            bossSpawned = true;
        }
    }
}
