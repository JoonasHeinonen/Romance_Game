using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonNavigation : MonoBehaviour
{
    int index = 0;
    public int totalButtons = 3;
    public float yOffset = 2f;

    // Actual text objects
    public GameObject startButton;
    public GameObject helpButton;
    public GameObject exitButton;

    // Shadow objects
    public GameObject startShadow;
    public GameObject helpShadow;
    public GameObject exitShadow;
    
    Color activeColor = new Color(150.0f / 255.0f, 225.0f / 255.0f, 255.0f / 255.0f);
    Color defaultColor = new Color(40.0f / 255.0f, 160.0f / 255.0f, 255.0f / 255.0f);

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (index < totalButtons - 1)
            {
                index++;
                changeButtonColor(index);
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (index > 0)
            {
                index--;
                changeButtonColor(index);
            }
        }
        if (Input.GetKeyDown("return"))
        {
            determineButton(index);
        }
    }

    void determineButton(int id)
    {
        switch(id)
        {
            case 0:
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                Debug.Log("Entering the game!");
                break;
            case 1:
                Debug.Log("HELP");
                break;
            case 2:
                Application.Quit();
                Debug.Log("Quitting the game...");
                break;
            default:
                Debug.Log("Default activity");
                break;
        }
    }

    void changeButtonColor(int id)
    {
        if (id == 0)
        {
            startButton.GetComponent<Text>().color = activeColor;
            helpButton.GetComponent<Text>().color = defaultColor;
            exitButton.GetComponent<Text>().color = defaultColor;

            // Shadow
            startShadow.SetActive(true);
            helpShadow.SetActive(false);
            exitShadow.SetActive(false);
        } else if (id == 1)
        {
            startButton.GetComponent<Text>().color = defaultColor;
            helpButton.GetComponent<Text>().color = activeColor;
            exitButton.GetComponent<Text>().color = defaultColor;

            // Shadow
            startShadow.SetActive(false);
            helpShadow.SetActive(true);
            exitShadow.SetActive(false);
        }
        else if (id == 2)
        {
            startButton.GetComponent<Text>().color = defaultColor;
            helpButton.GetComponent<Text>().color = defaultColor;
            exitButton.GetComponent<Text>().color = activeColor;

            // Shadow
            startShadow.SetActive(false);
            helpShadow.SetActive(false);
            exitShadow.SetActive(true);
        }
    }
}
