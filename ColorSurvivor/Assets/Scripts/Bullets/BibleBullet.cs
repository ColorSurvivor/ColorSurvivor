using System.Collections;
using UnityEngine;

public class BibleBullet : BulletBase
{
    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
