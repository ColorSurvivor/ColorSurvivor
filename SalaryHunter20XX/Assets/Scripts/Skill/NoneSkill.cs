using UnityEngine;
using System.Collections;

public class NoneSkill : Skills
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override IEnumerator Skill()
    {
        SkillEnd(10, 3); // 스킬 지속시간 및 쿨다운 자동시전
        weaponHanger.DamageMul = 1.1f;
        weaponHanger.ATKSpeedMul = 1.1f;
        Player temp = transform.parent.parent.GetComponent<Player>();
        temp.HPChange(temp.GetMaxHP() * 0.01f); // 플레이어 회복
        yield return new WaitForSeconds(10f);
        GetComponent<Animator>().SetTrigger("Change");
        weaponHanger.DamageMul = 1; // 원래 데미지로 복구
        weaponHanger.ATKSpeedMul = 1;
        weaponHanger.ShotLock = true; // 스킬 사용 후 공격 일정시간 봉인
        yield return new WaitForSeconds(3f);
        weaponHanger.ShotLock = false; // 봉인 해제
        Destroy(gameObject); // 스킬 오브젝트 제거
    }
}
