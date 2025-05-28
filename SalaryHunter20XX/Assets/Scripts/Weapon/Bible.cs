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

    protected override float GetColorBuffedDamage()
    {
        float dmg = bulletDamage;
        switch (weaponColor)
        {
            case ColorType.Red:   dmg *= 1.1f; break; // 빨강: 데미지 약간 상승
            case ColorType.Blue:  dmg *= 0.9f; break; // 파랑: 데미지 감소, 대신 유틸 효과 따로
            case ColorType.Green: dmg *= 0.8f; break; // 초록: 회복은 더 낮추고 데미지도 약간 깎음
        }
        return dmg;
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

            float bibleDamage = GetColorBuffedDamage() * WeaponHanger.DamageMul;

            if (target != null)
            {
                target.TakeDamage((int)bibleDamage, weaponColor);
                GameObject temp = Instantiate(ExampleBullet); //총알 생성
                temp.transform.position = target.transform.position;

                if (weaponColor == ColorType.Blue)
                {
                    target.ApplySlow(1f, 0.75f);
                }
                else if (weaponColor == ColorType.Green)
                {
                    GameManager.instance.player.HPChange(0.1f);
                }
            }
        }
    }
}
