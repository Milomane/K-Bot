using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImagePressActiv : MonoBehaviour
{
    public GameObject imagePressCanvas;
    // Start is called before the first frame update
    void Start()
    {
        imagePressCanvas.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            imagePressCanvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            imagePressCanvas.SetActive(false);
        }
    }
}
