using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorBehavior : MonoBehaviour
{
    [SerializeField] private GameObject pointA;
    [SerializeField] private GameObject pointB;
    [SerializeField] bool active = true;
    private bool atA = true;
    private bool atB = false;
    private Vector2 posA;
    private Vector2 posB;
    [SerializeField] private float eleSpeed = 10.0f;
    private string goTo = "stop";

    // Start is called before the first frame update
    void Start()
    {
        posA = new Vector2(pointA.transform.position.x, pointA.transform.position.y);
        posB = new Vector2(pointB.transform.position.x, pointB.transform.position.y);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(goTo != "stop")
        {
            if(goTo == "A")
            {
                atB = false;
                Vector2 movement = Vector2.MoveTowards(transform.position, posA, eleSpeed);
                transform.position = movement;
                if(Vector2.Distance(movement, posA) < 0.01)
                {
                    goTo = "stop";
                    atA = true;
                    transform.position = posA;
                }
            }
            if(goTo == "B")
            {
                atA = false;
                Vector2 movement = Vector2.MoveTowards(transform.position, posB, eleSpeed);
                transform.position = movement;
                if (Vector2.Distance(movement, posB) < 0.01)
                {
                    goTo = "stop";
                    atB = true;
                    transform.position = posB;
                }
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided!");
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Player jumped on Elevator");
            if (atA)
            {
                goTo = "B";
            }
            if (atB)
            {
                goTo = "A";
            }
        }
    }

    public void RecieveContact(string destination)
    {
        if (atA && destination == "B")
        {
            goTo = "A";
        }
        else if (atB && destination == "A")
        {
            goTo = "B";
        }
    }
}
