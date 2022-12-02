using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [Range(0,1)]
    public float percentage = 0.5f;
    public Vector2 scaleMinMax = new Vector2(0.25f, 2.5f);
    [SerializeField] Gradient gradient = null;

    private Transform model;
    private Renderer modelRenderer;

    void Start()
    {
        model = transform.Find("Model");
        modelRenderer = model.GetComponent<Renderer>();
    }

    public void UpdateModel()
    {
        if (modelRenderer != null) modelRenderer.material.color = gradient.Evaluate(percentage);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, (scaleMinMax.y - scaleMinMax.x) * percentage + scaleMinMax.x);
    }
}
