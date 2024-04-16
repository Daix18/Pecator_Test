using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class SpecialAttack : MonoBehaviour
{
    public static SpecialAttack THIS;
    [Header("Settings")]
    [SerializeField] private GameObject area;
    [SerializeField] private Transform fatherArrowRain;
    [SerializeField] private float damage;
    [SerializeField] private float duration;
    private ParticleSystem sistemaDeParticulas;
    private new BoxCollider2D collider2D;

    // Limites para el movimiento en el eje X
    [SerializeField] private float minXLimit = -5f;
    [SerializeField] private float maxXLimit = 5f;

    // Start is called before the first frame update
    void Start()
    {       
        collider2D = GetComponent<BoxCollider2D>();
        sistemaDeParticulas = GetComponent<ParticleSystem>();
        collider2D.enabled = false;
        sistemaDeParticulas.Stop();
    }

    private void Awake()
    {
        if (THIS == null)
        {
            THIS = this;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerCollider"))
        {
            AttackController.THIS.TakeDamage(damage);
        }
    }

    public IEnumerator ArrowRain()
    {
        collider2D.enabled = true;
        sistemaDeParticulas.Play();
        Vector2 originalFatherPosition = fatherArrowRain.transform.position;
        float randomXPosition = Random.Range(minXLimit, maxXLimit);
        //Vector2 newPosition = (randomXPosition,  originalFatherPosition.y);
        yield return new WaitForSeconds(duration);
        collider2D.enabled = false;
        sistemaDeParticulas.Stop();
        StartCoroutine(GuerraBossController.THIS.Stun());
    }
}
