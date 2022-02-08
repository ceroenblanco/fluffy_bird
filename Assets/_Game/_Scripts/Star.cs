using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : Ground_Movement
{
    [SerializeField] float _maxHigh, _minHigh;

    private void Start()
    {
        _gameController = Game_Controller.Instance;
        _initialPosition = transform.position;

        StartPosition();     //  altura aleatoria
    }

    void StartPosition ()
    {
        transform.position = new Vector2(_initialPosition.x, Random.Range(_minHigh, _maxHigh));
    }

    public override void Movement()
    {
        base.Movement();

        //      si el objeto sobrepasa la distancia maxima volvera a su posicion inicial y seleccionar una nueva altura aleatoria
        if (Vector2.Distance(transform.position, _initialPosition) >= _loopDistance)
        {
            StartPosition();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.transform.CompareTag("Player")) return;

        StartPosition();

        collision.transform.GetComponent<Player_Controller>().Invencible();

        AudioManager.Instance.Play("StarPickUp");   
    }
}
