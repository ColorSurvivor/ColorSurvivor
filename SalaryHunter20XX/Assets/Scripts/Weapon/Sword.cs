using System.Collections;
using UnityEngine;

public class Sword : BaseGun
{
    override protected void FireBullets(Vector2 lookvec)
    {
        GameObject temp = Instantiate(ExampleBullet); //총알 생성

        temp.transform.parent = transform; //위치지정의 편의성을 위해 자식 오브젝트로 편입
        temp.transform.rotation = transform.rotation; //바라보는 방향으로 돌리기
        temp.transform.localPosition = new Vector3(0.3f, 0, 0); //위치지정
        temp.transform.parent = null; //편입했던거 팽하기

        float dmg = GetColorBuffedDamage() * WeaponHanger.DamageMul;
        float spd = GetColorBuffedBulletSpeed();

        temp.GetComponent<BulletBase>().InitBullet((int)dmg, spd, MaxPenetration, lookvec, weaponColor); //총알 설정 및 발사

    }
}
