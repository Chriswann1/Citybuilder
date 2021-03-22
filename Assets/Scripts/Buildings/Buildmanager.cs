
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildmanager : MonoBehaviour
{
    GameplayManager manager = GameplayManager.Instance;
    // Start is called before the first frame update


    // Update is called once per frame
   


    // Update is called once per frame
    void BuildFarm()
    {
        manager.wood = manager.wood - 15;
        manager.stone = manager.stone - 5;
    }
    void BuildHouse()
    {
        manager.wood = manager.wood - 5;
        manager.stone = manager.stone - 2;
    }
    void BuildSchool()
    {
        manager.wood = manager.wood - 10;
        manager.stone = manager.stone - 5;
    }
    void BuildLibrary()
    {
        manager.wood = manager.wood - 20;
        manager.stone = manager.stone - 10;
    }
    void BuildMuseum()
    {
        manager.wood = manager.wood - 30;
        manager.stone = manager.stone - 15;
    }
    void DestroyFarm()
    {
        Destroy(gameObject);
        manager.wood = manager.wood + 4;
        manager.stone = manager.stone  + 2;
    }
    void DestroyLibrary()
    {
        manager.prosperity = GameplayManager.Instance.prosperity - 3;
        Destroy(gameObject);
        manager.wood = manager.wood + 8;
        manager.stone = manager.stone + 4;
    }
    void DestroyMuseum()
    {
        manager.prosperity = GameplayManager.Instance.prosperity - 5;
        Destroy(gameObject);
        manager.wood = manager.wood + 10;
        manager.stone = manager.stone + 6;
    }
    void DestroySchool()
    {
        Destroy(gameObject);
        manager.wood = manager.wood + 4;
        manager.stone = manager.stone + 2;
    }
    void DestroyHouse()
    {
        Destroy(gameObject);
        manager.wood = manager.wood + 2;
        
    }
}
