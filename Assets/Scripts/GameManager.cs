using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static string name = "";
    public GameObject panelWinOrLose;
    public WayPoint waypoins;
    public TextMeshProUGUI textScore;
    public TextMeshProUGUI textTimeCountUp;
    public TextMeshProUGUI textLogoWinOrLose;
    public TextMeshProUGUI textFPS;
    private float deltaTime = 0.0f;
    private long lastTimeUpdateShowUI;
    public int currentLevel = 0;
    public int coins = 0;
    public float moveSpeed = 2f;
    public float bulletSpeed = 10f;
    public List<Mob> listMobInMap = new List<Mob>();
    public int health = 3;
    public Image[] arrayHeart;
    public List<GameObject> listButtonWinLose;
    public Sprite full_Heart;
    public Sprite empty_Heart;
    private List<int> scores = new List<int>();
    public Vector3[] repoint;
    private float timeElapsed = 0f;
    private long lastTimeUpdateTime;
    public static string[] listLevel = new string[] { "Level_1", "Level_2", "Level_3" };
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public static string PathFile(string file)
    {
        return $"{Application.persistentDataPath}/{file}";
    }

    public void DemGiay()
    {
        if(health <= 0)
        {
            return;
        }
        if (Time.time - lastTimeUpdateTime >= 1f)
        {
            lastTimeUpdateTime = (long)Time.time;
            timeElapsed++;
            textTimeCountUp.text = $"Tiến độ Win: {timeElapsed}";
        }

    }
    public void ResetChar()
    {
        MainChar.instance.transform.position = Vector3.zero;
        currentLevel = 0;
        textScore.text = (coins = 0).ToString();
        health = 3;
    }
    public void Start()
    {
        Intit();
    }
    public void Instance(WayPoint wps)
    {
        waypoins = wps;
    }
    public Vector3 reponPossition()
    {
        return repoint[currentLevel];
    }
    public void HealthUpdate()
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
    public void MeDead()
    {
        MainChar.instance.currentCoin = 0;
        textScore.text = $"Coin: {coins}";
        Action Actions = new Action(delegate () {
            
        });
        waypoins.LoadMapLevel(currentLevel, Actions);
    }
    public void SetPanelWinOrLose(bool isWin)
    {

        panelWinOrLose.SetActive(true);
        textLogoWinOrLose.fontSize = 36;
        if (!isWin)
        {
            listButtonWinLose.ElementAt(0).SetActive(false);
            textLogoWinOrLose.text = "You Lose";
            
        }
        else
        {
            listButtonWinLose.ElementAt(0).SetActive(true);
            textLogoWinOrLose.text = "You Win";
        }
        Time.timeScale = 0;
        SaveCore();
    }
    public void Intit()
    {
        listMobInMap.Clear();
        listMobInMap = GameObject.FindGameObjectsWithTag("Mobs").Select(x => { return x.GetComponent<Mob>(); }).ToList();
        Debug.Log("So mob in map: " + listMobInMap.Count);
    }
    
    public void Update()
    {
        MainChar.instance.update();
        for(int i =0; i< listMobInMap.Count; i++)
        {
            if(listMobInMap[i] != null)
                listMobInMap[i].update();
        }
        DemGiay();
        HealthUpdate();
        DrawFPS();
    }
    private void DrawFPS()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        int fps = (int)(1.0f / deltaTime);
        textFPS.text = $"FPS: {fps}";
    }
    public void SaveCore()
    {
        if(coins > 0)
        {
            File.AppendAllText(PathFile("coins.txt"),name+"|"+ coins+"|"+timeElapsed+"\n");
            timeElapsed = 0;
            coins = 0;
        }
    }
    public void Top3()
    {
        List<string> array = File.ReadAllLines(PathFile("coins.txt")).ToList();
        array = array.OrderBy(x =>int.Parse(x.Split('|')[2])).ThenByDescending(x => int.Parse(x.Split('|')[1])).Take(3).Select(x => {
            string[] arr = x.Split('|');
            return $"{arr[0]}: Scrore:  {arr[1]} với số giây: {arr[2]}";
        }).ToList();
        textLogoWinOrLose.fontSize = 16;
        textLogoWinOrLose.text =  "🏆 **Bảng Xếp Hạng** 🏆\n"+ string.Join("\n", array);
        
    }
    public void AddCoins(int coin)
    {
        MainChar.instance.currentCoin += coin;
        textScore.text = $"Coin: {(coins + MainChar.instance.currentCoin)}";
    }public void AddHearth(int num)
    {
        if (health >= 3)
            return;
        health += num;
    }
    public void FixedUpdate()
    {
        MainChar.instance.Row();
    }

}
