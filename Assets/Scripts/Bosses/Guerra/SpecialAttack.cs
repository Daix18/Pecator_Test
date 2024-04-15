using System.Collections;
using System.Collections.Generic;
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

    // Update is called once per frame
    void Update()
    {

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
        yield return new WaitForSeconds(duration);
        collider2D.enabled = false;
        sistemaDeParticulas.Stop();
    }
}
