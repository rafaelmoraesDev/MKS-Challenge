using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    [SerializeField] private GameObject panelGameOver;
    public void GameStart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
        Time.timeScale = Constants.MINIMUM_VALUE;
    }

    public void MainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }

    public void GameOver()
    {
        Time.timeScale = Constants.ZERO;
        panelGameOver.SetActive(true);
    }
}
