using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    
    public ProjectileOption[] projectileOptions;
    public PlayerController playerController;
    public CinemachineMixingCamera mixingCamera;
    public Dropdown projectileOptionsDropdown;

    private const float fixedTimestep = 0.02f;
    private const float slowdownRate = 0.1f;
    private const float camChangingRate = 1;
    private const string projectileOptionPref = "ProjectileOptionPref";

    private bool isSlowingDown;
    private int vrCameraIndex = 0;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = fixedTimestep;
        var projectileOption = PlayerPrefs.GetInt(projectileOptionPref, 0);
        playerController.projectileOption = projectileOptions[projectileOption];
        projectileOptionsDropdown.SetValueWithoutNotify(projectileOption);
    }

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void SelectProjectile(int option)
    {
        playerController.projectileOption = projectileOptions[option];
        PlayerPrefs.SetInt(projectileOptionPref, option);
    }

    public static void SlowDown()
    {
        if (instance.isSlowingDown)
        {
            return;
        }

        instance.StartCoroutine(instance.SlowDownAsync());
    }
    
    private IEnumerator SlowDownAsync()
    {
       
        isSlowingDown = true;
        yield return new WaitForSecondsRealtime(0.05f);
        vrCameraIndex = 1;
        Time.timeScale = slowdownRate;
        Time.fixedDeltaTime = fixedTimestep * slowdownRate;
        yield return new WaitForSecondsRealtime(5);
        Time.timeScale = 1f;
        Time.fixedDeltaTime = fixedTimestep;
        isSlowingDown = false;
        vrCameraIndex = 0;
    }

    private void Update()
    {
        float targetWeight0 = vrCameraIndex == 0 ? 1 : 0;
        float targetWeight1 = vrCameraIndex == 1 ? 1 : 0;
        
        mixingCamera.m_Weight0 = Mathf.MoveTowards(mixingCamera.m_Weight0, targetWeight0, Time.unscaledDeltaTime * camChangingRate);
        mixingCamera.m_Weight1 = Mathf.MoveTowards(mixingCamera.m_Weight1, targetWeight1, Time.unscaledDeltaTime * camChangingRate);
    }
}
