using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameplayManger : MonoBehaviour
{
    private Camera _camera;
    public static GameplayManger Instance;
    public int food;
    public int wood;
    public int stone;
    public int resident = 0;
    public int freeHouse;

    private int housenumber;
    public Transform sleepinstance;
    public List<Building> tobuildlist;

    public Resident testminer, testtimber;


    [SerializeField] private GameObject upgradeui;
    private LineRenderer ui_linerenderer;
    [SerializeField] private Button[] upgradeui_button;
    private Resident selectedresident;
    
    private int target;
    private GameObject deadMan;



    public int time;
    public int prosperity;

    private RaycastHit Cursorray;
    [SerializeField] private LayerMask cursorray_mask;

    // Start is called before the first frame update
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

        if (upgradeui != null && upgradeui.GetComponent<LineRenderer>() != null) ui_linerenderer = upgradeui.GetComponent<LineRenderer>();

        housenumber = GameObject.FindGameObjectsWithTag("house").Length;
        freeHouse = housenumber;
        JobConvert(testminer, testminer.energy, testminer.Happiness, testminer.age,  testminer.gameObject.AddComponent<Miner>(), false);
        JobConvert(testtimber, testtimber.energy, testtimber.Happiness, testtimber.age,  testtimber.gameObject.AddComponent<Timber>(), false);
        ui_linerenderer.SetPosition(0, Camera.main.ScreenToWorldPoint(upgradeui.transform.position) - Vector3.down*2f);

    }

    // Update is called once per frame
    void Update()
    {
        
        if (time == 19) GiveFood();
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out Cursorray, 200, cursorray_mask))
        {
            //Debug.Log("RAYCAST at :"+Cursorray.point);
            //Debug.DrawLine(Camera.main.transform.position, Cursorray.point, Color.red, 20f);
            if (Input.GetButtonDown("Fire1") && Cursorray.collider.GetComponent<Resident>() != null)
            {
                SpawnUpgradeUI(Cursorray.collider.GetComponent<Resident>());
            }
        }

        if (selectedresident != null)
        {
            ui_linerenderer.SetPosition(1, new Vector3(selectedresident.transform.position.x, Camera.main.ScreenToWorldPoint(upgradeui.transform.position).y-2f, selectedresident.transform.position.z));
        }


    }

    void GiveFood()
    {

        if (food >= resident)
        {
            //Eat = true;
        }
        /*else
        {
            Resident[] NbResident = FindObjectsOfType<Resident>();
            int diff = resident - food;
            for (int i = 1; i <= NbResident.Length || diff != 0; i++)
            {
                Random.Range(1, 3)
                {
                    Destroy(gameObject);
                    diff--; 
                }
            }
        }*/
        else
        {
            KillRandom();
        }

        if (food <= resident)
        { 
            KillRandom();
            GiveFood();

        }
    }
    void KillRandom()
    {
        Resident[] NbResident = FindObjectsOfType<Resident>();
        target = Random.Range(0, NbResident.Length);
        deadMan = NbResident [target].gameObject;
        resident--;
    }


    public void JobConvert(Resident who, int energy, bool ishappy, int age, Resident targetedjob, bool isconvertfinished)
    {
        GameObject whoobject = who.gameObject;
        if (!(who is Student) && !whoobject.GetComponent<Student>())
        {
            Destroy(who);
            Student whostudent = whoobject.AddComponent<Student>();
            whostudent.classtarget = targetedjob;
            whostudent.classtarget.enabled = false;
            whostudent.age = age;
            whostudent.energy = energy;
            whostudent.Happiness = ishappy;
            Debug.Log("TYPE 1 CONVERT");


        }
        else
        {
            Student whostudent = who.gameObject.GetComponent<Student>();
            if (whostudent.classtarget != null && isconvertfinished)
            {
                whostudent.classtarget.age = age;
                whostudent.classtarget.energy = energy;
                whostudent.classtarget.Happiness = ishappy;
                whostudent.classtarget.enabled = true;
                Destroy(who);
                Debug.Log("TYPE 2 CONVERT");
            }
            else
            {
                Debug.Log("TYPE 3 CONVERT");
                whostudent.taskpercent = 0;
                Destroy(whostudent.classtarget);
                whostudent.classtarget = targetedjob;
                whostudent.classtarget.enabled = false;


            }


            

        }
    }

    public void SpawnUpgradeUI(Resident who)
    {
        for (int i = 0; i < 4; i++)
        {
            upgradeui_button[i].interactable = true;
        }

        upgradeui.SetActive(true);
        selectedresident = who;
        switch (who)
        {
            case Builder builder:
                upgradeui_button[0].interactable = false;
                break;
            case FoodSearcher foodSearcher:
                upgradeui_button[1].interactable = false;
                break;
            case Miner miner:
                upgradeui_button[2].interactable = false;
                break;
            case Timber timber:
                upgradeui_button[3].interactable = false;
                break;
            case Student student:
                switch (student.classtarget)
                {
                    case Builder builder:
                        upgradeui_button[0].interactable = false;
                        break;
                    case FoodSearcher foodSearcher:
                        upgradeui_button[1].interactable = false;
                        break;
                    case Miner miner:
                        upgradeui_button[2].interactable = false;
                        break;
                    case Timber timber:
                        upgradeui_button[3].interactable = false;
                        break;
                }
                break;

        }
        
    }

    public void ConvertButton(Button presssbutton)
    {
        if (presssbutton == upgradeui_button[0])
        {
            JobConvert(selectedresident, selectedresident.energy, selectedresident.Happiness, selectedresident.age, selectedresident.gameObject.AddComponent<Builder>(), false);
        }else if (presssbutton == upgradeui_button[1])
        {
            JobConvert(selectedresident, selectedresident.energy, selectedresident.Happiness, selectedresident.age, selectedresident.gameObject.AddComponent<FoodSearcher>(), false);
        }else if (presssbutton == upgradeui_button[2])
        {
            JobConvert(selectedresident, selectedresident.energy, selectedresident.Happiness, selectedresident.age, selectedresident.gameObject.AddComponent<Miner>(), false);
        }else if (presssbutton == upgradeui_button[3])
        {
            JobConvert(selectedresident, selectedresident.energy, selectedresident.Happiness, selectedresident.age, selectedresident.gameObject.AddComponent<Timber>(), false);
        }
        upgradeui.SetActive(false);
        
    }
    
}
