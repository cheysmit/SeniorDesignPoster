using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

public class CameraController : MonoBehaviour
{

    //public variables

    /// <summary>
    /// This holds a list of the buttons to be created for the UI
    /// </summary>
    public List<GameObject> buttonList = new List<GameObject>();
   
    /// <summary>
    /// Creates a button that will bring up an image
    /// </summary>
    public GameObject imageButton;

    /// <summary>
    /// Updates the canvas
    /// </summary>
    public Transform canvas;
    /// <summary>
    /// Creates a button outside of the button list that is used as the back button
    /// </summary>
    public Button backButton;
    /// <summary>
    /// Renders the sprite to display in the main menu
    /// </summary>
    public GameObject imageFrame;

    //Private Variables
    /// <summary>
    /// Holds the names of all the files in location
    /// </summary>
    private string[] info;
    /// <summary>
    /// The filenames in the directory "Assest/Images".
    /// </summary>
    public string[] fileNames
    {
        get
        {
            var targetDirectory = "Assets/Image";
            // Process the list of files found in the directory.
            info = Directory.GetFiles(targetDirectory, "*.jpg");
            return info;
        }
    }

    /// <summary>
    /// Holds all the images that will be used for this project
    /// </summary>
    private List<Sprite> images = new List<Sprite>();
    //-----------------------------------------------------------------------


    /*TODO LIST
    //make things like menu, textures, etc
    //project design--change for third party access
    //get button location to be placed
    */

    // Start is called before the first frame update
    void Start()
    {
        var x = fileNames; //Initializes filenames.
        createImageList();

        createButtonList();
        backButton.onClick.AddListener(backButtonPressed);
    }
    
    //--------------Public Methods-------------------------

    /// <summary>
    /// Simulates a specific button that is being pressed as a command prompt
    /// </summary>
    /// <param name="Button"></param>
    public void ClickButton(GameObject Button)
    {
        Button.GetComponent<ButtonScript>().TaskOnClick();
    }


    /// <summary>
    /// Simulates the back button being pressed as a command prompt
    /// </summary>
    public void ClickBackButton()
    {
        backButtonPressed();
    }


    /// <summary>
    /// Turns the back button on after a separate button is pressed
    /// </summary>
    public void backButtonOn()
    {
        backButton.gameObject.SetActive(true);
    }


    /// <summary>
    /// Turns off all buttons in the list after one is pressed 
    /// </summary>
    /// <param name="image"></param>
    public void allButtonsInListOff(Sprite image)
    {
        imageFrame.GetComponent<SpriteRenderer>().sprite = image;

        foreach (GameObject btn in buttonList)
        {
            btn.SetActive(false);
        }
    }

    //---------------Private Methods--------------------------

    /// <summary>
    /// Fixes the Senior Design team name from the files string that is passed into it
    /// </summary>
    /// <param name="s"></param> 
    /// <returns></returns>
    private String getSDTeamNumber(String s)
    {
        return s.Substring(s.Length - 8, 4);
    }

    /// <summary>
    /// Takes a texture2D file and converts it into a sprite to be used to bring up an image from a button press
    /// </summary>
    /// <param name="FilePath"></param>
    /// <returns></returns>
    private Texture2D LoadTexture(string FilePath)
    {
        // Load a PNG or JPG file from disk to a Texture2D
        // Returns null if load fails

        Texture2D Tex2D;
        byte[] FileData;

        if (File.Exists(FilePath))
        {
            FileData = File.ReadAllBytes(FilePath);
            Tex2D = new Texture2D(2, 2);           // Create new "empty" texture
            if (Tex2D.LoadImage(FileData))           // Load the imagedata into the texture (size is set automatically)
                return Tex2D;                 // If data = readable -> return texture
        }
        return null;                     // Return null if load failed
    }

    /// <summary>
    /// Creates a list of sprites located in the path defined
    /// </summary>
    private void createImageList()
    {
        foreach (String f in fileNames)
        {
            Texture2D tex = LoadTexture(f);
            Sprite NewSprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f, 0, SpriteMeshType.Tight);
            images.Add(NewSprite); 
        }
    }

    /// <summary>
    /// Creates a list of buttons as GameObjects to be used in the main menu
    /// </summary>
    private void createButtonList()
    {
        int i = 0;
        //Starts the main menu image DO NOT TOUCH
        imageFrame.GetComponent<SpriteRenderer>().sprite = null;

        foreach (Sprite im in images)
        {
            GameObject CatButton = GameObject.Instantiate(imageButton, canvas);
            CatButton.name = getSDTeamNumber(info[i]);
            CatButton.GetComponentInChildren<Text>().text = getSDTeamNumber(info[i]);
            CatButton.GetComponent<ButtonScript>().parentCC = this;
            CatButton.GetComponent<ButtonScript>().image = im;

            //would like to make it in a table format without breaking this
            CatButton.transform.position = new Vector2(+50, i * 50 + 50);


            buttonList.Add(CatButton);
            i++;
        }
    }

    /// <summary>
    /// Checks if backButton is pressed and returns all other button states to on
    /// </summary>
    private void backButtonPressed()
    {
        imageFrame.GetComponent<SpriteRenderer>().sprite = null;

        //turns all buttons back on and back button off
        foreach (GameObject btn in buttonList)
        {
            btn.SetActive(true);
        }
        //turns back back on
        backButton.gameObject.SetActive(false);
    }
}