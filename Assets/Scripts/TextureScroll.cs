using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureScroll : MonoBehaviour
{
    Material bgMaterial;
    public float scrollSpeed = 10f;
    public bool scroll = true;

    private void Awake()
    {
        bgMaterial = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(scroll)
        {
            float scrollOffsetX = scrollSpeed * (GameManager.instance.gameSpeed / 100000) * Time.time;
            Vector2 offset = new Vector2(scrollOffsetX, 0);
            bgMaterial.mainTextureOffset = offset;
        }
    }
}