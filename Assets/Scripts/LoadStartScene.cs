using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadStartScene : MonoBehaviour
{
    public DontDestroyOnLoad dontDestroyOnLoad;
    
    // Start is called before the first frame update
    void Start()
    {
        if (dontDestroyOnLoad.currentLevel > 1)
        {
            SceneManager.LoadScene(2);
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }
}
