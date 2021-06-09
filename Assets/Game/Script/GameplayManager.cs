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
            if (SceneManager.GetActiveScene().buildIndex.Equals(0))
            {
                // Set the volume 
                audioMixer.SetFloat("Volume", 0);
            }
            
            // Play Music
            FindObjectOfType<AudioManager>().Play("Music");
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
