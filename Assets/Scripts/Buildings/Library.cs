using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Library : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameplayManager.Instance.prosperity = GameplayManager.Instance.prosperity + 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
