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
    public int freeHouse;
    
    public Transform sleepinstance;
    
    public Queue<Building> tobuildqueue = new Queue<Building>();
    public Building currentbuildingtobuild;

    
    public Resident testminer, testtimber;



    [SerializeField] private GameObject upgradeui;
    private LineRenderer ui_linerenderer;
    [SerializeField] private Button[] upgradeui_button;
    private Resident selectedresident;
    
    private int target;
    private GameObject deadMan;
    [SerializeField] private bool paused;
    public int day;
    public int hour;
    private bool spawnPassed;



    public List<GameObject> houses = new List<GameObject>();
    public List<GameObject> schools = new List<GameObject>();
    public List<GameObject> farms = new List<GameObject>();
    public List<GameObject> libraries = new List<GameObject>();
    public List<GameObject> museums = new List<GameObject>();
    
    public List<GameObject> mines = new List<GameObject>();
    public List<GameObject> forest = new List<GameObject>();
    public List<GameObject> bush = new List<GameObject>();

    private bool Eat;
    public float time;
    public int prosperity;

    private RaycastHit Cursorray;
    [SerializeField] private LayerMask cursorray_mask;

    public GameObject victoryScreen, defeatScreen;
   
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
        freeHouse = houses.Count;
        JobConvert(testminer, testminer.energy, testminer.Happiness, testminer.age,  testminer.gameObject.AddComponent<Miner>(), false);
        JobConvert(testtimber, testtimber.energy, testtimber.Happiness, testtimber.age,  testtimber.gameObject.AddComponent<Timber>(), false);
        if (!(Camera.main is null)) ui_linerenderer.SetPosition(0, Camera.main.ScreenToWorldPoint(upgradeui.transform.position) - Vector3.down * 2f);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (prosperity == 100)
        {
            Timepaused();
            victoryScreen.SetActive(true);
        }
        if (resident == 0)
        {
            Timepaused();
            defeatScreen.SetActive(true);
        }

        time = time + Time.deltaTime;
        if (time >= 10)
        {
            time = 0;
            hour++;
        }
        if (hour >= 24)
        {
            hour = 0;
            day++;
        }
        if (hour == 8 &&  spawnPassed == false)
        {
            PoolManager.Instance.spawn_resident();
            spawnPassed = true;
        }

        if (hour == 9)
        {
            spawnPassed = false;
            Eat = true;
        }
        /*
        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            for (int i = 0; i < PoolManager.Instance.residents_active.Count; i++)
            {
                JobConvert(PoolManager.Instance.residents_active[i].GetComponent<Resident>(), 100, false, 15, PoolManager.Instance.residents_active[i].AddComponent<Miner>(), false);
            }
        }
        */
        /*
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            Time.timeScale = 0;
        }else if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            Time.timeScale = 1;
        }else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            Time.timeScale = 2;
        }else if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            Time.timeScale = 3;
        }*/

        if (selectedresident != null && selectedresident.actualbehaviour == Resident.behaviour.sleep)
        {
            selectedresident = null;
            upgradeui.SetActive(false);
        }
        if (hour == 19 && Eat == false)
        {
            GiveFood();
            Eat = true;
        }
        
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


        /*
        if (Input.GetKeyDown(KeyCode.A))
        {
            GameObject housetosupp = houses[0].gameObject;
            houses.Remove(houses[0]);
            Destroy(housetosupp);
        }
        */
        
        if(currentbuildingtobuild == null && tobuildqueue.Count > 0) currentbuildingtobuild = tobuildqueue.Dequeue();





    }
    
    void GiveFood()
    {
        for (int i = 0; i < PoolManager.Instance.residents_active.Count; i++)
        {
            if (food < PoolManager.Instance.residents_active.Count)
            {
                Debug.LogError("kill");
                KillRandom();
            }

            
            
                food--;
           
        }
    }
    public void KillRandom()
    {
        if (PoolManager.Instance.residents_active.Count > 0)
        {
            Debug.LogError("KillRandom");
            target = Random.Range(0, PoolManager.Instance.residents_active.Count);
            PoolManager.Instance.kill_resident(PoolManager.Instance.residents_active[target]);
        }

        
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
        if (schools.Count != 0)
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
        
        
        houses.AddRange((GameObject.FindGameObjectsWithTag("house")).ToList());
        schools.AddRange((GameObject.FindGameObjectsWithTag("school")).ToList());
        farms.AddRange((GameObject.FindGameObjectsWithTag("farm")).ToList());
        libraries.AddRange((GameObject.FindGameObjectsWithTag("library")).ToList());
        museums.AddRange((GameObject.FindGameObjectsWithTag("museum")).ToList());
        
        
        
        forest = GameObject.FindGameObjectsWithTag("forest").ToList();
        mines = GameObject.FindGameObjectsWithTag("stones").ToList();
        bush = GameObject.FindGameObjectsWithTag("bush").ToList();
        
        
    }
    
    public void ActivateBuilding(Building building)
    {
        currentbuildingtobuild = null;
        switch (building.name)
        {
            default:
                Debug.LogError("ERROR BUILDING");
                break;
            case "house":
                houses.Add(building.gameObject);
                freeHouse++;
                break;
            case "farm":
                farms.Add(building.gameObject);
                break;
            case "school":
                schools.Add(building.gameObject);
                break;
            case "museum":
                museums.Add(building.gameObject);
                break;
            case "library":
                libraries.Add(building.gameObject);
                break;
        }
        if(tobuildqueue.Count > 0) currentbuildingtobuild = tobuildqueue.Dequeue();
       
    }

    public void DestroyBuilding(GameObject building)
    {
        
    }
    
    
    public void Timepaused()
    {
        Time.timeScale = 0.0f;
    }
    public void TimeX1()
    {
        Time.timeScale = 1.0f;
    }
    public void TimeX2()
    {
        Time.timeScale = 2.0f;
    }
    public void TimeX3()
    {
        Time.timeScale = 3.0f;
    }

    

}