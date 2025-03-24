using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    public Image cactusImage;
    public Sprite[] cactusSprites;
    public Text dialogueText;
    public string[] dialogues;

    public GameObject panelTutorial;
    public GameObject panelJuego;

    private int currentStep = 0;

    void Start()
    {
        ShowStep(0);
    }

    public void NextStep()
    {
        currentStep++;

        if (currentStep < dialogues.Length)
        {
            ShowStep(currentStep);
        }
        else
        {
            StartGame();
        }
    }

    void ShowStep(int step)
    {
        dialogueText.text = dialogues[step];
        cactusImage.sprite = cactusSprites[step];
    }

    void StartGame()
    {
        panelTutorial.SetActive(false);
        panelJuego.SetActive(true);
    }
}

