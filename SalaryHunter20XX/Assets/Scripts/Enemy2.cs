using Unity.VisualScripting;
using UnityEngine;

public class Enemy2 : Monster
{
    public enum EnemyState
    {
        Move,
        Attack
    }
    public GameObject bullet;
    float AtkTime = 1.5f;
    float CurrentAtkTimer = 1.5f;
    EnemyState CurrentState = EnemyState.Move;

    void Update()
    {
        if(target == null) //플레이어 데이터가 없는 경우에는 이동 X(오류 상황)
        {
            return ;
        }
        
        CheckDist();

        switch(CurrentState) //상태에 따라 다른 액션을 진행행
        {
            case EnemyState.Move:
                Vector2 direction = (target.position - transform.position).normalized;
                DoMove(direction * 2.5f); // 5는 이동속도로 나중에 변경.
                break;
            case EnemyState.Attack:
                DoMove(Vector3.zero);
                if(CurrentAtkTimer >= AtkTime)
                {
                    GameObject spawnBullet = Instantiate(bullet, transform.position, Quaternion.identity);
                    spawnBullet.GetComponent<Rigidbody2D>().linearVelocity = (target.transform.position - transform.position).normalized * 5;
                    CurrentAtkTimer = 0f;
                }
                else
                {
                    CurrentAtkTimer += Time.deltaTime;
                }
                CurrentState = EnemyState.Move;
                break;
        }

    }

    void CheckDist() //플레이어와의 거리를 측정해서 CurrentState를 변경.
    {
        if(CurrentState == EnemyState.Move && Vector3.Distance(transform.position, target.transform.position) <= 5)
        {
            CurrentState = EnemyState.Attack;
        }
    }
}
