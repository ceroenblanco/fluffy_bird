using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead_Trigger : MonoBehaviour
{
    Game_Controller _gameController;

    private void Start()
    {
        _gameController = Game_Controller.Instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.transform.CompareTag("Player") || _gameController.IsGameOver) return;

        if (collision.transform.GetComponent<Player_Controller>().IsInvincible) return;

        _gameController.GameOver();
    }
}
