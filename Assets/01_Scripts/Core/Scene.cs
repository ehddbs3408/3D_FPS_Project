using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    public void GameQuit()
    {
        Application.Quit();
    }
    public void InGame()
    {
        SceneManager.LoadScene("InGame");
    }
    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

}
