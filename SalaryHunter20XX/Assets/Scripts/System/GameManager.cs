using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player player;
    public ItemPoolManager itemPool;
    public PlayerSkill playerSkill;

    public GameObject mainMenuCanvas;
    public GameObject inGameCanvas;
    public GameObject gameClearCanvas;
    public GameObject playerObject;
    public static bool isRestarting = false;

    public float curGameTime; // 현재 게임 시간
    public float maxGameTime = 900f; // 최대 게임 진행 시간
    public bool canFlowGameTime = true; //엘리트 스폰 시 시간 고정 여부

    private bool elite3minSpawned = false;
    private bool elite7minSpawned = false;
    private bool elite11minSpawned = false;
    private bool bossSpawned = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (isRestarting)
        {
            isRestarting = false; // 재시작 플래그 리셋
            curGameTime = 0f;
            elite3minSpawned = false;
            elite7minSpawned = false;
            elite11minSpawned = false;
            bossSpawned = false;
            canFlowGameTime = true;

            Time.timeScale = 1f;

            if (mainMenuCanvas != null)
                mainMenuCanvas.SetActive(false);

            if (inGameCanvas != null)
                inGameCanvas.SetActive(true);

            if (playerObject != null)
                playerObject.SetActive(true);
        }
        else
        {
            Time.timeScale = 0f;
            if (mainMenuCanvas != null)
                mainMenuCanvas.SetActive(true);

            if (inGameCanvas != null)
                inGameCanvas.SetActive(false);

            if (playerObject != null)
                playerObject.SetActive(false);
        }
    }

    public void StartGame()
    {
        mainMenuCanvas.SetActive(false);
        if (inGameCanvas != null)
        {
            inGameCanvas.SetActive(true);
        }
        if (playerObject != null)
        {
            playerObject.SetActive(true);
        }

        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        isRestarting = true; // 재시작 플래그 설정
        Time.timeScale = 1f;
        AudioManager.instance.ReplayBGM();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMain()
    {
        isRestarting = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("게임 종료");
    }

    public void CallGameClearCanvas()
    {
        StartCoroutine(CallGameClearCoroutine());
    }

    private IEnumerator CallGameClearCoroutine()
    {
        yield return new WaitForSeconds(4.0f);

        Time.timeScale = 0f;
        if (gameClearCanvas != null)
        {
            gameClearCanvas.SetActive(true);
        }
    }

    void Update()
    {
        if (Time.timeScale == 0f) return;
        
        if (canFlowGameTime) curGameTime += Time.deltaTime * 3;

        if (!elite3minSpawned && curGameTime >= 150f)
        {
            EliteMonster.instance.SpawnElite(1);
            elite3minSpawned = true;
            canFlowGameTime = false;
        }

        if (!elite7minSpawned && curGameTime >= 300f)
        {
            EliteMonster.instance.SpawnElite(2);
            elite7minSpawned = true;
            canFlowGameTime = false;
        }

        if (!elite11minSpawned && curGameTime >= 450f)
        {
            EliteMonster.instance.SpawnElite(3);
            elite11minSpawned = true;
            canFlowGameTime = false;
        }

        if (!bossSpawned && curGameTime >= 600f)
        {
            EliteMonster.instance.SpawnBoss();
            bossSpawned = true;
            canFlowGameTime = false;
        }
    }
}
