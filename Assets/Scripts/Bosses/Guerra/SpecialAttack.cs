using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttack : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private ParticleSystem sistemaDeParticulas;
    [SerializeField] private GameObject[] areas;
    [SerializeField] private float damage;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            // Activar el sistema de partículas
            if (sistemaDeParticulas != null)
            {
                sistemaDeParticulas.Play();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerCollider"))
        {
            AttackController.THIS.TakeDamage(damage);
        }
    }
}
