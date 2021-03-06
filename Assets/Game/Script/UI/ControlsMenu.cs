using System.Collections;
using System.Collections.Generic;
using Game.Script;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControlsMenu : MonoBehaviour
{
    private Transform menuPanel;
    private Event keyEvent;
    private TMP_Text buttonText;
    private KeyCode newKey;
    
    // Panel "OptionsMenu"
    [SerializeField] private GameObject optionsMenuUI;
    
    // Panel "PauseMenu"
    [SerializeField] private GameObject pauseMenuUI;

	bool waitingForKey;

	void Start ()
	{
		PauseMenu.isMenuPauseClosed = false;
		//Assign menuPanel to the Panel object in our Canvas
		//Make sure it's not active when the game starts
		menuPanel = transform.Find("Controls");

		//menuPanel.gameObject.SetActive(false);
		waitingForKey = false;

		/*iterate through each child of the panel and check
		 * the names of each one. Each if statement will
		 * set each button's text component to display
		 * the name of the key that is associated
		 * with each command. Example: the ForwardKey
		 * button will display "W" in the middle of it
		 */
		for(int i = 0; i < menuPanel.childCount; i++)
		{
			if(menuPanel.GetChild(i).name == "ForwardKey")
				menuPanel.GetChild(i).GetComponentInChildren<TMP_Text>().text = GameplayManager.instance.forward.ToString();
			else if(menuPanel.GetChild(i).name == "BackwardKey")
				menuPanel.GetChild(i).GetComponentInChildren<TMP_Text>().text = GameplayManager.instance.backward.ToString();
			else if(menuPanel.GetChild(i).name == "LeftKey")
				menuPanel.GetChild(i).GetComponentInChildren<TMP_Text>().text = GameplayManager.instance.left.ToString();
			else if(menuPanel.GetChild(i).name == "RightKey")
				menuPanel.GetChild(i).GetComponentInChildren<TMP_Text>().text = GameplayManager.instance.right.ToString();
			else if(menuPanel.GetChild(i).name == "JumpKey")
				menuPanel.GetChild(i).GetComponentInChildren<TMP_Text>().text =GameplayManager.instance.jump.ToString();
		}
	}

	void Update ()
	{
		//Escape key will open or close the panel
		/*if(Input.GetKeyDown(KeyCode.Escape) && !menuPanel.gameObject.activeSelf)
			menuPanel.gameObject.SetActive(true);
		else if(Input.GetKeyDown(KeyCode.Escape) && menuPanel.gameObject.activeSelf)
			menuPanel.gameObject.SetActive(false);*/
		
		if (pauseMenuUI != null)
		{
			if (Input.GetKeyDown(KeyCode.Escape) && gameObject.activeSelf)
			{
				pauseMenuUI.SetActive(true);
				gameObject.SetActive(false);
			}
		}
	}

	void OnGUI()
	{
		/*keyEvent dictates what key our user presses
		 * bt using Event.current to detect the current
		 * event
		 */
		keyEvent = Event.current;

		//Executes if a button gets pressed and
		//the user presses a key
		if(keyEvent.isKey && waitingForKey)
		{
			newKey = keyEvent.keyCode; //Assigns newKey to the key user presses
			
			waitingForKey = false;
		}
	}

	/*Buttons cannot call on Coroutines via OnClick().
	 * Instead, we have it call StartAssignment, which will
	 * call a coroutine in this script instead, only if we
	 * are not already waiting for a key to be pressed.
	 */
	public void StartAssignment(string keyName)
	{
		if(!waitingForKey)
			StartCoroutine(AssignKey(keyName));
	}

	//Assigns buttonText to the text component of
	//the button that was pressed
	public void SendText(TMP_Text text)
	{
		buttonText = text;
	}

	//Used for controlling the flow of our below Coroutine
	IEnumerator WaitForKey()
	{
		while(!keyEvent.isKey)
			yield return null;
	}

	/*AssignKey takes a keyName as a parameter. The
	 * keyName is checked in a switch statement. Each
	 * case assigns the command that keyName represents
	 * to the new key that the user presses, which is grabbed
	 * in the OnGUI() function, above.
	 */
	public IEnumerator AssignKey(string keyName)
	{
		waitingForKey = true;

		yield return WaitForKey(); //Executes endlessly until user presses a key
		Debug.Log("AssignKey : keyName = " + keyName);
		switch(keyName)
		{
		case "forward":
			GameplayManager.instance.forward = newKey; //Set forward to new keycode
			buttonText.text = GameplayManager.instance.forward.ToString(); //Set button text to new key
			PlayerPrefs.SetString("forwardKey", GameplayManager.instance.forward.ToString()); //save new key to PlayerPrefs
			break;
		case "backward":
			GameplayManager.instance.backward = newKey; //set backward to new keycode
			buttonText.text = GameplayManager.instance.backward.ToString(); //set button text to new key
			PlayerPrefs.SetString("backwardKey", GameplayManager.instance.backward.ToString()); //save new key to PlayerPrefs
			break;
		case "left":
			GameplayManager.instance.left = newKey; //set left to new keycode
			buttonText.text = GameplayManager.instance.left.ToString(); //set button text to new key
			PlayerPrefs.SetString("leftKey", GameplayManager.instance.left.ToString()); //save new key to playerprefs
			break;
		case "right":
			GameplayManager.instance.right = newKey; //set right to new keycode
			buttonText.text = GameplayManager.instance.right.ToString(); //set button text to new key
			PlayerPrefs.SetString("rightKey", GameplayManager.instance.right.ToString()); //save new key to playerprefs
			break;
		case "jump":
			GameplayManager.instance.jump = newKey; //set jump to new keycode
			buttonText.text = GameplayManager.instance.jump.ToString(); //set button text to new key
			PlayerPrefs.SetString("jumpKey", GameplayManager.instance.jump.ToString()); //save new key to playerprefs
			break;
		}

		yield return null;
	}
}
