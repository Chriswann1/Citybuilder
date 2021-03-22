using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    static public List<GameObject> residents_active = new List<GameObject>();
    static public List<GameObject> residents_unactive = new List<GameObject>();
    

    // Start is called before the first frame update
    void Awake()
    {
        residents_active.AddRange((GameObject.FindGameObjectsWithTag("resident")).ToList());
    }

    // Update is called once per frame
    void Update()
    {
        
    }



}
