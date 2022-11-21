using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float destroyTime = 5f;
    public int power = 1;
    public bool destroyOnImpact = true;
    void Awake()
    {
        StartCoroutine(DestroySelf(destroyTime));   
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (destroyOnImpact == true) 
        {
            StopAllCoroutines();
            Destroy(gameObject);
        }
        
    }

    IEnumerator  DestroySelf(float time) 
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
