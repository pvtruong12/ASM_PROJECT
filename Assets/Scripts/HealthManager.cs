using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public static int health = 3;
    public Image[] arrayHeart;
    public Sprite full_Heart;
    public Sprite empty_Heart;
    void Update()
    {
        foreach (Image img in arrayHeart)
        {
            img.sprite = empty_Heart;
        }

        for (int i = 0; i < health; i++)
        {
            arrayHeart[i].sprite = full_Heart;
        }
    }
}
