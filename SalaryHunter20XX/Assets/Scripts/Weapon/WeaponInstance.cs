using UnityEngine;

public class WeaponInstance : MonoBehaviour
{
    public WeaponData weaponData;
    public Transform firePoint;

    private float fireTimer;

    void Update()
    {
        fireTimer += Time.deltaTime;

        if (fireTimer >= weaponData.fireCooldown)
        {
            Fire();
            fireTimer = 0f;
        }
    }

    public void Fire()
    {
        GameObject bullet = Instantiate(weaponData.projectilePrefab, firePoint.position, firePoint.rotation);
        Projectile proj = bullet.GetComponent<Projectile>();
        proj.Init(weaponData.baseDamage, weaponData.color, weaponData.projectileSpeed);

        if (weaponData.fireSound != null)
        {
            AudioSource.PlayClipAtPoint(weaponData.fireSound, transform.position);
        }
    }
}
