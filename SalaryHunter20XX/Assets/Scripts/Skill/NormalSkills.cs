using UnityEngine;
using System.Collections;

public class NormalSkills : Skills
{
    int r = 0, g = 0, b = 0; // 스킬 색상 카운트 변수
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public override void Awake()
    {
        weaponHanger = transform.parent.GetComponent<PlayerSkill>();
        StartCoroutine(Skill());
    }
    public void SkillOn(int r = 0, int g = 0, int b = 0)
    {
        this.r = r;
        this.g = g;
        this.b = b;
        GetComponent<SpriteRenderer>().color = new Color(r * 0.35f, g * 0.35f, b * 0.35f, 0.4f);
        StartCoroutine(Skill());
    }
    // Update is called once per frame
    public override IEnumerator Skill()
    {
        SkillEnd(10, 3); // 스킬 지속시간 및 쿨다운 자동시전
        weaponHanger.DamageMul = 1 + (r*0.4f);
        weaponHanger.ATKSpeedMul = 1 + (b * 0.4f);
        Player temp = transform.parent.parent.GetComponent<Player>();
        temp.HPChange(temp.GetMaxHP() * 0.05f*g); // 플레이어 회복
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
