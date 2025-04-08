using UnityEngine;
using UnityEngine.UI;

public class SkinButton : MonoBehaviour
{
    public MenuAxolotlControl playerController;
    public Button[] skinButtons; // Array de botones de UI
    
    void Start()
    {
        // Verificar que tenemos referencia al controlador del jugador
        if (playerController == null)
        {
            Debug.LogError("No se ha asignado playerController en SkinButtonController");
            return;
        }
        
        // Configurar cada botón si el array está asignado
        if (skinButtons != null && skinButtons.Length > 0)
        {
            for (int i = 0; i < skinButtons.Length; i++)
            {
                if (skinButtons[i] != null)
                {
                    int skinIndex = i; // Capturar el valor del índice para el closure
                    skinButtons[i].onClick.AddListener(() => ChangeSkin(skinIndex));
                }
            }
        }
        else
        {
            Debug.LogWarning("No se han asignado botones en SkinButtonController");
        }
    }
    
    void ChangeSkin(int skinIndex)
    {
        playerController.ChangeSkin(skinIndex);
        Debug.Log("Cambiando a skin: " + skinIndex);
    }
}