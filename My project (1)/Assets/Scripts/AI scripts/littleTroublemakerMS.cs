using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Video;

public class littleTroublemakerMS : MonoBehaviour
{
    public MoneyCounter canInteractWO;
    public NavMeshAgent navMeshAgent;

    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionPointRadius = 0.5f;
    [SerializeField] private LayerMask _interactableMask;

    private readonly Collider[] _colliders = new Collider[3];
    public int _numFound;
    private IInteractable _interactable;

    private Vector3 pos;
    private int ObjGoTo = 0;

    private bool Failsafe = false;
    protected float failsafetimer;
    private int failsafeDelay = 5;


    //Roll the dice at the start to decide first appliance and go to it.
    private void Start()
    {
        //Function that gets a random number from a given range
        //This number is the same as the ID of an appliance
        ObjID();
    }

    //Custom Function used to get the location/position of the appliance and pass in the cordinates to the AI to go to.
    private void GoToAppliance(bool app_state, string lookfor_app)
    {
        //Check to see if the chosen appliance is turned on
        //If that is the case try to get a new appliance that is off
        if (app_state == false)
        {
            //Get cords
            pos = GameObject.Find(lookfor_app).transform.position;
            //Go to cords
            navMeshAgent.SetDestination(pos);

            //Oriantate the AI to face the object, helpful when the AI has to do tight corner turns
            if (DistanceCheck() == true) transform.LookAt(pos);

            //Interact with object when in range
            if (_numFound > 0)
            {
                _interactable = _colliders[0].GetComponent<IInteractable>();
                _interactable.InteractAI(this);
            }
            else
            {
                if (_interactable != null) _interactable = null;
            }
        }
        //Run last check to see if there is 1 last appliance that is not on
        else
        {
            ObjID();
            navMeshAgent.SetDestination(pos);
            //If all appliances are on go to a predesignated spot
            //This is here to fix an issue where the in case of every appliance being on
            //The AI would camp and constantly interact (turning on) the last appliance it went to
            //Good thing here is that the code is wrapped in such a way that when
            //an appliance DOES get turned off the AI would start moving torwards it again
            if (app_state == true)
            {
                pos = GameObject.Find("RestingPlace").transform.position;
                navMeshAgent.SetDestination(pos);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(roundCount);
        //Number of interactables in the collision hitbox sphere
        _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders, _interactableMask);

        //To which object can the AI go based on a dice roll.
        if (GameObject.Find("TeddyBearGrenade(Clone)") != null)
        {
            pos = GameObject.Find("TeddyBearGrenade(Clone)").transform.position;
            navMeshAgent.SetDestination(pos);
        }
        else
        {
            switch (ObjGoTo)
            {
                case 1:
                    {
                        GoToAppliance(canInteractWO.stateWashingMash, "Washing Mashine");
                    }
                    break;

                case 2:
                    {
                        GoToAppliance(canInteractWO.stateDryer, "Dryer");
                    }
                    break;

                /*case 3:
                    {
                        GoToAppliance(canInteractWO.stateBob, "Bob");
                    }
                    break;*/

                case 3:
                    {
                        GoToAppliance(canInteractWO.stateFridge, "Fridge");
                    }
                    break;

                case 4:
                    {
                        GoToAppliance(canInteractWO.stateTV, "TV");
                    }
                    break;

                case 5:
                    {
                        GoToAppliance(canInteractWO.stateRadio, "Radio");
                    }
                    break;

                case 6:
                    {
                        GoToAppliance(canInteractWO.stateLamp, "Lamp");
                    }
                    break;
            }
        }
        
        
        if (DistanceCheck() == true)
        {
            ObjID();
        }

        //If the AI losses the cords to where it needs to go, after 5 seconds try and pass in new cords.
        if (Failsafe == true)
        {
            failsafetimer += Time.deltaTime;

            if (failsafetimer >= failsafeDelay)
            {
                failsafetimer = 0f;
                ObjID();
            }
        }

    }
    //Gizmo sphere that shows the interactable hitbox of the AI.
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
    }

    //Distance check to the passed in cords of the Appliance to the AI.
    //!!! The cord are the origin point of the object. If the object is wide account for that by adding to the distance in Update.
    private bool DistanceCheck()
    {
        if (Vector3.Distance(transform.position, navMeshAgent.destination) <= 0.3f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //Function to roll a dice and decide to which object to go.
   private int ObjID()
    {
        ObjGoTo = Random.Range(1, 7);
        return ObjGoTo;
    }
}
