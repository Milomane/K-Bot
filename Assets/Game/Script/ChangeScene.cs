using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public Animator transition;
    
    public string sceneName;
    
    void Start()
    {
        transition.gameObject.SetActive(true);
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        transition.SetTrigger("end");
        yield return  new WaitForSeconds(0.9f);
        SceneManager.LoadScene(sceneName);
    }
}
