using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground_Movement : Object_Movement
{
    [SerializeField] protected float _loopDistance;       // distancia maxima para que el objeto vuelva a la posicion inicial

    protected Vector2 _initialPosition;      // posicion inicial del objeto

    private void Start()
    {
        _gameController = Game_Controller.Instance;
        _initialPosition = transform.position;
    }

    public override void Movement()
    {
        base.Movement();

        //      si el objeto sobrepasa la distancia maxima volvera a su posicion inicial
        if (Vector2.Distance(transform.position, _initialPosition) >= _loopDistance)
        {
            transform.position = _initialPosition;
        }
    }
}
