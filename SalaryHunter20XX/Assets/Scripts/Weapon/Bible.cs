using System.Collections.Generic;
using UnityEngine;

public class Bible : BaseGun
{
    protected override void Update()
    {
        if (curWaitTime*WeaponHanger.ATKSpeedMul >= (1f / shootSpeed)) //무기 발사 가능여부 체크
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
        Collider2D[] collidersInRange = Physics2D.OverlapCircleAll(transform.position, 7f);
        List<Collider2D> enemysInRange = new List<Collider2D>();

        foreach (Collider2D OBJ in collidersInRange)
        {
            if (OBJ.CompareTag("Enemy"))
            {
                enemysInRange.Add(OBJ);
                Debug.Log(OBJ.name);
            }
        }
        if (enemysInRange.Count != 0)
        {
            int randomIndex = Random.Range(0, enemysInRange.Count);
            Monster target = enemysInRange[randomIndex].gameObject.GetComponent<Monster>();

            GameObject temp = Instantiate(ExampleBullet); //총알 생성
            temp.transform.position = target.transform.position;

            target.TakeDamage((int)bulletDamage, weaponColor);
        }
    }
}
