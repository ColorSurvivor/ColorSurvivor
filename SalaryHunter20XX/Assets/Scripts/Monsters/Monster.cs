using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class Monster : Entity
{
    public ColorType monsterColor;
    public MonsterStat statData; //몬스터의 스탯을 인스펙터에서 지정.
    public EXP expPrefab;
    public float multiplier; //시간이 지남에 따라 레벨이 상승하고 레벨에 따라 스탯이 증가.
    protected float contactDMG;
    protected Transform target; //플레이어의 위치를 저장.

    protected bool isDead = false;

    protected override void Awake()
    {
        base.Awake();
        Ani = GetComponent<Animator>();
    }

    void OnEnable()
    {
        isDead = false;
        Init();
    }
    public float GetContactDMG()
    {
        return contactDMG;
    } 
    protected virtual void FixedUpdate()
    {
        if(isDead || target == null) return; //죽은 상태이거나 플레이어 데이터가 없는 경우에는 이동 X(오류 상황)
    
        Vector2 direction = (target.position - transform.position).normalized;
        
        if (direction.x < 0)//플레이어 방향 바라보기
            SR.flipX = true;
        else
            SR.flipX = false;
        
        DoMove(direction * GetSPD()); 
        
    }
    public void SetPlayerData(Transform tgt)
    {
        target = tgt;
    }

    void OnCollisionEnter2D(Collision2D collision) //모든 적은 플레이어와 충돌 시 플레이어에게 피해. 나중에 STAY로 바꿀 것.
    {
        if (isDead) return;

        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().HPChange(GetContactDMG() * -1);
            //충돌 딜레이도 변수로 선언해야함 
        }
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        CurHP -= amount;
        Debug.Log("피격! 현재 체력: " + CurHP);

        if (CurHP <= 0)
        {
            isDead = true;

            Ani.ResetTrigger("Hurt");
            Ani.Play("Death", 0, 0f);
            StartCoroutine(DieCoroutine());
        }
        else
        {
            Ani.ResetTrigger("Hurt");
            Ani.Play("Hurt", 0, 0f);
        }
    }

    System.Collections.IEnumerator DieCoroutine()
    {
        Debug.Log("몬스터 사망!");

        RD.linearVelocity = Vector2.zero;

        yield return new WaitForSeconds(0.5f);
        Instantiate(expPrefab,transform.position,transform.rotation);
        gameObject.SetActive(false);
    }
    
    void Init()
    {
        MaxHP = statData.baseMaxHP * multiplier;
        ATK = statData.baseATK * multiplier;
        contactDMG = statData.baseContactDMG * multiplier;
        CurHP = MaxHP;
        SetColor();
    }

    void SetColor()
    {
        int randomValue = Random.Range(0, 3);
        switch (randomValue)
        {
            case 0:
                monsterColor = ColorType.Red;
                SR.color = new Color(1f, 0.25f, 0.25f);
                break;
            case 1:
                monsterColor = ColorType.Green;
                SR.color = new Color(0.4f, 1f, 0.4f);
                break;
            case 2:
                monsterColor = ColorType.Blue;
                SR.color = new Color(0.4f, 0.4f, 1f);
                break;
            default:
                Debug.Log("Color init error!");
                break;
        }
    }

}

[System.Serializable]
public class MonsterStat
{
    public float baseMaxHP;
    public float baseATK;
    public float baseContactDMG;
}
