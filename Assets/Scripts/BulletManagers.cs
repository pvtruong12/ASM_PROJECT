using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManagers : MonoBehaviour
{
    public static BulletManagers instance;
    public long bulletCount = 10;
    public GameObject prefabBullets;
    private Queue<GameObject> gameobjects = new Queue<GameObject>();
    public Vector3 possitions;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
            GameObject bulletParent = new GameObject("BulletPool");
            DontDestroyOnLoad(bulletParent); 
            bulletParent.transform.SetParent(transform); 
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        GameObject bulletParent = GameObject.Find("BulletPool"); 

        for (int i = 0; i < bulletCount; i++)
        {
            GameObject obj = Instantiate(prefabBullets);

            obj.GetComponent<Bullets>().who = "ai";
            obj.SetActive(false);
            if (bulletParent != null) obj.transform.parent = bulletParent.transform; 
            gameobjects.Enqueue(obj);
        }
    }
    public GameObject GetBullets()
    {
        try
        {
            if (gameobjects.Count > 0)
            {
                GameObject bullet = gameobjects.Dequeue();
                if (bullet == null)
                {
                    return Instantiate(prefabBullets);
                }
                bullet.SetActive(true);
                return bullet;
            }
            else
            {
                GameObject newBullet = Instantiate(prefabBullets);
                return newBullet;
            }
        }catch(Exception ex)
        {
            return Instantiate(prefabBullets);
            Debug.Log(ex);
        }
        
    }

    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        bullet.transform.position = Vector3.zero;
        bullet.transform.rotation = Quaternion.identity;
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null) rb.velocity = Vector2.zero; 
        gameobjects.Enqueue(bullet);
    }
}
