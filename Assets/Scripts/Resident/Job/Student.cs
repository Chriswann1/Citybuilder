using System;
using UnityEngine;

public class Student : Resident
{


    [NonSerialized] public Resident classtarget;

    // Start is called before the first frame update
    protected override void Start()//Set the tag for the building where the workers, work + changing the color
    {
        buildingtag[0] = "school";
        base.Start();
        this.GetComponent<MeshRenderer>().material.color = Color.green;

    }

    // Update is called once per frame
    protected override void Update()//using resident script + adding the work behaviour
    {

        base.Update();

        if (actualbehaviour == behaviour.work)
        {
            taskpercent += taskspeed * Time.deltaTime;
            


            if (taskpercent >= 100)
            {
                Debug.Log("Trained !");
                GameplayManager.Instance.JobConvert(this, energy, Happiness, age, classtarget, true);
                actualbehaviour = behaviour.idle;
            }
        }
    }
}
