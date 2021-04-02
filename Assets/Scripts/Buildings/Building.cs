using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public string name;
    public float percentbuilded = 0;
    private bool builded;
    
    // Start is called before the first frame update
    void Start() //set the building bool + adding it to the Gameplaymanager queue (to let the builder build it)
    {
        builded = false;
        percentbuilded = 0;
        GameplayManager.Instance.tobuildqueue.Enqueue(this);
    }

    // Update is called once per frame
    void Update() //Check if builder builded it
    {
        if (percentbuilded >= 100 && !builded)
        {
            GameplayManager.Instance.ActivateBuilding(this);
			if(this.GetComponent<Museum>() != null){
				GameplayManager.Instance.prosperity += 18;
				
			}else if(this.GetComponent<Library>() != null){
				GameplayManager.Instance.prosperity += 8;
				
			}
            builded = true;
        } 
    }
}
