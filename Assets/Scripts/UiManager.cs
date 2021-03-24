using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiManager : MonoBehaviour
{
    public TextMeshProUGUI foodText, woodText, stoneText, workersText, housesText;
    public Slider prosperityBar;

    void Update()
    {
        foodText.text = "Food : " + GameplayManager.Instance.food;
        woodText.text = "Wood : " + GameplayManager.Instance.wood;
        stoneText.text = "Stone : " + GameplayManager.Instance.stone;

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
