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
      //  StartCoroutine(WaitEndToChangeScene());
    }

    
    void Update()
    {
        Debug.Log(Time.timeScale);
        Debug.Log(Time.deltaTime);
        Debug.Log(time);

        time -= Time.deltaTime;
        
        if (time < 0)
            SceneManager.LoadScene(sceneToLoad);
        
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
        Debug.Log("A");
        yield return new WaitForSeconds(time);
        Debug.Log("b");
       
        
        
       Debug.Log("c");
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
