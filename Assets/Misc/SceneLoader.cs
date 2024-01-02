using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    string One_Particles = "1-Particles";
    string Two_NoPalletes = "2-No Pallettes";


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            loadScene(One_Particles);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            loadScene(Two_NoPalletes);
        }
    }

    public void loadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
