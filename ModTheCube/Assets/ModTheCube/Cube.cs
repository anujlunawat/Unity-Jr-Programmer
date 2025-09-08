using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public MeshRenderer Renderer;
    private Color targetColor;

    void Start()
    {
        float posX = Random.Range(0, 10);
        float posY = Random.Range(0, 10);
        float posZ = Random.Range(0, 10);
        transform.position = new Vector3(posX, posY, posZ);
        transform.localScale = Vector3.one * 1.3f;


        //Material material = Renderer.material;
        //material.color = new Color(0.5f, 1.0f, 0.3f, 1f);
    }
    
    void Update()
    {
        Renderer.material.color = Color.Lerp(Renderer.material.color, targetColor, Time.deltaTime);

        if(Vector4.Distance(Renderer.material.color, targetColor) < 0.05f)
        {
            SetNewTargetColor();
        }

        transform.Rotate(10.0f * Time.deltaTime, 0.0f, 0.0f);
    }

    void SetNewTargetColor()
    {
        targetColor = new Color(Random.value, Random.value, Random.value);
    }
}
