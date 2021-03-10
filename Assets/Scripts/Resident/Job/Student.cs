using System;
using UnityEngine;

public class Student : Resident
{


    [NonSerialized] public Resident classtarget;

    // Start is called before the first frame update
    protected override void Start()
    {
        buildingtag = "school";
        base.Start();


    }

    // Update is called once per frame
    protected override void Update()
    {

        base.Update();

        if (actualbehaviour == behaviour.work)
        {
            taskpercent += taskspeed * Time.deltaTime;
            


            if (taskpercent >= 100)
            {
                Debug.Log("Trained !");
                GameplayManger.Instance.JobConvert(this, energy, Happiness, age, classtarget);
                actualbehaviour = behaviour.idle;
            }
        }
    }
}
