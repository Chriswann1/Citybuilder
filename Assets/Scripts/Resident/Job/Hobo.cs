
using UnityEngine;

public class Hobo : Resident
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start(); 
        this.GetComponent<MeshRenderer>().material.color = Color.white;
        actualbehaviour = behaviour.waiting;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
