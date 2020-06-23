using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public ProjectileOption[] projectileOptions;
    public PlayerController playerController;
    
    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void SelectProjectile(int option)
    {
        playerController.projectileOption = projectileOptions[option];
    }
    
    public static IEnumerator SlowDown()
    {
        var originalFixedDeltaTime = Time.fixedDeltaTime;
        Time.timeScale = 0.1f;
        Time.fixedDeltaTime = originalFixedDeltaTime * 0.1f;
        yield return new WaitForSecondsRealtime(2);
        Time.timeScale = 1f;
        Time.fixedDeltaTime = originalFixedDeltaTime;
    }
}
