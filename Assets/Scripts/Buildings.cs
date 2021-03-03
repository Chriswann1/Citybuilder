using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildings : MonoBehaviour
{
    public GameObject building1, building2;
    public GameObject objectInHand = null;

    void Update()
    {
        //Put a building in hand. If it's already in hand, take it off
        if (Input.GetKeyDown("a"))
        {
            if (objectInHand == building1)
            {
                objectInHand = null;
            }
            else
            {
                objectInHand = building1;
            }
        }
        if (Input.GetKeyDown("z"))
        {
            if (objectInHand == building2)
            {
                objectInHand = null;
            }
            else
            {
                objectInHand = building2;
            }
        }

        if (objectInHand != null)
        {
            objectInHand.transform.position = Input.mousePosition;
            Debug.Log("Position of the object in hand = " + objectInHand.transform.position);
        }
    }
}
