using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Assignment/Lab/Project: Metroidvania
//Name: Talyn Epting

public class DialogueCon : MonoBehaviour
{
    [SerializeField] float waitingTime;
    [SerializeField] string msg;


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            GameManager.gm.dialogueDisplay.SetActive(true);
            GameManager.gm.alert[0].text = msg;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            StartCoroutine(ClearDialogue(waitingTime));
        }
        StopCoroutine(ClearDialogue(waitingTime));
    }

    IEnumerator ClearDialogue(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        GameManager.gm.dialogueDisplay.SetActive(false);
        GameManager.gm.alert[0].text = "";
    }
}
