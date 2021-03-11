
using UnityEngine;

public class Builder : Resident
{
    // Start is called before the first frame update
    protected override void Start()
    {
        
        base.Start();   
        this.GetComponent<MeshRenderer>().material.color = Color.yellow;
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }
}
