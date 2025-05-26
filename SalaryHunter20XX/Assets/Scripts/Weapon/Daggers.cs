using UnityEngine;

public class Daggers : BaseGun
{
    float rotationStep = 10f;
    override protected void FireBullets(Vector2 lookvec)
    {
        Quaternion spawnRotation = transform.rotation;
        spawnRotation *= Quaternion.Euler(0, 0, rotationStep * -2);

        for (int i = 0; i < 5; i++)
        {
            GameObject temp = Instantiate(ExampleBullet, transform.position, spawnRotation); //총알 생성
            spawnRotation *= Quaternion.Euler(0, 0, rotationStep);

            temp.transform.parent = transform; //위치지정의 편의성을 위해 자식 오브젝트로 편입
            temp.transform.localPosition = new Vector3(0.3f, 0, 0); //위치지정
            temp.transform.parent = null; //편입했던거 팽하기
            temp.GetComponent<BulletBase>().InitBullet((int)(bulletDamage * WeaponHanger.DamageMul),
                                                            bulletSpeed, MaxPenetration, lookvec, weaponColor); //총알 설정 및 발사
        }

    }
}
