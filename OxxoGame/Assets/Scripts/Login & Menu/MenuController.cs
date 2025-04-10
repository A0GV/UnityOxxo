using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject[] Axolots;
         Transform SpawnPoint;
    private GameObject player;

    void Awake()
    {
        if (player == null)
        {
            setSkin();
        }
    }

    public void setSkin()
    {
        GameObject playerSkin;
        int skinIndex = PlayerPrefs.HasKey("id_skin") ? PlayerPrefs.GetInt("id_skin") : 0;
        Debug.Log(skinIndex);
        Debug.Log("Has key"+PlayerPrefs.HasKey("id_skin"));
        Debug.Log("get int idskin"+PlayerPrefs.GetInt("id_skin"));
        
        playerSkin = Axolots[skinIndex];
        

        player = Instantiate(playerSkin, SpawnPoint.position, Quaternion.identity);
        Debug.Log(player);
        MenuAxolotlControl controller = player.GetComponent<MenuAxolotlControl>();
        if (controller != null)
        {
            controller.enabled = true;

            // Configurar componentes solo si es necesario
            if (controller.rig == null)
                controller.rig = player.GetComponent<Rigidbody2D>();

            if (controller.sr == null)
                controller.sr = player.GetComponent<SpriteRenderer>();

            // Añadir un log para verificar
        }
    }
}
