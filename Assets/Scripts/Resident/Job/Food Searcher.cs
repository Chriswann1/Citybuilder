
using UnityEngine;

public class FoodSearcher : Resident
{
    // Start is called before the first frame update
    protected override void Start()
    {
        taskspeed = 10f;
        buildingtag[0] = "bush";
        buildingtag[1] = "farm";
        base.Start(); 
        this.GetComponent<MeshRenderer>().material.color = Color.red;

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (actualbehaviour == behaviour.work)
        {
            if (target.CompareTag("farm"))
            {
                taskpercent += taskspeed * Time.deltaTime * 5;
            }
            else
            {
                taskpercent += taskspeed * Time.deltaTime;
            }

            if (taskpercent >= 100)
            {
                GameplayManager.Instance.food++;
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
