using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    string One_Particles = "1-Particles";
    string Two_NoPalletes = "2-No Pallettes";
    string Three_Droste = "3-Droste";
    string Four_Pixel = "4-Pixels";


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Screen.SetResolution(2560, 1440, true);
            loadScene(One_Particles);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Screen.SetResolution(2560, 1440, true);
            loadScene(Two_NoPalletes);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Screen.SetResolution(2560, 1440, true);
            loadScene(Three_Droste);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Screen.SetResolution(1440, 1440, true);
            loadScene(Four_Pixel);
        }
    }

    public void loadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
