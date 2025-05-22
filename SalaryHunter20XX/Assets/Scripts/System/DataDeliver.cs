using UnityEngine;

public class DataDeliver : MonoBehaviour
{
    int[] ClassPoints = {0,0,0,0};//직업 포인트 저장소
    public void SetClassPoint(int what, int how)//직업포인트 저장
    {
        ClassPoints[what]=how;
    }
    public int GetClassPoint(int what)//직업포인트 출력
    {
        return ClassPoints[what];
    }
    int Gold = 0; //골드정보
    public void SetGold(int How)//골드정보 저장
    {
        Gold = How;
    }
    public int GetGold()//골드정보 출력
    {
        return Gold;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(gameObject);   //이 스크립트가 들은 오브젝트는, 씬을 넘겨도 살음.
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
