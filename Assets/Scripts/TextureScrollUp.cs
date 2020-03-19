using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureScrollUp : MonoBehaviour
{
    Material bgMaterial;
    public float scrollSpeed = 10f;
    public bool scroll = true;
    float scrollOffsetY = 0;
    [SerializeField] float scrollOffsetYInit = 0;

    private void Awake()
    {
        bgMaterial = GetComponent<Renderer>().material;
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (scroll)
        {
            scrollOffsetY = ((scrollSpeed / 1000) * Time.time) + scrollOffsetYInit;
            Vector2 offset = new Vector2(0, scrollOffsetY);
            bgMaterial.mainTextureOffset = offset;
        }
    }
}