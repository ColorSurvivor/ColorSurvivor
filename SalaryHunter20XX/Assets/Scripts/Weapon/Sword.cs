using System.Collections;
using UnityEngine;

public class Sword : BaseGun
{
    protected override void Update()
    {
        if (curWaitTime * WeaponHanger.ATKSpeedMul >= (1f / shootSpeed)) //무기 발사 가능여부 체크
        {
            CanShot = true;
        }
        else
        {
            curWaitTime += Time.deltaTime;
        }

        if (CanShot && !WeaponHanger.ShotLock) //쏠 수 있다면
        {
            CanShot = false; //일단 방아쇠 당겼음
            curWaitTime = 0f;

            FireBullets();
        }
    }

    protected void FireBullets()
    {
        GameObject temp = Instantiate(ExampleBullet); //총알 생성

        temp.transform.parent = transform; //위치지정의 편의성을 위해 자식 오브젝트로 편입
        temp.transform.localPosition = new Vector3(0.3f, 0, 0); //위치지정
        temp.transform.parent = null; //편입했던거 팽하기
        temp.GetComponent<SwordBullet>().InitBullet((int)(bulletDamage * WeaponHanger.DamageMul),
                                                        bulletSpeed, MaxPenetration, weaponColor); //총알 설정 및 발사

        StartCoroutine(FollowHit());
    }

    IEnumerator FollowHit()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject temp = Instantiate(ExampleBullet); //총알 생성
        temp.transform.rotation = temp.transform.rotation * Quaternion.Euler(0, 0, 180f);

        temp.transform.parent = transform; //위치지정의 편의성을 위해 자식 오브젝트로 편입
        temp.transform.localPosition = new Vector3(0.3f, 0, 0); //위치지정
        temp.transform.parent = null; //편입했던거 팽하기
        temp.GetComponent<SwordBullet>().InitBullet((int)(bulletDamage * WeaponHanger.DamageMul),
                                                        bulletSpeed, MaxPenetration, weaponColor); //총알 설정 및 발사

    }
}
