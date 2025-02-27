using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WayPoint : MonoBehaviour
{
    public void Start()
    {
        GameManager.instance.Instance(this);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("myCharz"))
        {
            GameManager.instance.coins += MainChar.instance.currentCoin;
            MainChar.instance.currentCoin = 0;
            GameManager.instance.currentLevel = GameManager.instance.currentLevel + 1;
            int level = GameManager.instance.currentLevel;
            if (level >= GameManager.listLevel.Length)
            {
                GameManager.instance.SetPanelWinOrLose(true);
                return;
            }
            LoadMapScr.instance.ShowLoading();
            SceneManager.LoadScene(GameManager.listLevel[level]);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }
    public void LoadMapLevel(int curentLevelm, Action Actions)
    {
        LoadMapScr.instance.ShowLoading();
        Actions();
        SceneManager.LoadScene(GameManager.listLevel[curentLevelm]);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        LoadMapScr.instance.StartCoroutine(LoadMapScr.instance.HideAfterDelay());
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

}
