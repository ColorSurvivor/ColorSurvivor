using UnityEngine;

public class Player : Entity
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Start()
    {
        SPD = 3;
    }
    void Update()
    {
        Vector2 MoveVec = new Vector2(0,0);
        if(Input.GetKey(KeyCode.W))
            MoveVec+= new Vector2(0,1*this.SPD);
        if(Input.GetKey(KeyCode.A))
            MoveVec+= new Vector2(-1*this.SPD,0);
        if(Input.GetKey(KeyCode.S))
            MoveVec+= new Vector2(0,-1*this.SPD);
        if(Input.GetKey(KeyCode.D))
            MoveVec+= new Vector2(1*this.SPD,0);//WASD 인풋 받기, 스피드 값을 기본치에 곱해줌
        DoMove(MoveVec);//받은거 이동에 반영
    }
}