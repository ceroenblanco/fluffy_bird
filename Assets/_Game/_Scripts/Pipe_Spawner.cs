using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe_Spawner : MonoBehaviour
{
    Game_Controller _gameController;

    [SerializeField] GameObject _prefabPipe;        //  prefab del pipe
    Queue<GameObject> _objectsInQueue = new Queue<GameObject>();        //  lista disponibles para instanciar

    [SerializeField] float _spawnFrequency;     //  frecuencia de aparicion de objetos
    [SerializeField] float _maxHigh, _minHigh;      //  altura maxima y minima de paricion de los objetos
    [SerializeField] bool _spawnOnStart;

    float _spawnTimer = 0;        //  temporizador de aparicion objeto

    private void Start()
    {
        _gameController = Game_Controller.Instance;

        //      poblar lista de objetos
        for (int i = 0; i < 10; i++)
        {
            GameObject obj = (GameObject)Instantiate(_prefabPipe, transform.position, Quaternion.identity, transform);

            _objectsInQueue.Enqueue(obj);
            obj.SetActive(false);
        }

        if (_spawnOnStart)
            _spawnTimer = (1 / _spawnFrequency) / _gameController.GlobalSpeed;

        StartCoroutine(SpawnObjects());     //  inicializar rutina de aparicion de objetos
    }

    IEnumerator SpawnObjects ()
    {
        while (!_gameController.IsGameOver)     //  si el juego ha terminado sale del ciclo
        {
            _spawnTimer += Time.deltaTime;        //  incrementar temporizador

            if (_spawnTimer >= (1 / _spawnFrequency) / _gameController.GlobalSpeed)       //  si el temporizador es mayor a la frecuencia por segundos entre la velocidad global del juego
            {
                _spawnTimer = 0;      //  se reinicia el temporizador

                GameObject new_go;

                if (_objectsInQueue.Count > 0)      //  cuando hay obejtos disponibles en la lista
                {
                    new_go = _objectsInQueue.Dequeue();

                    new_go.transform.position = transform.position;
                    new_go.SetActive(true);
                }
                else        //  cuando no hay objetos disponibles en la lista
                {
                    new_go = (GameObject)Instantiate(_prefabPipe, transform.position, Quaternion.identity, transform);
                }

                //      altura aleatoria de objeto
                Vector2 newPosition = new_go.transform.position;
                newPosition.y = Random.Range(_minHigh, _maxHigh);

                new_go.transform.position = newPosition;
            }

            yield return null;      //  espera hasta el siguiente frame antes de continuar
        }

        yield break;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //      cuando un obejto con la etiqueta Pipe colisiona con este collider, el objeto se desactiva y se agrega a la lista de objetos disponibles
        if (!collision.transform.CompareTag("Pipe")) return;

        if (_objectsInQueue.Contains(collision.transform.parent.gameObject)) return;       //  si el objeto ya se encuentra en la pisina de obejto no se agrega

        _objectsInQueue.Enqueue(collision.transform.parent.gameObject);        //  agregar objeto a la lista de objetos disponibles

        collision.transform.parent.gameObject.SetActive(false);        //  desactivar objeto
    }
}
