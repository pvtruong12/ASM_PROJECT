using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public TextMeshProUGUI textScore;
    public TextMeshProUGUI textFPS;
    private float deltaTime = 0.0f;
    private long lastTimeUpdateShowUI;
    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void DrawFPS()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        int fps = (int)(1.0f / deltaTime);
        textFPS.text = $"FPS: {fps}";
    }
    public void Update()
    {
        if (MainChar.currentTimeMillis() - lastTimeUpdateShowUI <= 1000)
            return;
        lastTimeUpdateShowUI = MainChar.currentTimeMillis();
        DrawFPS();
    }
    public void AddCoins(int coin)
    {
        MainChar.instance.coins += coin;
        textScore.text = $"Coin: {MainChar.instance.coins}";
    }
}
