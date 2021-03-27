using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Resident : MonoBehaviour
{

    [SerializeField] private protected float minrange = 2;
    private protected Transform buildingentrance;
    public float energy = 100;
    public int age = 20;
    public bool Happiness = true;
    private protected GameObject target;
    private protected GameObject lastworkplace;
    private protected NavMeshAgent agent;
    public float taskpercent = 0;
    private protected float taskspeed = 20;
    private protected GameObject closesthouse;
    private protected const float sleepspeed = 20;
    private protected float sleeptime = 0;
    private protected Collider thiscollider;
    private protected string[] buildingtag = new string[2];
    private protected Vector3 waitingpoint;
    private const float waitrange = 5f;
    private bool iscouroutinerunning = false;

    private bool agePassed;
    private bool sleepOut;

    public enum behaviour
    {
        idle,
        work,
        gosleep,
        gowork,
        sleep,
        waiting
    }

    public behaviour actualbehaviour = behaviour.idle;
    //private int;






    // Start is called before the first frame update
    protected virtual void Start()
    {

        thiscollider = this.GetComponent<Collider>();
        if (this.TryGetComponent<NavMeshAgent>(out agent))
        {
            Debug.Log("NavMeshAgent Component Found !");
        } else {

            Debug.LogError("NavMeshAgent Component Not Found !");
            this.enabled = false;
        }

        actualbehaviour = behaviour.gowork;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (actualbehaviour != behaviour.waiting)
        {
            Happiness = true;
        }
        
        if (GameplayManager.Instance.food >= GameplayManager.Instance.resident)
        {
        
                GameplayManager.Instance.resident++;
                energy = 100;
        
        }
        
        if (GameplayManager.Instance.hour == 19 && agePassed == false)
        {
            energy = 10;
            Age();
            agePassed = true;
        }
        if (GameplayManager.Instance.hour == 20 && Happiness == false && sleepOut == false)
        {
            GameplayManager.Instance.prosperity--;
            sleepOut = true;
        }
        if (GameplayManager.Instance.hour == 20 && Happiness == true && sleepOut == false)
        {
            GameplayManager.Instance.prosperity++;
            sleepOut = true;
        }
        if (GameplayManager.Instance.hour == 2 && (agePassed == true || sleepOut == true))
        {
            agePassed = false;
            sleepOut = false;
        }

        energy = Mathf.Clamp(energy, 0, 100);

        if (actualbehaviour != behaviour.sleep && actualbehaviour != behaviour.gosleep && energy <= 10 &&
            actualbehaviour != behaviour.waiting)
        {
            closesthouse = null;
            actualbehaviour = behaviour.gosleep;
        }

            switch (actualbehaviour)
        {
            case behaviour.work:
                
                energy -= (taskspeed / 4) * Time.deltaTime;
                break;

            case behaviour.gowork:
                
                /*if (FindClosestWorkPlace(buildingtag[0]) == null && FindClosestWorkPlace(buildingtag[1]) == null)
                {
                    actualbehaviour = behaviour.waiting;
                    break;
                }*/
                if (target == null)
                {
                    if (buildingtag[1] != null && FindClosestWorkPlace(buildingtag[1]) != null)
                    {
                        target = FindClosestWorkPlace(buildingtag[1]);

                    }
                    else
                    {
                        target = FindClosestWorkPlace(buildingtag[0]);
                    }
                    


                    if (target != null)
                    {
                        buildingentrance = target.transform.GetChild(0);
                        agent.SetDestination(buildingentrance.position);
                        actualbehaviour = behaviour.gowork;
                    }
                    else
                    {
                        actualbehaviour = behaviour.waiting;
                    }

                }
                


                else if (buildingentrance != null && Vector3.Distance(transform.position, buildingentrance.position) <= minrange)
                {


                    actualbehaviour = behaviour.work;
                    Debug.Log("Starting to work !");

                }
                
   

                break;
            case behaviour.sleep:
                sleeptime += sleepspeed * Time.deltaTime;
                if (sleeptime >= 100)
                {
                    Happiness = true;
                    energy = 100;
                    sleeptime = 0;
                    actualbehaviour = behaviour.gowork;
                    target = null;
                    thiscollider.enabled = true;
                    transform.position = buildingentrance.position;
                    agent.enabled = true;
                    GameplayManager.Instance.freeHouse++;
                    Debug.Log("Exit Sleep Mode");
                }

                break;
            case behaviour.gosleep:

                if (GameplayManager.Instance.freeHouse > 0)
                {
                    if (closesthouse == null && FindClosestWorkPlace("house") != null)
                    {
                        closesthouse = FindClosestWorkPlace("house");
                        
                        buildingentrance = closesthouse.transform.GetChild(0);
                        agent.SetDestination(buildingentrance.position);
                    }
                    else if (Vector3.Distance(transform.position, buildingentrance.position) <= minrange && closesthouse != null)
                    {
                        if (GameplayManager.Instance.freeHouse > 0)
                        {
                            actualbehaviour = behaviour.sleep;
                            closesthouse = null;
                            Debug.Log("Enter in Sleep mode");
                            GameplayManager.Instance.freeHouse--;
                            thiscollider.enabled = false;
                            agent.enabled = false;
                            transform.position = GameplayManager.Instance.sleepinstance.position;
                        }
                        else
                        {
                            Debug.Log("No Free House Available !");
                            actualbehaviour = behaviour.waiting;


                        }
                    }
                    else
                    {
                        actualbehaviour = behaviour.waiting;
                    }
                }
                else
                {
                    Debug.Log("No Free House available !");

                    actualbehaviour = behaviour.waiting;


                }

                break;
            case behaviour.waiting:
                if (!iscouroutinerunning)
                {
                    StartCoroutine(WaitingMove());
                }

                if (energy <= 10 && GameplayManager.Instance.freeHouse > 0)
                {
                    closesthouse = null;
                    actualbehaviour = behaviour.gosleep;
                }
                else if (FindClosestWorkPlace(buildingtag[0]) != null)
                {
                    actualbehaviour = behaviour.gowork;
                    
                }
                target = null;
                buildingentrance = null;

                break;


        }

        
    }
    

    protected GameObject FindClosestWorkPlace(string workplacetag)
    {

        GameObject[] workplace;
        switch (workplacetag)
        {
            default:
                //Debug.LogError("Error, workplace tag not valid => "+workplacetag);
                return null;

            case "school":
                workplace = GameplayManager.Instance.schools.ToArray();
                break;
            case "house":
                workplace = GameplayManager.Instance.houses.ToArray();
                break;
            case "stones":
                workplace = GameplayManager.Instance.mines.ToArray();
                break;
            case "forest":
                workplace = GameplayManager.Instance.forest.ToArray();
                break;
            case "bush":
                workplace = GameplayManager.Instance.bush.ToArray();
                break;
            case "farm":
                workplace = GameplayManager.Instance.farms.ToArray();
                break;
            case "tobuild":
                if (GameplayManager.Instance.currentbuildingtobuild != null)
                {
                    GameObject tobuild = GameplayManager.Instance.currentbuildingtobuild.gameObject;
                    return tobuild;
                }
                else
                {
                    return null;
                }


        }

        if (workplace.Length != 0)
        {
            lastworkplace = workplace[0];
            for (int i = 1; i < workplace.Length; i++)
            {
                if (Vector3.Distance(transform.position, lastworkplace.transform.position) >
                    Vector3.Distance(transform.position, workplace[i].transform.position))
                {
                    lastworkplace = workplace[i];
                }
            }

            return lastworkplace;
        }
        else
        {
            return null;
        }

    }

    protected IEnumerator WaitingMove()
    {
            

        iscouroutinerunning = true;
        waitingpoint = transform.position;

        if (!(this is Hobo) && energy <= 10)
        {
            Happiness = false;
        }

        while (actualbehaviour == behaviour.waiting)
        {
            agent.SetDestination(new Vector3(Random.Range(waitingpoint.x - waitrange, waitingpoint.x + waitrange),
                waitingpoint.y, Random.Range(waitingpoint.z - waitrange, waitingpoint.z + waitrange)));
            yield return new WaitForSeconds(3.5f);
        }

        iscouroutinerunning = false;
        yield return null;
    }

    void Age()
    {
        if (age < 50)
        {
            age++;
        }

        if (50 <= age && age < 55)
        {
            if (Random.Range(1, 10) >= 10)
            {
                PoolManager.Instance.kill_resident(this.gameObject);
            }
            else
            {
                age++;
            }
        }

        else if (55 <= age && age < 60)
        {
            if (Random.Range(1, 10) >= 7)
            {
                PoolManager.Instance.kill_resident(this.gameObject);
            }
            else
            {
                age++;
            }
        }


        else if (60 <= age && age < 65)
        {
            if (Random.Range(1, 10) >= 4)
            {
                PoolManager.Instance.kill_resident(this.gameObject);

            }
            else
            {
                age++;
            }
            
        }
        else if (65 <= age && age < 70)
        {
            if (Random.Range(1, 10) >= 2)
            {
                PoolManager.Instance.kill_resident(this.gameObject);
            }
            else
            {
                age++;
            }
        }
        else if (age >= 70)
        {
            PoolManager.Instance.kill_resident(this.gameObject);
        }
        else

        {
            age++;
        }
    }


    
}
