using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Movement : MonoBehaviour
{
    protected Game_Controller _gameController;

    [SerializeField] protected float _localSpeed;     // velocidad local del objeto

    private void Start()
    {
        _gameController = Game_Controller.Instance;
    }

    private void Update()
    {
        Movement();
    }

    public virtual void Movement()
    {
        //      el objeto se trasnlada hacia la izquierda. Su velocdad es el producto de su velocidad local por la velocidad global del juego
        transform.Translate(-Vector3.right * _localSpeed * _gameController.GlobalSpeed * Time.deltaTime);
    }
}
