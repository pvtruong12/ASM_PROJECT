using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // Thêm namespace để sử dụng SceneManager

public class CameraManager : MonoBehaviour
{
    private CinemachineConfiner2D confiner;

    private void Start()
    {
        confiner = GetComponent<CinemachineConfiner2D>();
        UpdateConfiner();
        SceneManager.sceneLoaded += OnSceneLoaded;
        EnsureCinemachineBrain();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateConfiner(); EnsureCinemachineBrain();
    }
    private void EnsureCinemachineBrain()
    {
        Camera mainCam = Camera.main;  
        if (mainCam != null && mainCam.GetComponent<CinemachineBrain>() == null)
        {
            mainCam.gameObject.AddComponent<CinemachineBrain>();
        }
    }
    public void UpdateConfiner()
    {
        GameObject bounds = GameObject.FindWithTag("camera");
        if (bounds != null)
        {
            confiner.m_BoundingShape2D = bounds.GetComponent<Collider2D>();
            confiner.InvalidateCache();
        }
    }
}
