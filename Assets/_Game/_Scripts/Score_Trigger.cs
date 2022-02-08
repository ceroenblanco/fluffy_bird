using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score_Trigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.transform.CompareTag("Player")) return;

        Game_Controller.Instance.AddScore();
    }
}
