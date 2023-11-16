using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject PanelMainMenu;
    public GameObject PanelSetting;

    public void OpenSetting()
    {
        PanelMainMenu.SetActive(false);
        PanelSetting.SetActive(true);
    }

    public void OpenGameplay()
    {
        SceneManager.LoadScene("MazeStage");
    }

    // Start is called before the first frame update
    void Start()
    {

        //Debug.Log("Hello World");
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Hello World Update");
    }
}
