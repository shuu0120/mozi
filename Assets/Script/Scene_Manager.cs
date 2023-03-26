using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour
{
    public void GameScene()
    {
        SceneManager.LoadScene("Play_White");
    }

    public void HomeScene()
    {
        SceneManager.LoadScene("Home");
    }
    public void Restart()
    {
        //現シーンをリスタート
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}