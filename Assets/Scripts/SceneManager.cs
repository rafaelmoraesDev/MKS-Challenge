using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    [SerializeField] private GameObject panelGameOver;
    public void GameStart()
    {
        Time.timeScale = Constants.MINIMUM_VALUE;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
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
