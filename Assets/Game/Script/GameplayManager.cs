using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

namespace Game.Script
{
    public class GameplayManager : MonoBehaviour
    {
        public AudioMixer audioMixer;
        
        // Start is called before the first frame update
        void Start()
        {
            Time.timeScale = 1f;
            
            // If we are in main menu
            if (SceneManager.GetActiveScene().name.Equals("MainMenu"))
            {
                // Set the volume 
                audioMixer.SetFloat("Volume", 0);
                // Play Music
                FindObjectOfType<AudioManager>().Play("MusicMainMenu");
            }
            else
            {
                // Stop Music
                FindObjectOfType<AudioManager>().Stop("MusicMainMenu");
            }
            
            
            // If we are in Factory
            if (SceneManager.GetActiveScene().name.Equals("Factory"))
            {
                // Set the volume 
                audioMixer.SetFloat("Volume", 0);
                // Play Music
                FindObjectOfType<AudioManager>().Play("MusicFactory");
            }
            else
            {
                // Stop Music
                FindObjectOfType<AudioManager>().Stop("MusicFactory");
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
