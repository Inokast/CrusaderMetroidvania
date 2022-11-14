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

    // Start is called before the first frame update
    void Start()
    {
        posA = new Vector2(pointA.transform.position.x, pointA.transform.position.y);
        posB = new Vector2(pointB.transform.position.x, pointB.transform.position.y);

        transform.position = posA;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(atA)
        }
    }
}
