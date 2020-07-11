using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerPlayInstructions : MonoBehaviour {

    [SerializeField] GameObject playInstructionsObject;

    [SerializeField] Canvas canvasElement;

    void Start() {
        
    }

    void Update() {
        
    }

    public void OpenPlayInstructions() {
        if (FindObjectsOfType<HowToPlay>().Length < 1) {
            GameObject playInstructions = Instantiate(playInstructionsObject);
            playInstructions.transform.SetParent(canvasElement.transform);
            RectTransform rectTransform = playInstructions.GetComponent<RectTransform>();
            rectTransform.localPosition = new Vector2(0, 0);
        }
    }
}
