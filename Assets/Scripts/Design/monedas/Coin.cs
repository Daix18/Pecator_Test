using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    public int valor = 1;
    public GameController gameController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerCollider"))
        {
            gameController.SumarPuntos(valor);
            Destroy(this.gameObject);
        }

    }

    private void Awake()
    {
        GameObject.Find("MonedaP_0").GetComponent<Coin>().gameController = gameController;
    }
}