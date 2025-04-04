using UnityEngine;

public class Monster : Entity
{
    protected float contactDMG = 1f;
    protected Transform target; //플레이어의 위치를 저장.

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
    public void SetPlayer(Transform tgt)
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

}
