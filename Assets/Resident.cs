using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resident : MonoBehaviour
{
    [SerializeField]
    private int energy;
    [SerializeField]
    private int age = 20;
    [SerializeField]
    private bool Happiness;


    // Start is called before the first frame update
    void Start()
    {
        GameplayManger.Instance.resident = GameplayManger.Instance.resident + 1;
        energy = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameplayManger.Instance.time == 19)
        {
            energy = 10;
        }
    }
    void Age()
    {
        if (age < 50)
        {
            age++;
        }
        if (50 <= age && age < 55)
        {
            if (Random.Range(1,10)>= 10)
            {
                Destroy(gameObject);
            }
            else
            {
                age++;
            }           
        }

        else if (55 <= age && age < 60)
        {
            if (Random.Range(1, 10) >= 7)
            {
                Destroy(gameObject);
            }
            else
            {
                age++;
            }
        }

        else if (60 <= age && age < 65)
        {
            if (Random.Range(1, 10) >= 4)
            {
                Destroy(gameObject);
            }
            else
            {
                age++;
            }
        }

        else if (65 <= age && age < 70)
        {
            if (Random.Range(1, 10) >= 2)
            {
                Destroy(gameObject);
            }
            else
            {
                age++;
            }
        }

        else if (age <= 70)
        {
            Destroy(gameObject);
        }
    }
}
