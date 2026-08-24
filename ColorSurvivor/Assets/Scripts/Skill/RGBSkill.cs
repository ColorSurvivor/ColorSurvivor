using UnityEngine;
using System.Collections;
public class RGBSkill : Skills
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    public override IEnumerator Skill()
    {
        StartCoroutine(GamingRGB()); // RGB 효과 시작
        SkillEnd(10, 3); // 스킬 지속시간 및 쿨다운 자동시전
        weaponHanger.DamageMul = 2;
        weaponHanger.ATKSpeedMul = 2;
        Player temp = transform.parent.parent.GetComponent<Player>();
        temp.HPChange(temp.GetMaxHP() * 0.1f); // 플레이어 회복
        yield return new WaitForSeconds(10f);
        GetComponent<Animator>().SetTrigger("Change");
        weaponHanger.DamageMul = 1; // 원래 데미지로 복구
        weaponHanger.ATKSpeedMul = 1;
        weaponHanger.ShotLock = true; // 스킬 사용 후 공격 일정시간 봉인
        yield return new WaitForSeconds(3f);
        weaponHanger.ShotLock = false; // 봉인 해제
        Destroy(gameObject); // 스킬 오브젝트 제거
    }
    IEnumerator GamingRGB()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        Color[] colors = new Color[] { new Color(1,0,0,0.4f), new Color(0,1,0,0.4f), new Color(0,0,1,0.4f) }; // 빨강, 초록, 파랑 색상 배열
        float duration = 0.5f; // 색상 변경 간격
        float TimeGo = 0f;
        int colorIndex = 0;

        while (TimeGo < 10f)
        {
            sr.color = colors[colorIndex % colors.Length];
            colorIndex++;
            yield return new WaitForSeconds(duration);
            TimeGo += duration;
        }
        sr.color = Color.white; // 원래 색상으로 복구

    }
}
