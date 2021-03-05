using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManger : MonoBehaviour
{
    public static GameplayManger Instance;
    public int food;
    public int wood;
    public int stone;
    public int resident;
    public int freeHouse;
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
        deadMan = NbResident [target];
    }
}
