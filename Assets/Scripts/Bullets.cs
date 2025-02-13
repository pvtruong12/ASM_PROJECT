using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 5f);
        
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(collision.CompareTag("Ground"))
    //    {
    //        Destroy(gameObject);
    //    }
    //    if (collision.CompareTag("Mobs"))
    //    {
    //        Destroy(collision.gameObject);
    //    }
    //    if (collision.CompareTag("myCharz"))
    //    {
    //        Debug.Log("is ME DIEd");
    //    }

    //}
}
