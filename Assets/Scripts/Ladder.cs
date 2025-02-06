using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("myCharz"))
        {
            MainChar.instance.isLadder = true;
            MainChar.instance.rb.gravityScale = 0;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("myCharz"))
        {
            MainChar.instance.isLadder = false;
            MainChar.instance.rb.gravityScale = 3;
        }
    }
}
