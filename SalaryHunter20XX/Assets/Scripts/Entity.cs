using UnityEngine;

public class Entity : MonoBehaviour
{
    protected int MaxHP = 10, HP = 10; //걍 넣어놓은 기본값
    public int GetMaxHP()
    {
        return MaxHP;
    }
    public int GetHP()
    {
        return HP;
    } //이 방식으로 접근해야 보안성이 확보됨.
    protected float ATK=1, RPM=1, HPReg=0, SPD=1, DFF=0, EXPM=1, GOLDM = 1, Meg = 1; //각종 능력치들, 공격력, 공속, 체젠, 이속, 방어력, 경험치배율, 돈배율, 자석범위위
    public float GetATK()
    {
        return ATK;
    }
    public float GetRPM()
    {
        return RPM;
    }
    public float GetHPReg()
    {
        return HPReg;
    }
    public float GetSPD()
    {
        return SPD;
    }
    public float GetDFF()
    {
        return DFF;
    }
    public float GetEXPM()
    {
        return EXPM;
    }
    public float GetGOLDM()
    {
        return GOLDM;
    }
    public float GetMeg()
    {
        return Meg;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected Rigidbody2D RD; //물리엔진 접근을 위한 변수
    virtual protected void Awake() 
    {
        RD = GetComponent<Rigidbody2D>();
        Debug.Log(gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void HPChange(int How) //힐도 딜도 이걸로 통합 처리
    {
        HP += How;
    }
    protected void DoMove(Vector2 Where) //이동실행 통합 함수, 이동할 방향+속도로 이동함.
    {
        RD.linearVelocity = Where;
    }
}
