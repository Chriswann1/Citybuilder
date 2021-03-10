using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildmanage : MonoBehaviour
{
    GameplayManger manager = GameplayManger.Instance;
    // Start is called before the first frame update


    // Update is called once per frame
    void BuildFarm()
    {
        manager.wood = manager.wood - 15;
        manager.stone = manager.stone - 5;
    }
    void BuildHouse()
    {

    }
    void BuildSchool()
    {

    }
    void BuildLibrary()
    {

    }
    void BuildMuseum()
    {

    }
    void DestroyFarm()
    {
        Destroy(gameObject);
    }
    void DestroyLibrary()
    {
        manager.prosperity = GameplayManger.Instance.prosperity - 3;
        Destroy(gameObject);
    }
    void DestroyMuseum()
    {
        manager.prosperity = GameplayManger.Instance.prosperity - 5;
        Destroy(gameObject);
    }
    void DestroySchool()
    {
        Destroy(gameObject);
    }
    void DestroyHouse()
    {
        Destroy(gameObject);
    }
}
