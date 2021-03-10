using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManger : MonoBehaviour
{
    public static GameplayManger Instance;
    public int food;
    public int wood;
    public int stone;
    public int resident = 0;
    public int freeHouse;
    private int target;
    private GameObject deadMan;
    public float time;
    public int prosperity;
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
        
        if (time == 19)
        GiveFood();
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
   
}
