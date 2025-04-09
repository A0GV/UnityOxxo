using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject[] Axolots;
    public Transform SpawnPoint;
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
        int skinIndex = PlayerPrefs.HasKey("id") ? PlayerPrefs.GetInt("id") : 0;
        
        playerSkin = Axolots[skinIndex];
        

        player = Instantiate(playerSkin, SpawnPoint.position, Quaternion.identity);

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
