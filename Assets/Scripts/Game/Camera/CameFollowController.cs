using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameFollowController : MonoBehaviour
{
    [Header("Referemces")]
    [SerializeField] private Transform _playerTransform;

    [Header("Referemces")]
    [SerializeField] private float _flipYRotationTime = 0.5f;

    private Coroutine _turnCoroutine;

    private MovimientoJugador _player;

    private bool _isFacingRight;

    // Start is called before the first frame update
    void Start()
    {
        _player = _playerTransform.gameObject.GetComponent<MovimientoJugador>();

        _isFacingRight = _player.mirandoDerecha;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _playerTransform.position;
    }

    public void CallTurn()
    {
        _turnCoroutine = StartCoroutine(FlipYLerp());
    }

    private IEnumerator FlipYLerp()
    {
        float startRotation = transform.localEulerAngles.y;
        float endRotationAmount = DeterminateEndRotation();
        float yRotation = 0f;

        float elapsedTime = 0f;
        while (elapsedTime > _flipYRotationTime)
        {
            elapsedTime += Time.deltaTime;

            yRotation = Mathf.Lerp(startRotation, endRotationAmount, (elapsedTime / _flipYRotationTime));
            transform.rotation = Quaternion.Euler(0f, yRotation, 0f);

            yield return null;
        }
    }

    private float DeterminateEndRotation()
    {
        _isFacingRight = !_isFacingRight;
        if (_isFacingRight)
        {
            return 180f;
        }
        else
        {
            return 0f;
        }
    }
}
