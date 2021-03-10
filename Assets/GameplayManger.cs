using UnityEngine;

public class GameplayManger : MonoBehaviour
{
    public static GameplayManger Instance;
    public int food;
    public int wood;
    public int stone;
    public int resident;
    public int freeHouse;

    private int housenumber;
    public Transform sleepinstance;
    public Buildings tobuildlist;

    public Resident testminer, testtimber;
    

    private int target;
    private GameObject deadMan;

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
        housenumber = GameObject.FindGameObjectsWithTag("house").Length;
        freeHouse = housenumber;
        JobConvert(testminer, testminer.Eat, testminer.Sleep, testminer.Happiness, testminer.age,  testminer.gameObject.AddComponent<Miner>());
        JobConvert(testtimber, testtimber.Eat, testtimber.Sleep, testtimber.Happiness, testtimber.age,  testtimber.gameObject.AddComponent<Timber>());
    }

    // Update is called once per frame
    void Update()
    {
        GiveFood();
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
    }
    void KillRandom()
    {
        Resident[] NbResident = FindObjectsOfType<Resident>();
        target = Random.Range(0, NbResident.Length);
        //deadMan = NbResident [target];
    }

    public void JobConvert(Resident who, bool isstarving, bool isenergied, bool ishappy, int age, Resident targetedjob)
    {
        GameObject whoobject = who.gameObject;
        if (!(who is Student))
        {
            Destroy(who);
            Student whostudent = whoobject.AddComponent<Student>();
            whostudent.classtarget = targetedjob;
            whostudent.classtarget.enabled = false;
            whostudent.age = age;
            whostudent.Eat = isstarving;
            whostudent.Sleep = isenergied;
            whostudent.Happiness = ishappy;


        }
        else
        {
            Student whostudent = who.gameObject.GetComponent<Student>();
            if (whostudent.classtarget != null)
            {
                whostudent.classtarget.age = age;
                whostudent.classtarget.Eat = isstarving;
                whostudent.classtarget.Sleep = isenergied;
                whostudent.classtarget.Happiness = ishappy;
                whostudent.classtarget.enabled = true;
                Destroy(who);
            }


            

        }
    }

}
