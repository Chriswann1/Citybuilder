using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public string name;
    public int[] cost;
    public float percentbuilded;
    private bool builded;
    
    // Start is called before the first frame update
    void Start()
    {
        builded = false;
        percentbuilded = 0;
        GameplayManager.Instance.tobuildqueue.Enqueue(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (percentbuilded >= 100 && !builded)
        {
            GameplayManager.Instance.ActivateBuilding(this);
            builded = true;
        } 
    }
}
