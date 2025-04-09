using UnityEngine;

public class LevelUPStatButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    GameObject ManagetObject;//UI 프리펩 오브젝트
    public int What = 0, How = 0;
    public void Select()
    {
        ManagetObject = transform.parent.gameObject;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().SetStat(What, How);
        Time.timeScale = 1;
        Destroy(ManagetObject);//UI는 역할을 다했으니 제거
    }
}
