using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player player;
    public EnemyManagerPool pool;

    public float curGameTime; //현재 게임 시간
    public float maxGameTime = 300f; // 최대게임진행시간

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        curGameTime += Time.deltaTime;
    }
}
