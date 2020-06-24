using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    
    public ProjectileOption[] projectileOptions;
    public PlayerController playerController;
    public CinemachineMixingCamera mixingCamera;
    public Dropdown projectileOptionsDropdown;

    private const float FixedTimestep = 0.02f;
    private const float SlowdownRate = 0.1f;
    private const float CamChangingRate = 2;
    private const string ProjectileOptionIndexPref = "ProjectileOptionIndex";

    private bool _isSlowingDown;
    private int _cameraIndex;

    private void Start()
    {
        _instance = this;
        ScaleTime(1);
        var projectileOptionIndex = PlayerPrefs.GetInt(ProjectileOptionIndexPref, 0);
        playerController.projectileOption = projectileOptions[projectileOptionIndex];
        projectileOptionsDropdown.SetValueWithoutNotify(projectileOptionIndex);
    }

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void SelectProjectileOption(int index)
    {
        playerController.projectileOption = projectileOptions[index];
        PlayerPrefs.SetInt(ProjectileOptionIndexPref, index);
    }

    public static void SlowDown()
    {
        if (_instance._isSlowingDown)
        {
            return;
        }

        _instance.StartCoroutine(_instance.SlowDownAsync());
    }
    
    private IEnumerator SlowDownAsync()
    {
        _isSlowingDown = true;
        yield return new WaitForSecondsRealtime(0.05f);
        _cameraIndex = 1;
        ScaleTime(SlowdownRate);
        yield return new WaitForSecondsRealtime(5);
        ScaleTime(1f);
        _isSlowingDown = false;
        _cameraIndex = 0;
    }

    private void Update()
    {
        var isCameraIndexFirst = _cameraIndex == 0;
        float targetWeight0 = isCameraIndexFirst ? 1 : 0;
        float targetWeight1 = isCameraIndexFirst ? 0 : 1;
        mixingCamera.m_Weight0 = Mathf.MoveTowards(mixingCamera.m_Weight0, targetWeight0, Time.unscaledDeltaTime * CamChangingRate);
        mixingCamera.m_Weight1 = Mathf.MoveTowards(mixingCamera.m_Weight1, targetWeight1, Time.unscaledDeltaTime * CamChangingRate);
    }

    private static void ScaleTime(float scale)
    {
        Time.timeScale = scale;
        Time.fixedDeltaTime = FixedTimestep * scale;
    }
}
