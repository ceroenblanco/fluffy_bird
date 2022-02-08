using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    Rigidbody2D _rigidbody2D;
    [Space]
    [SerializeField] float _jumpForce;      //  velocidad vertical del jugador
    [SerializeField] float _maxPositionY, _minPositionY;     //  limite maximo y minimo de la posicion vertical del jugador
    [Space]
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] Animator _animator;
    [SerializeField] AudioSource _audioSource;
    [Space]
    [SerializeField] bool _invincible;      //  bandera invensibilidad
    [SerializeField] float _invincibleTime;     //  tiempo de invensibilidad
    float _invincibleTimer;     //  temporizador invensibilidad
    public bool IsInvincible => _invincible;

    private void Awake()
    {
        _rigidbody2D = transform.GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Jump();
    }

    private void Update()
    {
        //      cuando el jugador presione el boton izquierdo del raton se actualiza la velocidad vertical del objeto
        if (Input.GetMouseButtonDown(0))
        {
            Jump();

            _audioSource.Play();
        }

        //      Mathf.Clamp para que el jugador no salga de los limites de la zona de juego
        transform.position = new Vector2(transform.position.x, Mathf.Clamp(transform.position.y, _minPositionY, _maxPositionY));
    }

    private void Jump()
    {
        //      se acutliza la velocidad vertical del objeto y se ultiliza Mathf.Abs para que el valor no sea negativo
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, Mathf.Abs(_jumpForce));

        //      reiniciar animacion
        _animator.Rebind();
    }

    public void Invencible ()
    {
        //          activar rutina invencible
        StartCoroutine(Rutine_Invincible());
    }

    private void OnDisable()
    {
        //      para desactivar la gravedad cuando el objeto se desactive
        _rigidbody2D.simulated = false;

        //      para desactivar la animacion cuando el objeto se desactive
        _animator.enabled = false;
    }

    IEnumerator Rutine_Invincible ()
    {
        _invincible = true;
        _invincibleTimer = 0;

        StartCoroutine(Rutine_FadePlayerSprite());

        while (_invincibleTimer < _invincibleTime)
        {
            _invincibleTimer += Time.deltaTime;

            yield return null;
        }

        _invincible = false;

        yield break;
    }

    IEnumerator Rutine_FadePlayerSprite ()
    {
        Color newColor = _spriteRenderer.color;
        newColor.a = 0;
        _spriteRenderer.color = newColor;

        while (_invincible)
        {
            newColor = _spriteRenderer.color;
            newColor.a = Mathf.MoveTowards(newColor.a, 1, 10 * Time.unscaledDeltaTime);
            _spriteRenderer.color = newColor;

            if (_spriteRenderer.color.a == 1)
            {
                newColor.a = 0;
                _spriteRenderer.color = newColor;
            }

            yield return null;
        }

        newColor.a = 1;
        _spriteRenderer.color = newColor;

        yield break;
    }
}
