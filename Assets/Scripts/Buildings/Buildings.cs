using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildings : MonoBehaviour
{
    public GameObject building1, building2;
    public GameObject objectInHand = null;
    public bool inHand = false;


    void Update()
    {
        //Put a building in hand. If it's already in hand, take it off
        if (Input.GetKeyDown("a"))
        {
            if (inHand)
            {
                string tag = objectInHand.tag;
                Destroy(objectInHand);
                objectInHand = null;
                inHand = false;
                if (tag != "test1")
                {
                    objectInHand = building1;
                    objectInHand = Instantiate(building1);
                    inHand = true;
                }
            }
            else
            {
                objectInHand = building1;
                objectInHand = Instantiate(building1);
                inHand = true;
            }
        }
        if (Input.GetKeyDown("z"))
        {
            if (inHand)
            {
                string tag = objectInHand.tag;
                Destroy(objectInHand);
                objectInHand = null;
                inHand = false;
                if (tag != "test2")
                {
                    objectInHand = building2;
                    objectInHand = Instantiate(building2);
                    inHand = true;
                }
            }
            else
            {
                objectInHand = building2;
                objectInHand = Instantiate(building2);
                inHand = true;
            }
        }

        if (objectInHand != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, 1<<LayerMask.NameToLayer("Terrain")))
                {
                objectInHand.transform.position = hit.point;
                Debug.Log(objectInHand.transform.position);
            }
        }
    }
}
