using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game_Controller : MonoBehaviour
{
    public static Game_Controller Instance;

    [SerializeField] Player_Controller _player;     //  jugador en la escena

    [SerializeField] Button _btnRestart;         // boton reiniciar
    [SerializeField] Text _txtScore;        //  componente de texto de la puntuacion
    [SerializeField] int _score;        //  puntuacion del jugador

    [SerializeField] float _globalSpeed;        //  velocidad global del juego
    public float GlobalSpeed => _globalSpeed;
    [SerializeField] bool _gameOver;
    public bool IsGameOver => _gameOver;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _gameOver = false;

        _score = 0;
        UpdateScoreText();

        _btnRestart.gameObject.SetActive(false);
    }

    void UpdateScoreText()
    {
        _txtScore.text = _score.ToString();
    }

    public void GameOver ()
    {
        _gameOver = true;

        _player.enabled = false;        //  desactivar el controlador del jugador

        _globalSpeed = 0;       //  detener la velocidad global del juego

        _btnRestart.gameObject.SetActive(true);         //  activar el boton reiniciar

        AudioManager.Instance.Play("GameOver");     //  sonido game over
    }

    public void Btn_Reiniciar ()
    {
        AudioManager.Instance.Play("Button");       //  sonido boton

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void AddScore()
    {
        _score++;       //  aumentar puntuacion

        UpdateScoreText();      //  actualizar interfaz

        AudioManager.Instance.Play("Score");        //  sonido puntuacion
    }
}
