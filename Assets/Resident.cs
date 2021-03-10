using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Resident : MonoBehaviour
{
    [SerializeField] private protected float minrange = 2;
    private protected Transform buildingentrance;
    public bool Eat = false;
    public bool Sleep = false;
    public int age = 20;
    public bool Happiness;
    private protected GameObject target;
    private protected GameObject lastworkplace;
    private protected NavMeshAgent agent;
    private protected float taskpercent = 0;
    private protected float taskspeed = 20;
    private protected GameObject closesthouse;
    private protected float sleepspeed = 20;
    private protected float sleeptime = 0;
    private protected Collider thiscollider;
    private protected string buildingtag;
    private protected Vector3 waitingpoint;
    private const float waitrange = 5f;
    
    protected enum behaviour
    {
        idle,
        work,
        gosleep,
        gowork,
        sleep,
        waiting
    }

    [SerializeField] protected behaviour actualbehaviour = behaviour.idle;
    //private int;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        thiscollider = this.GetComponent<Collider>();
        if (this.TryGetComponent<NavMeshAgent>(out agent))
        {
            Debug.Log("NavMeshAgent Component Found !");
        }else{
            
            Debug.LogError("NavMeshAgent Component Not Found !");
        }

        actualbehaviour = behaviour.gowork;
    }

    // Update is called once per frame
    protected virtual void Update()
    {/*
        if (GameplayManger.Instance.food >= GameplayManger.Instance.resident)
        {
            Eat = true;
        }
        else
        {
            int diff = GameplayManger.Instance.resident - GameplayManger.Instance.food;
            for (int i = 1; i <= diff; i++)
            {
                if(Random.Range(1,3) >= 3)
                {
                    Destroy(gameObject);
                }
            }
        }

        if (age <= 50)
        {
            if (Random.Range(1,10)>= 10)
            {
                Destroy(gameObject);
            }            
        }
        if (age <= 55)
        {
            if (Random.Range(1, 10) >= 7)
            {
                Destroy(gameObject);
            }
        }
        if (age <= 60)
        {
            if (Random.Range(1, 10) >= 4)
            {
                Destroy(gameObject);
            }
        }
        if (age <= 65)
        {
            if (Random.Range(1, 10) >= 2)
            {
                Destroy(gameObject);
            }
        }
        if (age <= 70)
        {
            Destroy(gameObject);
        }*/
        if (actualbehaviour != behaviour.sleep && actualbehaviour != behaviour.gosleep && Sleep && actualbehaviour != behaviour.waiting) actualbehaviour = behaviour.gosleep;

        switch (actualbehaviour)
        {


            case behaviour.gowork:
                if (target == null)
                {
                    target = FindClosestWorkPlace(buildingtag);
                    buildingentrance = target.transform.GetChild(0);
                    if (target != null)
                    {
                        agent.SetDestination(buildingentrance.position);
                        actualbehaviour = behaviour.gowork;
                    }

                }
                else
                {
                    if (actualbehaviour == behaviour.gowork &&
                        Vector3.Distance(transform.position, buildingentrance.position) <= minrange)
                    {
                        actualbehaviour = behaviour.work;
                        Debug.Log("Starting to work !");

                    }

                }

                break;
            case behaviour.sleep:
                sleeptime += sleepspeed * Time.deltaTime;
                if (sleeptime >= 100)
                {
                    Sleep = false;
                    sleeptime = 0;
                    actualbehaviour = behaviour.gowork;
                    target = null;
                    thiscollider.enabled = true;
                    transform.position = buildingentrance.position;
                    agent.enabled = true;
                    GameplayManger.Instance.freeHouse++;
                    Debug.Log("Exit Sleep Mode");
                }
                break;
            case behaviour.gosleep:
                if (GameplayManger.Instance.freeHouse > 0)
                {
                    if (closesthouse == null)
                    {
                        closesthouse = FindClosestWorkPlace("house");
                        buildingentrance = closesthouse.transform.GetChild(0);
                        agent.SetDestination(buildingentrance.position);
                    }
                    else if (Vector3.Distance(transform.position, buildingentrance.position) <= minrange)
                    {
                        if (GameplayManger.Instance.freeHouse > 0)
                        {
                            actualbehaviour = behaviour.sleep;
                            closesthouse = null;
                            Debug.Log("Enter in Sleep mode");
                            GameplayManger.Instance.freeHouse--;
                            thiscollider.enabled = false;
                            agent.enabled = false;
                            transform.position = GameplayManger.Instance.sleepinstance.position;
                        }
                        else
                        {
                            Debug.Log("No Free House Available !");
                            waitingpoint = transform.position;
                            actualbehaviour = behaviour.waiting;
                            closesthouse = null;
                            StartCoroutine(WaitingMove());
                        }
                        
                    }
                }
                else
                {
                    Debug.Log("No Free House available !");
                    waitingpoint = transform.position;
                    actualbehaviour = behaviour.waiting;
                    closesthouse = null;
                    StartCoroutine(WaitingMove());
                }
                break;
            case behaviour.waiting:
                if (Sleep && GameplayManger.Instance.freeHouse > 0)
                {
                    actualbehaviour = behaviour.gosleep;
                }
                break;
            
            
        }
        
    }

    protected void Convert(Resident targetedjob)
    {
        GameplayManger.Instance.JobConvert(this, Eat, Sleep, Happiness, age, targetedjob);
    }
    
    protected GameObject FindClosestWorkPlace(string workplacetag)
    {
        
        GameObject[] workplace = GameObject.FindGameObjectsWithTag(workplacetag);
        if (workplace.Length != 0)
        {
            lastworkplace = workplace[0];
            for (int i = 1; i < workplace.Length; i++)
            {
                if (Vector3.Distance(transform.position, lastworkplace.transform.position) > Vector3.Distance(transform.position, workplace[i].transform.position))
                {
                    lastworkplace = workplace[i];
                }
            }
        }

        return lastworkplace;
    }

    protected IEnumerator WaitingMove()
    {
        while (actualbehaviour == behaviour.waiting)
        {
            agent.SetDestination(new Vector3(Random.Range(waitingpoint.x - waitrange, waitingpoint.x + waitrange),
                waitingpoint.y, Random.Range(waitingpoint.z - waitrange, waitingpoint.z + waitrange)));
            yield return new WaitForSeconds(3.5f);
        }

        yield return null;
    }


    
}
