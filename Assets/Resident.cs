using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resident : MonoBehaviour
{
    private bool Eat = false;
    private bool Sleep = false;
    private int age = 20;
    private bool Happiness; 
    //private int;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameplayManger.Instance.food >= GameplayManger.Instance.resident)
        {
            Eat = true;
        }
        else
        {
            int diff = GameplayManger.Instance.resident - GameplayManger.Instance.food;
            for (int i = 1; i <= diff; i++)
            {
                if(Random.Range(1,3) >= 3)
                {
                    Destroy(gameObject);
                }
            }
        }

        if (age <= 50)
        {
            if (Random.Range(1,10)>= 10)
            {
                Destroy(gameObject);
            }            
        }
        if (age <= 55)
        {
            if (Random.Range(1, 10) >= 7)
            {
                Destroy(gameObject);
            }
        }
        if (age <= 60)
        {
            if (Random.Range(1, 10) >= 4)
            {
                Destroy(gameObject);
            }
        }
        if (age <= 65)
        {
            if (Random.Range(1, 10) >= 2)
            {
                Destroy(gameObject);
            }
        }
        if (age <= 70)
        {
            Destroy(gameObject);
        }
    }
}
