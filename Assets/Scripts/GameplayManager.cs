
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameplayManager : MonoBehaviour
{
    private Camera _camera;
    public static GameplayManager Instance;
    public int food;
    public int wood;
    public int stone;
    public int resident = 0;
    public int freeHouse;
    
    public Transform sleepinstance;
    public List<Building> tobuildlist;

    public Resident testminer, testtimber;


    [SerializeField] private GameObject upgradeui;
    private LineRenderer ui_linerenderer;
    [SerializeField] private Button[] upgradeui_button;
    private Resident selectedresident;
    
    private int target;
    private GameObject deadMan;
    
    
    public List<GameObject> residents_active = new List<GameObject>();
    public List<GameObject> houses_active = new List<GameObject>();
    public List<GameObject> schools_active = new List<GameObject>();
    public List<GameObject> farms_active = new List<GameObject>();
    ///////////////////////////////////////////////////////////////////
    public List<GameObject> residents_unactive = new List<GameObject>();
    public List<GameObject> houses_unactive = new List<GameObject>();
    public List<GameObject> schools_unactive = new List<GameObject>();
    public List<GameObject> farms_unactive = new List<GameObject>();
    //////////////////////////////////////////////////////////////////
    public List<GameObject> mines = new List<GameObject>();
    public List<GameObject> forest = new List<GameObject>();
    public List<GameObject> bush = new List<GameObject>();


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

        initTracker();
        freeHouse = houses_active.Count;
        JobConvert(testminer, testminer.energy, testminer.Happiness, testminer.age,  testminer.gameObject.AddComponent<Miner>(), false);
        JobConvert(testtimber, testtimber.energy, testtimber.Happiness, testtimber.age,  testtimber.gameObject.AddComponent<Timber>(), false);
        if (!(Camera.main is null))
            ui_linerenderer.SetPosition(0,
                Camera.main.ScreenToWorldPoint(upgradeui.transform.position) - Vector3.down * 2f);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < schools_active.Count; i++)
        {
            if (!schools_active[i].activeSelf)
            {
                schools_unactive.Add(schools_active[i]);
                schools_active.Remove(schools_active[i]);
            }
        }

        for (int i = 0; i < schools_unactive.Count; i++)
        {
            if (schools_unactive[i].activeSelf)
            {
                schools_active.Add(schools_unactive[i]);
                schools_unactive.Remove(schools_unactive[i]);
            }
        }
        if (selectedresident != null && selectedresident.actualbehaviour == Resident.behaviour.sleep)
        {
            selectedresident = null;
            upgradeui.SetActive(false);
        }
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
        if (food <= resident)
        { 
            KillRandom();
            GiveFood();

        }
    }
    void KillRandom()
    {
        
        target = Random.Range(0, residents_active.Count);
        deadMan = residents_active [target];
        
    }


    public void JobConvert(Resident who, float energy, bool ishappy, int age, Resident targetedjob, bool isconvertfinished)
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
        if (schools_active.Count != 0)
        {
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
        else
        {
            for (int i = 0; i < 4; i++)
            {
                upgradeui_button[i].interactable = false;
            }
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

    private void initTracker()
    {
        
        residents_active.AddRange((GameObject.FindGameObjectsWithTag("resident")).ToList());
        houses_active.AddRange((GameObject.FindGameObjectsWithTag("house")).ToList());
        schools_active.AddRange((GameObject.FindGameObjectsWithTag("school")).ToList());
        farms_active.AddRange((GameObject.FindGameObjectsWithTag("farm")).ToList());
        
        forest = GameObject.FindGameObjectsWithTag("forest").ToList();
        mines = GameObject.FindGameObjectsWithTag("stones").ToList();
        bush = GameObject.FindGameObjectsWithTag("bush").ToList();
        

        
    }

}