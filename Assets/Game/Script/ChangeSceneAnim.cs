using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class ChangeSceneAnim : MonoBehaviour
{
    public float time = 68f;
    public string sceneToLoad = "HubIntro";
    
    public VideoPlayer vid;

    public bool check;
    public GameObject skipText;
    
    void Start()
    {
        StartCoroutine(WaitEndToChangeScene());
    }

    
    void Update()
    {
        if (Input.GetButtonDown("Submit") || Input.GetButtonDown("Jump"))
        {
            if (!check)
            {
                StartCoroutine(CheckWait());
            }
            else
            {
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }

    public IEnumerator WaitEndToChangeScene()
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(sceneToLoad);
    }

    public IEnumerator CheckWait()
    {
        check = true;
        skipText.SetActive(true);
        yield return new WaitForSeconds(5);
        check = false;
        skipText.SetActive(false);
    }
}
