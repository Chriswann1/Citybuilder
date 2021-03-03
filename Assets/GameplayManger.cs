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
       
    }

    void GiveFood()
    {
        if (food >= resident)
        {
            Eat = true;
        }
        else
        {
            List NbResident = GameObject.FindObjectsOfType(Resident.gameObject);
            int diff = resident - food;
            for (int i = 1; i < NbResident.Lenght || diff = 0; i++)
            {
                if (Random.Range(1, 3) >= 3)
                {
                    Destroy(gameObject);
                    diff--; 
                }
            }
        }
    }
}
