using UnityEngine;

public class Monster : Entity
{
    public MonsterStat statData; //몬스터의 스탯을 인스펙터에서 지정.
    public EXP expPrefab; // TODO: 비활성화 시 스폰되게 세팅해야함
    public float muliplier; //시간이 지남에 따라 레벨이 상승하고 레벨에 따라 스탯이 증가.
    protected float contactDMG;
    protected Transform target; //플레이어의 위치를 저장.

    void OnEnable()
    {
        Init();
    }
    public float GetContactDMG()
    {
        return contactDMG;
    } 
    void FixedUpdate()
    {
        if(target != null) //플레이어 데이터가 없는 경우에는 이동 X(오류 상황)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            DoMove(direction * GetSPD()); 
        }
    }
    public void SetPlayerData(Transform tgt)
    {
        target = tgt;
    }
    void OnCollisionEnter2D(Collision2D collision) //모든 적은 플레이어와 충돌 시 플레이어에게 피해.
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().HPChange(GetContactDMG() * -1);
            //충돌 딜레이도 변수로 선언해야함 
        }
    }
    void Init()
    {
        MaxHP = statData.baseMaxHP * muliplier;
        ATK = statData.baseATK * muliplier;
        contactDMG = statData.baseContactDMG * muliplier;
        CurHP = MaxHP;
    }

}

[System.Serializable]
public class MonsterStat
{
    public float baseMaxHP;
    public float baseATK;
    public float baseContactDMG;
}
