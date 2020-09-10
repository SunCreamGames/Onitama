using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }
    public static void LoadMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
