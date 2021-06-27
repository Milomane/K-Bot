using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class ChangeSceneAnim : MonoBehaviour
{
    public VideoPlayer vid;
    
    void Start()
    {
        StartCoroutine(WaitEndToChangeScene());
    }

    
    void Update()
    {
    }

    public IEnumerator WaitEndToChangeScene()
    {
        yield return new WaitForSeconds(68f);
        SceneManager.LoadScene("HubIntro");
    }
}
