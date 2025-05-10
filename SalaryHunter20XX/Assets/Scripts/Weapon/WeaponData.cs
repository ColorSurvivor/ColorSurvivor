using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/WeaponData")]
public class WeaponData : ScriptableObject
{
    public ColorType color;
    public WeaponType weaponType;
    public WeaponGrade grade;

    public float baseDamage;
    public float projectileSpeed;
    public float fireCooldown;
    public GameObject projectilePrefab;
    public AudioClip fireSound;
}