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
        int skinIndex = PlayerPrefs.HasKey("id_skin") ? PlayerPrefs.GetInt("id_skin") : 0;
        Debug.Log(skinIndex);
        Debug.Log("Has key" + PlayerPrefs.HasKey("id_skin"));
        Debug.Log("get int idskin" + PlayerPrefs.GetInt("id_skin"));

        playerSkin = Axolots[skinIndex];


        player = Instantiate(playerSkin, SpawnPoint.position, Quaternion.identity);
        Debug.Log(player);
        MenuAxolotlControl controller = player.GetComponent<MenuAxolotlControl>();
    }
}
