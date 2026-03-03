using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class F_PauseMenu : MonoBehaviour
{
    bool isPaused = false; //checks whether the game is currently paused or not
    GameObject playerObject; //a reference to the player object to gain access to its components
    [SerializeField] GameObject pauseMenuObject; // a reference to the pausemenuobject to turn it on and fof
    [SerializeField] GameObject pauseStartMenuObject;// a reference to our "startMenu" object to reset the pause menu everytime it is exited
    [SerializeField] GameObject menuList;
    private Animator animator; // a reference to the animator of the pause menu to reset it when the menu is closed
    private bool isActive = false; // a variable to check whether the pause menu is active or not, this is used to reset the animator when the menu is closed
    private float menuOffset;

    private void Start()
    {
        transform.localScale = Vector2.zero; // set the scale of the pause menu to 0 so that it is invisible
        playerObject = GameObject.FindGameObjectWithTag("Player");
        animator = pauseMenuObject.GetComponent<Animator>();
        ResetPauseUI();
    }

    void OnPauseGame(InputValue inputValue)//is called when our escape key is pressed
    {
        Debug.Log("OnPauseGameCalled");
        animator.SetTrigger("newAnimation");
        if (!isPaused) { PauseGame(); }
        else { UnPauseGame(); }
    }


    public void PauseGame()//handles pausing the game and activating the pause menu
    {
        transform.localScale = Vector2.one; // set the scale of the pause menu to 1 so that it is visible
        isPaused = true;
        Time.timeScale = 0;
        playerObject.GetComponent<PlayerInput>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pauseMenuObject.SetActive(true);
        
    }
    public void UnPauseGame()// handles unpausing and unactivating the pause menu
    {
        isPaused= false;
        Time.timeScale = 1;
        playerObject.GetComponent<PlayerInput>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        pauseMenuObject.SetActive(false);
        ResetPauseUI();
        
    }
    void ResetPauseUI()//handles reseting the pause menu so that everytime it is open it opens on the start menu
    {
        for(int i = 0; i < menuList.transform.childCount; i++)
        {
            menuList.transform.GetChild(i).gameObject.SetActive(false);
        }
        pauseStartMenuObject.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();//this is how a game is quit in a build form but this doesnt work in the editor
        UnityEditor.EditorApplication.isPlaying = false;// this is how to stop the editor form playing, we will use this for testing purposes
        //line 55 needs to be removed if you ever decide to build the project 
    }
}
