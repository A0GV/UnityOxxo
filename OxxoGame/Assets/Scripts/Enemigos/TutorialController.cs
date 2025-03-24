using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    public Image cactusImage; // Imagen del cactus (puede ser UI o SpriteRenderer)
    public Sprite[] cactusSprites; // Sprites por paso
    public Text dialogueText; // Texto en el globo
    public string[] dialogues;  // Frases del cactus

    public GameObject panelTutorial; // Panel que contiene el tutorial
    public GameObject panelJuego; // Panel que contiene el gameplay

    private int currentStep = 0;

    void Start()
    {
        ShowStep(0); // Muestra el primer paso al iniciar
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
            StartGame(); // Aquí termina el tutorial
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

