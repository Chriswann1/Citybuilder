using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildManagerTP : MonoBehaviour  //##HAVE TO BE RENAMED TO BuildManager##//
{
    public GameObject building1, building2;
    public GameObject farm, school, museum, library, house;
    public GameObject objectContainer;
    public GameObject objectInHand = null;
    public bool inHand = false;
    public Vector3 spawnCoordinates;
    [SerializeField] private Terrain _terrain;
    float radius = 5;
    [SerializeField] LayerMask buildingLayer;
    private GameObject spawnedbuilding;

    public Button farmButton, houseButton, libraryButton, museumButton, schoolButton;

    void Update()
    {
        farmButton.interactable = false;
        houseButton.interactable = false;
        libraryButton.interactable = false;
        museumButton.interactable = false;
        schoolButton.interactable = false;

        if (Input.GetKeyDown("0"))
        {
            if (inHand)
            {
                Destroy(objectInHand);
                objectInHand = null;
                inHand = false;
                
            }
        }

        #region Call the spawn of building in hand

        //Put a building in hand. If it's already in hand, take it off

        //farm
        if (GameplayManager.Instance.wood >= 15 && GameplayManager.Instance.stone >= 10)
        {
            farmButton.interactable = true;
            if (Input.GetKeyDown("1"))
            {
                Invoke("SpawnFarm", 0.0f);
            }
        }

        //house
        if (GameplayManager.Instance.wood >= 8 && GameplayManager.Instance.stone >= 4)
        {
            houseButton.interactable = true;
            if (Input.GetKeyDown("2"))
            {
                Invoke("SpawnHouse", 0.0f);
            }
        }
        
        //library
        if (GameplayManager.Instance.wood >= 25 && GameplayManager.Instance.stone >= 10)
        {
            libraryButton.interactable = true;
            if (Input.GetKeyDown("3"))
            {
                Invoke("SpawnLibrary", 0.0f);
            }
        }
        
        //museum
        if (GameplayManager.Instance.wood >= 30 && GameplayManager.Instance.stone >= 35)
        {
            museumButton.interactable = true;
            if (Input.GetKeyDown("4"))
            {
                Invoke("SpawnMuseum", 0.0f);
            }
        }
        
        //school
        if (GameplayManager.Instance.wood >= 10 && GameplayManager.Instance.stone >= 15)
        {
            schoolButton.interactable = true;
            if (Input.GetKeyDown("5"))
            {
                Invoke("SpawnSchool", 0.0f);
            }
        }
        


        //tests
        /*
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
        */
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
                Debug.Log(_terrain.SampleHeight(spawnCoordinates));
                if (tag == "farm")
                {
                    CheckRessources(15, 10);
                    spawnedbuilding = Instantiate(farm, new Vector3(spawnCoordinates.x,-objectInHand.transform.GetChild(0).transform.position.y+_terrain.SampleHeight(spawnCoordinates)*2, spawnCoordinates.z), transform.rotation, objectContainer.transform);
                    Destroy(objectInHand);
                    objectInHand = null;
                    inHand = false;
                }

                if (tag == "school")
                {
                    CheckRessources(10, 15);
                    spawnedbuilding = Instantiate(school, new Vector3(spawnCoordinates.x,-objectInHand.transform.GetChild(0).transform.position.y+_terrain.SampleHeight(spawnCoordinates)*2, spawnCoordinates.z), transform.rotation, objectContainer.transform);
                    Destroy(objectInHand);
                    objectInHand = null;
                    inHand = false;
                }

                if (tag == "museum")
                {
                    CheckRessources(30, 35);

                    spawnedbuilding = Instantiate(museum, new Vector3(spawnCoordinates.x,-objectInHand.transform.GetChild(0).transform.position.y+_terrain.SampleHeight(spawnCoordinates)*2, spawnCoordinates.z), transform.rotation, objectContainer.transform);

                    GameplayManager.Instance.prosperity += 18;
                    spawnedbuilding = Instantiate(museum, spawnCoordinates, transform.rotation, objectContainer.transform);

                    Destroy(objectInHand);
                    objectInHand = null;
                    inHand = false;
                }

                if (tag == "library")
                {
                    CheckRessources(25, 10);

                    spawnedbuilding = Instantiate(library, new Vector3(spawnCoordinates.x,-objectInHand.transform.GetChild(0).transform.position.y+_terrain.SampleHeight(spawnCoordinates)*2, spawnCoordinates.z), transform.rotation, objectContainer.transform);

                    GameplayManager.Instance.prosperity += 8;
                    spawnedbuilding = Instantiate(library, spawnCoordinates, transform.rotation, objectContainer.transform);

                    Destroy(objectInHand);
                    objectInHand = null;
                    inHand = false;
                }

                if (tag == "house")
                {
                    CheckRessources(8, 4);
                    spawnedbuilding = Instantiate(house, new Vector3(spawnCoordinates.x,-objectInHand.transform.GetChild(0).transform.position.y+_terrain.SampleHeight(spawnCoordinates)*2, spawnCoordinates.z), transform.rotation, objectContainer.transform);
                    Destroy(objectInHand);
                    objectInHand = null;
                    inHand = false;
                }


                //test
                if (tag == "test1")
                {
                    spawnedbuilding = Instantiate(building1, new Vector3(spawnCoordinates.x,-objectInHand.transform.GetChild(0).transform.position.y, spawnCoordinates.z), transform.rotation, objectContainer.transform);
                    Destroy(objectInHand);
                    objectInHand = null;
                    inHand = false;
                }
                if (tag == "test2")
                {
                    spawnedbuilding = Instantiate(building2, new Vector3(spawnCoordinates.x,-objectInHand.transform.GetChild(0).transform.position.y, spawnCoordinates.z), transform.rotation, objectContainer.transform);
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

    #region Spawn building in hand
    public void SpawnFarm()
    {
        if (inHand)
        {
            string tag = objectInHand.tag;
            Destroy(objectInHand);
            objectInHand = null;
            inHand = false;
            if (tag != "farm")
            {
                objectInHand = farm;
                objectInHand = Instantiate(farm);
                inHand = true;
            }
        }
        else
        {
            objectInHand = farm;
            objectInHand = Instantiate(farm);
            inHand = true;
        }
    }

    public void SpawnHouse()
    {
        if (inHand)
        {
            string tag = objectInHand.tag;
            Destroy(objectInHand);
            objectInHand = null;
            inHand = false;
            if (tag != "house")
            {
                objectInHand = house;
                objectInHand = Instantiate(house);
                inHand = true;
            }
        }
        else
        {
            objectInHand = house;
            objectInHand = Instantiate(house);
            inHand = true;
        }
    }

    public void SpawnLibrary()
    {
        if (inHand)
        {
            string tag = objectInHand.tag;
            Destroy(objectInHand);
            objectInHand = null;
            inHand = false;
            if (tag != "library")
            {
                objectInHand = library;
                objectInHand = Instantiate(library);
                inHand = true;
            }
        }
        else
        {
            objectInHand = library;
            objectInHand = Instantiate(library);
            inHand = true;
        }
    }

    public void SpawnMuseum()
    {
        if (inHand)
        {
            string tag = objectInHand.tag;
            Destroy(objectInHand);
            objectInHand = null;
            inHand = false;
            if (tag != "museum")
            {
                objectInHand = museum;
                objectInHand = Instantiate(museum);
                inHand = true;
            }
        }
        else
        {
            objectInHand = museum;
            objectInHand = Instantiate(museum);
            inHand = true;
        }
    }

    public void SpawnSchool()
    {
        if (inHand)
        {
            string tag = objectInHand.tag;
            Destroy(objectInHand);
            objectInHand = null;
            inHand = false;
            if (tag != "school")
            {
                objectInHand = school;
                objectInHand = Instantiate(school);
                inHand = true;
            }
        }
        else
        {
            objectInHand = school;
            objectInHand = Instantiate(school);
            inHand = true;
        }
    }
    #endregion

    public void CheckRessources(int woodRequired, int stoneRequired)
    {
        GameplayManager.Instance.wood -= woodRequired;
        GameplayManager.Instance.stone -= stoneRequired;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(spawnCoordinates, radius);
    }
}
