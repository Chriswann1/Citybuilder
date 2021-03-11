
using UnityEngine;

public class Timber : Resident
{
    // Start is called before the first frame update
    protected override void Start()
    {
        buildingtag[0] = "forest";
        base.Start(); 
        this.GetComponent<MeshRenderer>().material.color = Color.blue;
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
                GameplayManger.Instance.wood++;
                taskpercent = 0;
                if (Vector3.Distance(transform.position, buildingentrance.position) >= minrange)
                {
                    actualbehaviour = behaviour.gowork;
                    target = null;
                }
            }
        }
    }
}
