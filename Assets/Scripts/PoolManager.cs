﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance;
    
    [SerializeField]private Transform deadinstance;
    [SerializeField]private Transform activeinstance;
    [SerializeField] private Transform spawn;
    
    [SerializeField] private GameObject basicresident;
    private GameObject newresident;
    
    public List<GameObject> residents_active = new List<GameObject>();
    public List<GameObject> residents_unactive = new List<GameObject>();
    public int residents;
    

    // Start is called before the first frame update
    void Awake()//set the Singletone + adding the scene resident in list
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        for (int i = 0; i < deadinstance.childCount; i++)
        {
            residents_unactive.Add(deadinstance.GetChild(i).gameObject);
        }
        residents_active.AddRange((GameObject.FindGameObjectsWithTag("resident")).ToList());
    }

    // Update is called once per frame
    void Update()//cheat codes + counting resident
    {
        if (Input.GetKey(KeyCode.Keypad9))
        {
            spawn_resident();
        }else if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            //GameplayManager.Instance.KillRandom();
        }
        residents = residents_active.Count;
    }

    public void kill_resident(GameObject who)//the function that "kill" resident, desassign them from "alive" group/list and assign them in "dead" group/list
     {
        Debug.Log("Test");
        Resident thisresident = who.GetComponent<Resident>();
        residents_active.Remove(who);
        who.SetActive(false);
        who.transform.position = deadinstance.position;
        who.transform.parent = deadinstance;

        if (thisresident is Student || who.GetComponent<Student>() != null)
        {
            Student student = who.gameObject.GetComponent<Student>();
            Destroy(student.classtarget);
            Destroy(student);
        }
        else
        {
            Destroy(thisresident);
        }

        who.AddComponent<Hobo>();
        residents_unactive.Add(who);



    }

    public void spawn_resident()//the function that take in dead group/list and set them alive, if the dead group/list is empty, it instantiate one of them
    {
        if (residents_unactive.Count > 0)
        {
            GameObject spawningresident = residents_unactive[0];
            residents_unactive.Remove(spawningresident);
            spawningresident.transform.position = spawn.position;
            spawningresident.transform.parent = activeinstance;
            residents_active.Add(spawningresident);
            spawningresident.SetActive(true);
            
            
        }
        else
        {
            newresident = Instantiate(basicresident, deadinstance.position, Quaternion.identity);
            residents_unactive.Add(newresident);
            spawn_resident();
        }

    }



}
