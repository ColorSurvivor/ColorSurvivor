using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class BaseGun : MonoBehaviour
{
    public GameObject ExampleBullet;
    bool CanShot = true;
    public float bulletDamage;
    public float bulletSpeed;
    public float shootSpeed;
    public float MaxPenetrate;
    public ColorType WeaponColor;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //마우스 위치 받기
        Vector2 LookVec = mouseWorldPos-new Vector2(transform.position.x,transform.position.y); //마우스 위치 바라보는 방향 얻기
        transform.rotation = quaternion.EulerXYZ(0,0,Mathf.Atan2(LookVec.y,LookVec.x)); //바라보기
        if(CanShot) //마우스가 눌려있고, 쏠 수 있다면
        {
            CanShot=false; //일단 방아쇠 당겼음
            GameObject temp = Instantiate(ExampleBullet); //총알 생성
            temp.transform.rotation = transform.rotation; //바라보는 방향으로 돌리기
            temp.transform.parent = transform; //위치지정의 편의성을 위해 자식 오브젝트로 편입
            temp.transform.localPosition = new Vector3(0.3f,0,0); //위치지정
            temp.transform.parent = null; //편입했던거 팽하기
            temp.GetComponent<ExampleBullet>().GoVec = LookVec; //날아갈 방향 지정
            temp.GetComponent<ExampleBullet>().Shot(); //쏘기
            StartCoroutine(CoolDown()); //다음 발사 쿨다운
        }
    }
    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(0.5f);
        CanShot = true;
    }
}
