using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class SpecialAttack : MonoBehaviour
{
    public static SpecialAttack THIS;
    [Header("Settings")]
    [SerializeField] private GameObject area;
    [SerializeField] private float damage;
    [SerializeField] private float duration;
    private ParticleSystem sistemaDeParticulas;
    private new BoxCollider2D collider2D;

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
        Vector2 originalFatherPosition = GuerraBossController.THIS.fatherArrowRain.transform.position;
        float randomXPosition = Random.Range(GuerraBossController.THIS.minXLimit, GuerraBossController.THIS.maxXLimit);
        Vector2 newPosition = new Vector2 (randomXPosition, originalFatherPosition.y);
        GuerraBossController.THIS.fatherArrowRain.transform.position = newPosition;
        yield return new WaitForSeconds(duration);
        collider2D.enabled = false;
        sistemaDeParticulas.Stop();
        StartCoroutine(GuerraBossController.THIS.Stun());
    }
}