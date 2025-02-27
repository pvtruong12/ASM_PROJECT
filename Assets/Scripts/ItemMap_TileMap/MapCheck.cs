using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCheck : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullets"))
        {
            BulletManagers.instance.ReturnBullet(collision.gameObject);
        }
    }
}
