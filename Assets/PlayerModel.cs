using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    [Header("Main")] 
    public GameObject model;
    
    [Header("In the back")] 
    public GameObject normalBack;
    public GameObject generatorBack;
    public GameObject springBack;
    public GameObject acceleratorBack;
    public GameObject explosionBack;
    public GameObject lampBack;

    public GameObject[] arrayBackModels = new GameObject[6];
    
    void Start()
    {
        for (int i = 0; i < 6; i++)
        {
            switch (i)
            {
                case 0:
                    arrayBackModels[i] = normalBack;
                    break;
                case 1:
                    arrayBackModels[i] = explosionBack;
                    break;
                case 2:
                    arrayBackModels[i] = springBack;
                    break;
                case 3:
                    arrayBackModels[i] = generatorBack;
                    break;
                case 4:
                    arrayBackModels[i] = lampBack;
                    break;
                case 5:
                    arrayBackModels[i] = acceleratorBack;
                    break;
            }
        }
    }
    
    void Update()
    {
        
    }
}
