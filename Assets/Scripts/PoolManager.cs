using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] private Transform deadinstance;
    [SerializeField] private Transform activeinstance;
    
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

    void kill_resident(Resident who)
    {
        GameObject thisresidentobject = who.gameObject;
        residents_active.Remove(thisresidentobject);
        thisresidentobject.SetActive(false);
        thisresidentobject.transform.position = deadinstance.position;
        thisresidentobject.transform.parent = deadinstance;

        if (who is Student || who.gameObject.GetComponent<Student>() != null)
        {
            Student student = who.gameObject.GetComponent<Student>();
            Destroy(student.classtarget);
            Destroy(student);
        }
        else
        {
            Destroy(who);
        }

        thisresidentobject.AddComponent<Hobo>();
        residents_unactive.Add(thisresidentobject);



    }

    void spawn_resident()
    {
        if (residents_unactive.Count > 0)
        {
            GameObject spawningresident = residents_unactive[0];
            residents_unactive.Remove(spawningresident);
            spawningresident.transform.position = activeinstance.position;
            spawningresident.transform.parent = activeinstance;
            residents_active.Add(spawningresident);
            spawningresident.SetActive(true);


        }

    }



}
