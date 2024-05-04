using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[ExecuteInEditMode]
public class ParallaxBackground : MonoBehaviour
{
    public ParallaxCamera parallaxCamera;
    List<ParallaxLayer> parallaxLayers = new List<ParallaxLayer>();

    void Start()
    {
        if (parallaxCamera == null)
            parallaxCamera = Camera.main.GetComponent<ParallaxCamera>();

        if (parallaxCamera != null)
            parallaxCamera.onCameraTranslate += Move;

        FindLayers();

        SetLayers();
    }

    void FindLayers()
    {
        parallaxLayers.Clear();

        // Busca todos los objetos en la escena con el componente ParallaxLayer
        ParallaxLayer[] layersInScene = FindObjectsOfType<ParallaxLayer>();

        // Agrega las capas encontradas a la lista
        foreach (ParallaxLayer layer in layersInScene)
        {
            parallaxLayers.Add(layer);
        }
    }


    void SetLayers()
    {
        parallaxLayers.Clear();

        for (int i = 0; i < transform.childCount; i++)
        {
            ParallaxLayer layer = transform.GetChild(i).GetComponent<ParallaxLayer>();

            if (layer != null)
            {
                layer.name = "Layer-" + i;
                parallaxLayers.Add(layer);
            }
        }
    }

    void Move(float delta)
    {
        foreach (ParallaxLayer layer in parallaxLayers)
        {
            layer.Move(delta);
            Debug.Log(layer.name);
        }
    }
}