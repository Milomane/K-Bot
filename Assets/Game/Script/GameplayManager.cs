using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

namespace Game.Script
{
    public class GameplayManager : MonoBehaviour
    {
        public static GameplayManager instance;
        public AudioMixer audioMixer;
        
        public KeyCode jump { get; set;}
        public KeyCode forward { get; set;}
        public KeyCode backward { get; set;}
        public KeyCode left { get; set;}
        public KeyCode right { get; set;}
        public KeyCode sprint { get; set;}
        public KeyCode interact { get; set;}
        public KeyCode selfDestroy { get; set;}
        public KeyCode resetBodies { get; set;}

        void Awake()
        {
            if (instance == null)
            {
                DontDestroyOnLoad(gameObject);
                instance = this;
            }
            else if(instance != null)
            {
                Destroy(gameObject);
            }

            jump = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("jumpKey", "Space"));
            forward = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("forwardKey", "Z"));
            backward = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("backwardKey", "S"));
            left = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("leftKey", "Q"));
            right = (KeyCode) System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rightKey", "D"));

            /*jump = KeyCode.Space;
            forward = KeyCode.Z;
            backward = KeyCode.S;
            left = KeyCode.Q;
            right = KeyCode.D;*/
        }
        
        // Start is called before the first frame update
        void Start()
        {
            Time.timeScale = 1f;
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
