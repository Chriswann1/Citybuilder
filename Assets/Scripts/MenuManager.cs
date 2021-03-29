using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject panelcrédit;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnClick_Start()
    {
        SceneManager.LoadScene(1);
    }
    public void OnClick_Crédits()
    {
        panelcrédit.SetActive(true);
    }
    public void Return_Menu()
    {
        panelcrédit.SetActive(false);
    }
    public void Doquit()

    {
        Debug.Log("has quit game");
        Application.Quit();
    }

    public void OnClick_Menu()
    {
        SceneManager.LoadScene(0);
    }
}
