using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiManager : MonoBehaviour
{
    public TextMeshProUGUI foodText, woodText, stoneText, workersText, housesText, hourText, dayText;
    public Slider prosperityBar;

    void Update()
    {
        foodText.text = "Food : " + GameplayManager.Instance.food;
        woodText.text = "Wood : " + GameplayManager.Instance.wood;
        stoneText.text = "Stone : " + GameplayManager.Instance.stone;
        //workersText.text =
        //housesText.text =
        hourText.text = "Time : " + GameplayManager.Instance.time;
        dayText.text = "Day : " + GameplayManager.Instance.day;


        prosperityBar.value = GameplayManager.Instance.prosperity;
    }

    public void OnClick_Farm()
    {

    }
    public void OnClick_House()
    {

    }
    public void OnClick_Library()
    {

    }
    public void OnClick_Museum()
    {

    }
    public void OnClick_School()
    {

    }
}
