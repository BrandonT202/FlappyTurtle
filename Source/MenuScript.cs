using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuScript : MonoBehaviour
{
    public SpriteRenderer SpriteOn;
    public SpriteRenderer SpriteOff;

    //levels
    int MAINMENU = 0;
    int MAINLEVEL = 1;
    int OPTIONS = 2;

    public bool isQuitButton = false;
    bool isOn = true;

    GUIText GUITEXT = null;

    void Start()
    {
        if (transform.name == "Options")
        {
            GUITEXT = transform.FindChild("GUI Text").GetComponent<GUIText>();
            GUITEXT.enabled = false;
        }
    }

    void OnMouseEnter()
    {
        SpriteOn.enabled = isOn;
        SpriteOff.enabled = !isOn;
    }

    void OnMouseExit()
    {
        SpriteOn.enabled = !isOn;
        SpriteOff.enabled = isOn;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Quit");
            Application.Quit();
        }
    }

    void OnMouseUp()
    {
        switch (transform.name)
        {
            case "PlayGame":
                SceneManager.LoadScene(MAINLEVEL);
                break;
            case "Options":
                SceneManager.LoadScene(OPTIONS);
                break;
            case "QuitGame":
                Debug.Log("Quit");
                Application.Quit();
                break;
            case "Back":
                SceneManager.LoadScene(MAINMENU);
                break;
            default:
                Debug.Log("Invalid choice");
                break;
        }
    }

}
