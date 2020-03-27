using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuScript : MonoBehaviour
{

    [SerializeField]
    private string MainScene;

    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            SceneManager.LoadScene(MainScene);
        }
    }
}
