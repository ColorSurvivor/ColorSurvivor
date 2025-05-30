using UnityEngine;
using System.Collections;

public class Player : Entity
{
    protected float ATSpd = 1f, HPReg =0f, DEF = 0f, EXPM = 1f, GOLDM = 1f, Mag = 1f; //공속, 체젠, 방어력, 경험치배율, 돈배율, 자석범위
    float curEXP = 0, MaxEXP = 15;
    int LV = 1;
    protected bool isDead;
    public GameObject gameOverCanvas;
    public int GetLV()
    {
        return LV;
    }
    public float GetATSpd()
    {
        return ATSpd;
    }
    public float GetHPReg()
    {
        return HPReg;
    }
    public float GetDEF()
    {
        return DEF;
    }
    public float GetEXPM()
    {
        return EXPM;
    }
    public float GetGOLDM()
    {
        return GOLDM;
    }
    public float GetMag()
    {
        return Mag;
    }
    public float GetCurEXP()
    {
        return curEXP;
    }
    public float GetMaxEXP()
    {
        return MaxEXP;
    }
    public Vector2 inputVec; // 이동방식 변경에 따른 추가
    public GameObject LevelUPOb;
    public GameObject weaponHanger;
    void Start()
    {
        // 플레이어의 이동 속도 설정 (SPD는 Entity에서 상속받은 이동속도 변수)
        SPD = 3;
        Ani = GetComponent<Animator>();
        isDead = false;
        CurHP = MaxHP;
    }

    void Update()
    {
        if (isDead)
        {
            DoMove(Vector2.zero);
            return;
        }
        // 매 프레임마다 입력 벡터를 초기화 (이전 프레임의 방향을 제거)
        inputVec = Vector2.zero;

        // 마우스 위치를 받아옴
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // WASD 키 입력을 감지하고 그에 따라 방향 벡터를 누적함
        if (Input.GetKey(KeyCode.W)) inputVec += Vector2.up;     // 위쪽 (0, 1)
        if (Input.GetKey(KeyCode.S)) inputVec += Vector2.down;   // 아래 (-0, -1)
        if (Input.GetKey(KeyCode.A)) inputVec += Vector2.left;   // 왼쪽 (-1, 0)
        if (Input.GetKey(KeyCode.D)) inputVec += Vector2.right;  // 오른쪽 (1, 0)

        // 대각선 입력 시 이동 속도 보정을 위해 방향 벡터를 정규화 (길이를 1로 맞춤)
        inputVec = inputVec.normalized;
        Ani.SetFloat("Move", inputVec.magnitude);

        // Entity에서 정의한 DoMove() 함수 호출 → Rigidbody2D의 linearVelocity에 적용됨
        // 최종적으로 (방향 * 속도)로 이동이 실행됨
        DoMove(inputVec * SPD);

        // 마우스의 위치가 플레이어의 X 좌표보다 작으면 flipX를 true로 변경
        // 반대의 경우에는 작동하지 않음
        if (mouseWorldPos.x < transform.position.x)
        {
            SR.flipX = true;
        }
        else
        {
            SR.flipX = false;
        }

        // 애니메이션 설정
        Ani.SetFloat("Move", inputVec.magnitude);

        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            print("!");
            getEXP(1500);
        }
    }
    public override void HPChange(float How) //힐도 딜도 이걸로 통합 처리. 플레이어의 데미지는 방어력을 계산해서 적용.
    {
        if (isDead)
        {
            return;
        }

        if (How < 0) //데미지의 경우
        {
            CurHP += How;

            if (CurHP > 0)
            {
                AudioManager.instance.PlayPlayerHurt();
                Ani.SetTrigger("Hurt");
            }
            else
            {
                CurHP = 0;
                Dead();
            }
        }
        else
        {
            if (CurHP + How >= MaxHP) CurHP = MaxHP; //최대체력까지만 회복
            else CurHP += How; //회복은 그대로 적용.
        }
    }
    
    protected void Dead()
    {
        isDead = true;

        AudioManager.instance.PlayPlayerDead();

        Ani.SetBool("Death",true);

        StartCoroutine(DieCoroutine());
    }

    protected virtual IEnumerator DieCoroutine()
    {
        yield return new WaitForSecondsRealtime(2.0f);

        Time.timeScale = 0f;
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(true);
        }
    }

    public void getEXP(float amount)
    {
        curEXP += amount * EXPM;
        CheckLevelup();
    }

    public void CheckLevelup()
    {
        if (curEXP > MaxEXP) //경험치 초과
        {
            curEXP -= MaxEXP; //최대치만큼 소거
            LV++;
            MaxEXP = SetMaxEXP();
            AudioManager.instance.PlayPlayerLevelUp();
            Instantiate(LevelUPOb, GameObject.Find("Canvas").transform);
            Time.timeScale = 0;
        }
    }

    float SetMaxEXP()
    {
        if (LV <= 5) return MaxEXP *= 1.4f;
        else if (LV < 10) return MaxEXP *= 1.2f;
        else if (LV < 15) return MaxEXP *= 1.1f;
        else if (LV < 30) return MaxEXP *= 1.05f;
        else return MaxEXP *= 1.03f;
    }
    public void GetWeapon(WeaponData newWeapon)
    {
        GameObject temp = Instantiate(newWeapon.weaponPrefab);
        temp.GetComponent<BaseGun>().Init(newWeapon);
        GameManager.instance.itemPool.curWeapons.Add(temp);
        temp.transform.SetParent(GameManager.instance.player.GetComponent<Player>().weaponHanger.transform);
        temp.transform.position = transform.position;
    }
}
