
using UnityEngine;

public class Builder : Resident
{
    // Start is called before the first frame update
    protected override void Start()
    {
        buildingtag[0] = "tobuild";
        taskspeed = 5f;
        base.Start();   
        this.GetComponent<MeshRenderer>().material.color = Color.yellow;
        
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (actualbehaviour == behaviour.work)
        {
            if (GameplayManager.Instance.currentbuildingtobuild == null || target != GameplayManager.Instance.currentbuildingtobuild.gameObject || GameplayManager.Instance.currentbuildingtobuild.percentbuilded >= 100)
            {
                actualbehaviour = behaviour.waiting;
                Debug.Log("Test");
                
            }
            else
            {
                GameplayManager.Instance.currentbuildingtobuild.percentbuilded += taskspeed * Time.deltaTime;
            }


        }
        
    }
}
