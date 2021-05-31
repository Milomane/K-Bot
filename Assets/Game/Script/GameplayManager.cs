using UnityEngine;
using UnityEngine.Audio;

namespace Game.Script
{
    public class GameplayManager : MonoBehaviour
    {
        public AudioMixer audioMixer;
        
        // Start is called before the first frame update
        void Start()
        {
            // Set the volume 
            audioMixer.SetFloat("Volume", 0);
            // Play Music
            // FindObjectOfType<AudioManager>().Play("Music");
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
