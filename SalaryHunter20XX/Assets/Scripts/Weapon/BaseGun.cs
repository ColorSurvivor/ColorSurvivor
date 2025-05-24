using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class BaseGun : MonoBehaviour
{
    public GameObject ExampleBullet;
    public Sprite weaponSprite;
    bool CanShot = true;
    public float bulletDamage;
    public float bulletSpeed;
    public float shootSpeed;
    public int MaxPenetration;
    public ColorType weaponColor;
    public WeaponGrade rarity;
    PlayerSkill WeaponHanger;

    float curWaitTime = 0f;
    void Start()
    {
        WeaponHanger = transform.parent.gameObject.GetComponent<PlayerSkill>();
    }

    void Update()
    {
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //마우스 위치 받기
        Vector2 LookVec = mouseWorldPos - new Vector2(transform.position.x, transform.position.y); //마우스 위치 바라보는 방향 얻기
        transform.rotation = quaternion.EulerXYZ(0, 0, Mathf.Atan2(LookVec.y, LookVec.x)); //바라보기

        if (curWaitTime*WeaponHanger.ATKSpeedMul >= (1f / shootSpeed)) //무기 발사 가능여부 체크
        {
            CanShot = true;
        }
        else
        {
            curWaitTime += Time.deltaTime;
        }

        if (CanShot&&!WeaponHanger.ShotLock) //쏠 수 있다면
        {
            CanShot = false; //일단 방아쇠 당겼음
            curWaitTime = 0f;

            GameObject temp = Instantiate(ExampleBullet); //총알 생성
            temp.transform.rotation = transform.rotation; //바라보는 방향으로 돌리기
            temp.transform.parent = transform; //위치지정의 편의성을 위해 자식 오브젝트로 편입
            temp.transform.localPosition = new Vector3(0.3f, 0, 0); //위치지정
            temp.transform.parent = null; //편입했던거 팽하기
            temp.GetComponent<ExampleBullet>().InitBullet((int)(bulletDamage*WeaponHanger.DamageMul), bulletSpeed, MaxPenetration, LookVec); //총알 설정.
            temp.GetComponent<ExampleBullet>().Shot(); //쏘기
            temp.GetComponent<ExampleBullet>().bulletColor = weaponColor; //총알 색상 지정
        }
    }

    public void Init(WeaponData weapondata)
    {
        weaponSprite = weapondata.weaponSprite;
        bulletDamage = weapondata.bulletDamage[(int)weapondata.rarity];
        bulletSpeed = weapondata.bulletSpeed[(int)weapondata.rarity];
        shootSpeed = weapondata.ShootingSpeed[(int)weapondata.rarity];
        MaxPenetration = weapondata.MaxPenetration[(int)weapondata.rarity];
        weaponColor = weapondata.weaponcolor;
        rarity = weapondata.rarity;
    }
}
