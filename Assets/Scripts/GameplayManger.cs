using System.Collections;
using UnityEngine;


public class GameplayManger : MonoBehaviour
{
    public static GameplayManger Instance;
    public int food;
    public int wood;
    public int stone;
    public int resident = 0;
    public int freeHouse;

    private int housenumber;
    public Transform sleepinstance;
    public Buildings tobuildlist;

    public Resident testminer, testtimber;
    

    private int target;
    private GameObject deadMan;


    public int day;
    public int time;
    public int prosperity;
    public bool paused;

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
        //JobConvert(testminer, testminer.energy, testminer.Happiness, testminer.age,  testminer.gameObject.AddComponent<Miner>());
        //JobConvert(testtimber, testtimber.energy, testtimber.Happiness, testtimber.age,  testtimber.gameObject.AddComponent<Timber>());

        StartCoroutine("Time");

    }

    // Update is called once per frame
    void Update()
    {
        
        if (time == 19) GiveFood();
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
        Resident[] NbResident = FindObjectsOfType<Resident>();
        target = Random.Range(0, NbResident.Length);
        deadMan = NbResident [target].gameObject;
        resident = resident - 1;
    }


    public void JobConvert(Resident who, int energy, bool ishappy, int age, Resident targetedjob)
    {
        GameObject whoobject = who.gameObject;
        if (!(who is Student))
        {
            Destroy(who);
            Student whostudent = whoobject.AddComponent<Student>();
            whostudent.classtarget = targetedjob;
            whostudent.classtarget.enabled = false;
            whostudent.age = age;
            whostudent.energy = energy;
            whostudent.Happiness = ishappy;


        }
        else
        {
            Student whostudent = who.gameObject.GetComponent<Student>();
            if (whostudent.classtarget != null)
            {
                whostudent.classtarget.age = age;
                whostudent.classtarget.energy = energy;
                whostudent.classtarget.Happiness = ishappy;
                whostudent.classtarget.enabled = true;
                Destroy(who);
            }


            

        }
    }
    IEnumerator Time()
    {
        while(!paused)
        {                
            time = time++;
            yield return new WaitForSeconds(5f);
            
            if (time == 24)
            {
                time = 0;
                day++;
            }

            if (time == 25)
            {
                Debug.Log("25Hin1day");
                
            }
        }
        yield return null;
    }
    
}
