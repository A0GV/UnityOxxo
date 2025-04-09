using UnityEngine;
using UnityEngine.InputSystem;

public class MenuAxolotlControl : MonoBehaviour
{
    // Adjust movement speed and jump force
    public float moveSpeed;
    public GameObject[] Axolots;
    public Transform SpawnPoint;
    private GameObject player;
    // References to player components
    public Rigidbody2D rig; // Controls the character's physics
    public SpriteRenderer sr; // Manages the sprite visualization
    Animator animatorController; // Animation controller


    void Awake()
    {
        if (player==null)
        {
            setSkin();
        }
    }
    void Start()
    {
        // Gets the Animator component from the object
        animatorController = GetComponent<Animator>();
    }

    void Update()
    {

        // Change the character's facing direction based on its X velocity
        if(rig.linearVelocity.x > 0){
            sr.flipX = false;
        }else if(rig.linearVelocity.x < 0){
            sr.flipX = true;
        }
    }


    private void FixedUpdate()
    {
        // Capture player input on the horizontal axis (left/right arrows)
        float xInput = Input.GetAxis("Horizontal");

        // Update the character's Rigidbody2D velocity on the X-axis
        rig.linearVelocity = new Vector2(xInput * moveSpeed, rig.linearVelocity.y);

        // If the character is moving and not in the air, change animation to "walk"
        if(xInput != 0 && rig.linearVelocity.y == 0){
            UpdateAnimation(PlayerAnimation.walk);
        }
        else{
            // If the character is not moving, change animation to "idle"
            UpdateAnimation(PlayerAnimation.idle);
        }
    }

    // Enumeration defining the different animation states of the player
    public enum PlayerAnimation{
        idle, walk, jump
    }

    // Method to update the character's animation based on its state
    void UpdateAnimation(PlayerAnimation nameAnimation){
        switch(nameAnimation){
            case PlayerAnimation.idle:
                // Set Animator parameters for "idle" state
                animatorController.SetBool("isWalking", false);
                break;

            case PlayerAnimation.walk:
                // Set Animator parameters for "walk" state
                animatorController.SetBool("isWalking", true);
                break;

            case PlayerAnimation.jump:
                // Set Animator parameters for "jump" state
                animatorController.SetBool("isWalking", false);
                break;
        }
    }

    public void setSkin()
    {
        GameObject playerSkin;
        
        if (PlayerPrefs.HasKey("id")) 
        {
            playerSkin = Axolots[PlayerPrefs.GetInt("id")];
        }
        else
        {
            // Valor por defecto si no existe "id" en PlayerPrefs
            playerSkin = Axolots[0]; 
        }
        
        player = Instantiate(playerSkin, SpawnPoint.position, Quaternion.identity);
    }
}