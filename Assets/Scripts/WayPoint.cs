using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WayPoint : MonoBehaviour
{
    private int num = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("myCharz"))
        {
            num++;
            Debug.Log("num ="+ num);
            LoadMapScr.instance.ShowLoading();
            MainChar.instance.currentLevel = MainChar.instance.currentLevel + 1;
            int level = MainChar.instance.currentLevel;
            if (level >= MainChar.listLevel.Length)
                level = 1;
            SceneManager.LoadScene(MainChar.listLevel[level]);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LoadMapScr.instance.StartCoroutine(LoadMapScr.instance.HideAfterDelay(2f));
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

}
