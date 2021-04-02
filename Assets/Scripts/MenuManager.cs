using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject panelcrédit;
    // Start is called before the first frame update
    void Start()//reset timescale to 1
    {
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnClick_Start()//load level
    {
        SceneManager.LoadScene(1);
    }
    public void OnClick_Crédits()//activate credits
    {
        panelcrédit.SetActive(true);
    }
    public void Return_Menu()//desactivate credit
    {
        panelcrédit.SetActive(false);
    }
    public void Doquit()//quit the game

    {
        Debug.Log("has quit game");
        Application.Quit();
    }

    public void OnClick_Menu()//Load menu
    {
        SceneManager.LoadScene(0);
    }
}
