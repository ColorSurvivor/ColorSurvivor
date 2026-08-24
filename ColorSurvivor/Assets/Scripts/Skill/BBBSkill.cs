using System.Collections;
using UnityEngine;

public class BBBSkill : Skills
{
    void Update()
    {

    }
    public override IEnumerator Skill()
    {
        SkillEnd(10,3); // 스킬 지속시간 및 쿨다운 자동시전
        weaponHanger.ATKSpeedMul = 2;
        yield return new WaitForSeconds(10f);
        GetComponent<Animator>().SetTrigger("Change");
        weaponHanger.ATKSpeedMul = 1;
        weaponHanger.ShotLock = true; // 스킬 사용 후 공격 일정시간 봉인
        yield return new WaitForSeconds(3f);
        weaponHanger.ShotLock = false; // 봉인 해제
        Destroy(gameObject); // 스킬 오브젝트 제거
    }
}
