using UnityEngine;

public class Entity : MonoBehaviour
{
    protected float MaxHP = 10f, CurHP = 10f, ATK=1f, SPD=1f; //최대체력, 현재체력, 공격력, 이속,
    
    public float GetMaxHP()
    {
        return MaxHP;
    }
    public float GetCurHP()
    {
        return CurHP;
    } //이 방식으로 접근해야 보안성이 확보됨.
    
    public float GetATK()
    {
        return ATK;
    }
    public float GetSPD()
    {
        return SPD;
    }
    
    protected Rigidbody2D RD; //물리엔진 접근을 위한 변수
    protected SpriteRenderer SR; //스프라이트 접근을 위한 변수
    protected Animator Ani; //애니메이션 접근을 위한 변수
    virtual protected void Awake() 
    {
        RD = GetComponent<Rigidbody2D>();
        SR = GetComponent<SpriteRenderer>();
        Ani = GetComponent<Animator>();
        Debug.Log(gameObject.name);
    }

    public void HPChange(float How) //힐도 딜도 이걸로 통합 처리
    {
        CurHP += How;
    }
    protected void DoMove(Vector2 Where) //이동실행 통합 함수, 이동할 방향+속도로 이동함.
    {
        RD.linearVelocity = Where;
    }
}
