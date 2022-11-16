using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float destroyTime = 5;
    public int power = 1;
    void Awake()
    {
        StartCoroutine(DestroySelf(destroyTime));   
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StopAllCoroutines();
        Destroy(gameObject);
    }

    IEnumerator  DestroySelf(float time) 
    {
        yield return new WaitForSeconds(time);
    }
}
