using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    void Update()
    {
        // Backspace - Reload
        //TODO make this work with controller
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            LevelLoader.ReloadLevel();
        }
        // Escape - Quit
        if (Input.GetButtonDown("Cancel"))
        {
            Application.Quit();
        }
    }
}
