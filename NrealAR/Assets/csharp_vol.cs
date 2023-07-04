using UnityEngine;
using UnityEngine.UI;

public class csharp_vol : MonoBehaviour
{
    public GameObject buttonvolup;
    public GameObject buttonvoldown;
    private bool isButtonsShown_vol = false;

    public void ToggleButtons_vol()
    {
        if (!isButtonsShown_vol)
        {
            buttonvolup.SetActive(true);
            buttonvoldown.SetActive(true);
            isButtonsShown_vol = true;
        }
        else
        {
            buttonvolup.SetActive(false);
            buttonvoldown.SetActive(false);
            isButtonsShown_vol = false;
        }
    }
}
