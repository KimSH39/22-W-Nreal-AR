using UnityEngine;
using UnityEngine.UI;

public class csharp : MonoBehaviour
{
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;
    private bool isButtonsShown = false;

    public void ToggleButtons()
    {
        if (!isButtonsShown)
        {
            button1.SetActive(true);
            button2.SetActive(true);
            button3.SetActive(true);
            button4.SetActive(true);
            isButtonsShown = true;
        }
        else
        {
            button1.SetActive(false);
            button2.SetActive(false);
            button3.SetActive(false);
            button4.SetActive(false);
            isButtonsShown = false;
        }
    }
}
