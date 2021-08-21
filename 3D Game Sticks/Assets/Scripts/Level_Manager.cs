using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Manager : MonoBehaviour
{
    public void loadScene(int sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
