using UnityEngine;
using UnityEngine.AI;

public class Student : MonoBehaviour
{
    GameObject lastschool;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    GameObject FindClosestSchool()
    {
        
        GameObject[] schools = GameObject.FindGameObjectsWithTag("school");
        if (schools.Length != 0)
        {
            lastschool = schools[0];
            for (int i = 1; i < schools.Length; i++)
            {
                if (Vector3.Distance(transform.position, lastschool.transform.position) > Vector3.Distance(transform.position, schools[i].transform.position))
                {
                    lastschool = schools[i];
                }
            }
        }

        return lastschool;
    }
}
