using System.Collections;
using UnityEngine;

public class Skills : MonoBehaviour
{
    PlayerSkill weaponHanger;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public virtual void Awake()
    {
        weaponHanger = transform.parent.GetComponent<PlayerSkill>();
        StartCoroutine(Skill());
    }

    // Update is called once per frame
    public virtual IEnumerator Skill()
    {
        print("RRR");
        yield return new WaitForSeconds(0.5f);
    }
}
