using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public void OnClickGameAgain()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnClickMainMenu()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
