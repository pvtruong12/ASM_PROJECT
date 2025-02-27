using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMapManager : MonoBehaviour
{
    public int lenghtCoin = 10;
    public GameObject prefabsCoin;
    public GameObject prefabsHearth;
    public static ItemMapManager instance;
    public Queue<GameObject> listCoin = new Queue<GameObject>();
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            GameObject itemMaps = new GameObject("CoinPool");
            DontDestroyOnLoad(itemMaps);
            itemMaps.transform.SetParent(transform);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Start()
    {
        GameObject bulletParent = GameObject.Find("CoinPool");
        for (int i = 0; i < lenghtCoin; i++)
        {
            GameObject obj = Instantiate(prefabsCoin);
            obj.SetActive(false);
            if (bulletParent != null) obj.transform.parent = bulletParent.transform;
            listCoin.Enqueue(obj);
        }
    }
    public GameObject GetCoin(Vector3 possitons)
    {
        try
        {
            if (listCoin.Count > 0)
            {
                GameObject game = listCoin.Dequeue();
                game.SetActive(true);
                game.transform.position = possitons;
                return game;
            }
            else
            {
                GameObject gameobjec = Instantiate(prefabsCoin, possitons, Quaternion.identity);
                return gameObject;
            }
        }catch(Exception ex)
        {
            Debug.Log(ex);
            return Instantiate(prefabsCoin, possitons, Quaternion.identity);
        }
    }
    public void ReturnCoin(GameObject game)
    {
        if (!listCoin.Contains(game))
            Destroy(game);
        game.SetActive(false);
        listCoin.Enqueue(game);
    }

}
