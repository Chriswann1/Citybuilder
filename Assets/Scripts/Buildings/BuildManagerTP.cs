using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManagerTP : MonoBehaviour  //##HAVE TO BE RENAMED TO BuildManager##//
{
    public GameObject building1, building2;
    public GameObject farm, school, museum, library, house;
    public GameObject objectContainer;
    public GameObject objectInHand = null;
    public bool inHand = false;
    public Vector3 spawnCoordinates;
    float radius = 5;
    [SerializeField] LayerMask buildingLayer;
    private GameObject spawnedbuilding;

    private void Start()
    {
        
    }

    void Update()
    {
        #region Spawn building in hand

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
                objectInHand = Instantiate(farm);
                inHand = true;
            }
        }
        #endregion

        #region Building in hand follow mouse

        if (objectInHand != null) //If you have something in hand
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100, 1<<LayerMask.NameToLayer("ressources"))) //If the raycast touch the ground (which has the layer "ressources")
            {
                objectInHand.transform.position = hit.point;
                Debug.Log(objectInHand.transform.position);
                
                spawnCoordinates = objectInHand.transform.position;

            }
        }
        #endregion

        #region Spawn building on Ground

        if (Input.GetMouseButtonDown(0) && objectInHand != null) //If you have something in hand and you left click
        {
            string tag = objectInHand.tag;
            Collider[] hitColliders = Physics.OverlapSphere(spawnCoordinates, radius, buildingLayer);
            Debug.Log(hitColliders[0].name);
            if (hitColliders.Length == 1) //If there's no building in the radius (except the one in your hand)
            {
                if (tag == "farm")
                {
                    spawnedbuilding = Instantiate(farm, new Vector3(spawnCoordinates.x,-objectInHand.transform.GetChild(0).transform.position.y, spawnCoordinates.z), transform.rotation, objectContainer.transform);
                    Destroy(objectInHand);
                    objectInHand = null;
                    inHand = false;
                }

                if (tag == "school")
                {
                    spawnedbuilding = Instantiate(school, spawnCoordinates, transform.rotation, objectContainer.transform);
                    Destroy(objectInHand);
                    objectInHand = null;
                    inHand = false;
                }

                if (tag == "museum")
                {
                    spawnedbuilding = Instantiate(museum, spawnCoordinates, transform.rotation, objectContainer.transform);
                    Destroy(objectInHand);
                    objectInHand = null;
                    inHand = false;
                }

                if (tag == "library")
                {
                    spawnedbuilding = Instantiate(library, spawnCoordinates, transform.rotation, objectContainer.transform);
                    Destroy(objectInHand);
                    objectInHand = null;
                    inHand = false;
                }

                if (tag == "house")
                {
                    spawnedbuilding = Instantiate(house, spawnCoordinates, transform.rotation, objectContainer.transform);
                    Destroy(objectInHand);
                    objectInHand = null;
                    inHand = false;
                }


                //test
                if (tag == "test1")
                {
                    spawnedbuilding = Instantiate(building1, spawnCoordinates, transform.rotation, objectContainer.transform);
                    Destroy(objectInHand);
                    objectInHand = null;
                    inHand = false;
                }
                if (tag == "test2")
                {
                    spawnedbuilding = Instantiate(building2, spawnCoordinates, transform.rotation, objectContainer.transform);
                    Destroy(objectInHand);
                    objectInHand = null;
                    inHand = false;
                }

                if (spawnedbuilding.GetComponent<Building>() != null)
                {
                    spawnedbuilding.GetComponent<Building>().enabled = true;
                }
            }
        }
        #endregion
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(spawnCoordinates, radius);
    }
}
