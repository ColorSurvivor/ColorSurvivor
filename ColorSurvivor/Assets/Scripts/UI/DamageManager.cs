using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DamageManager : MonoBehaviour
{
    public static DamageManager instance;
    public TextMeshPro damageText;
    void Awake()
    {
        instance = this;
    }
    public void CreateDamageText(Vector2 spawnPos, int damage)
    {
        TextMeshPro newDamage = Instantiate(damageText);
        newDamage.text = damage.ToString();
        newDamage.transform.position = spawnPos;
        Destroy(newDamage, 1f);
    }
}
