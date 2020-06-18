using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour
{
    public void restart()
    {
        SceneManager.LoadScene("GameLevelScene");
    }

    public void goBackMenu()
    {
        SceneManager.LoadScene("MenuLevelScene");
    }
}
