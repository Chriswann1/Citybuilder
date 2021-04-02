using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance;

    public TextMeshProUGUI foodText, woodText, stoneText, workersText, housesText, hourText, dayText;
    public Slider prosperityBar;
    public Button pauseButton, x1Button, x2Button, x3Button;
    public GameObject quitMenu;
    public bool inQuitMenu = false;

    //public BuildManagerTP buildManager;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()//setting the text in UITEXT + check for the ingame menu with echap
    {
        foodText.text = "Food : " + GameplayManager.Instance.food;
        woodText.text = "Wood : " + GameplayManager.Instance.wood;
        stoneText.text = "Stone : " + GameplayManager.Instance.stone;
        workersText.text = "Residents : " + PoolManager.Instance.residents;
        housesText.text = "Free houses : " + GameplayManager.Instance.freeHouse;
        hourText.text = "Hour : " + GameplayManager.Instance.hour;
        dayText.text = "Day : " + GameplayManager.Instance.day;


        prosperityBar.value = GameplayManager.Instance.prosperity;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (inQuitMenu)
            {
                OnClick_Resume();
            }
            else
            {
                GameplayManager.Instance.Invoke("Timepaused", 0);
                quitMenu.SetActive(true);
                inQuitMenu = true;
            }
        }
    }

    public void OnClick_Resume()//disabling the ingame menu and resetting the time
    {
        quitMenu.SetActive(false);
        GameplayManager.Instance.Invoke("TimeX1", 0);
        inQuitMenu = false;
    }

    public void OnClick_Pause()//disbaling pause button and enabling the others buttons
    {
        pauseButton.interactable = false;
        x1Button.interactable = true;
        x2Button.interactable = true;
        x3Button.interactable = true;

        GameplayManager.Instance.Invoke("Timepaused", 0);
    }
    public void OnClick_X1()//disabling x1 button and enabling the others buttons
    {
        pauseButton.interactable = true;
        x1Button.interactable = false;
        x2Button.interactable = true;
        x3Button.interactable = true;

        GameplayManager.Instance.Invoke("TimeX1", 0);
    }
    public void OnClick_X2()//disabling x2 button and enabling the others buttons
    {
        pauseButton.interactable = true;
        x1Button.interactable = true;
        x2Button.interactable = false;
        x3Button.interactable = true;

        GameplayManager.Instance.Invoke("TimeX2", 0);
    }
    public void OnClick_X3()//disabling x3 button and enabling the others buttons
    {
        pauseButton.interactable = true;
        x1Button.interactable = true;
        x2Button.interactable = true;
        x3Button.interactable = false;

        GameplayManager.Instance.Invoke("TimeX3", 0);
    }
}
