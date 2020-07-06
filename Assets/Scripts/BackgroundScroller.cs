using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour {
    [Tooltip("Rate at which we want to move our scrolling background")]
    [SerializeField] float scrollSpeed = 1f;

    Material backgroundImageMaterial;
    Vector2 scrollingOffset;

    void Start() {
        backgroundImageMaterial = GetComponent<Renderer>().material;
        scrollingOffset = new Vector2(scrollSpeed, 0);
    }

    void Update() {
        backgroundImageMaterial.mainTextureOffset += scrollingOffset * Time.deltaTime;
    }
}
