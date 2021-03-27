using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiManager : MonoBehaviour
{
    public TextMeshProUGUI foodText, woodText, stoneText, workersText, housesText, hourText, dayText;
    public Slider prosperityBar;
    public Button pauseButton, x1Button, x2Button, x3Button;

    //public BuildManagerTP buildManager;

    void Update()
    {
        foodText.text = "Food : " + GameplayManager.Instance.food;
        woodText.text = "Wood : " + GameplayManager.Instance.wood;
        stoneText.text = "Stone : " + GameplayManager.Instance.stone;
        //workersText.text =
        //housesText.text =
        hourText.text = "Hour : " + GameplayManager.Instance.hour;
        dayText.text = "Day : " + GameplayManager.Instance.day;


        prosperityBar.value = GameplayManager.Instance.prosperity;
    }

    public void OnClick_Pause()
    {
        pauseButton.interactable = false;
        x1Button.interactable = true;
        x2Button.interactable = true;
        x3Button.interactable = true;

        GameplayManager.Instance.Invoke("Timepaused", 0);
    }
    public void OnClick_X1()
    {
        pauseButton.interactable = true;
        x1Button.interactable = false;
        x2Button.interactable = true;
        x3Button.interactable = true;

        GameplayManager.Instance.Invoke("TimeX1", 0);
    }
    public void OnClick_X2()
    {
        pauseButton.interactable = true;
        x1Button.interactable = true;
        x2Button.interactable = false;
        x3Button.interactable = true;

        GameplayManager.Instance.Invoke("TimeX2", 0);
    }
    public void OnClick_X3()
    {
        pauseButton.interactable = true;
        x1Button.interactable = true;
        x2Button.interactable = true;
        x3Button.interactable = false;

        GameplayManager.Instance.Invoke("TimeX3", 0);
    }
}
