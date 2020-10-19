using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public Button button;
    public Sprite image;
    public CameraController parentCC;

    // Start is called before the first frame update
    void Start()
    {
        Button b = button.GetComponent<Button>();
        b.onClick.AddListener(TaskOnClick);
    }

    public void TaskOnClick()
    {
        //image button pressed
         
        parentCC.backButtonOn();
        parentCC.allButtonsInListOff(image);
    }
}
