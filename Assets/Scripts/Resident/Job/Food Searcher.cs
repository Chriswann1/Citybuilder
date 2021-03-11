
using UnityEngine;

public class FoodSearcher : Resident
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start(); 
        this.GetComponent<MeshRenderer>().material.color = Color.red;
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }
}
