using UnityEngine;

public class Monster : Entity
{
    protected Transform target; //플레이어의 위치를 저장.

    void Update()
    {
        if(target != null) //플레이어 데이터가 없는 경우에는 이동 X(오류 상황)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            DoMove(direction * 2.5f); // 5는 이동속도로 나중에 변경.
        }
        

    }
    public void SetPlayer(Transform tgt)
    {
        target = tgt;
    }
    void OnCollisionEnter(Collision other) //모든 적은 플레이어와 충돌 시 플레이어에게 피해.
    {
        if(other.gameObject.tag == "Player")
        {
            //플레이어에게 데미지
        }
    }

}
