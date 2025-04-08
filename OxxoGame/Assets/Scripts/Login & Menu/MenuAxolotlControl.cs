using UnityEngine;
using UnityEngine.InputSystem;

public class MenuAxolotlControl : MonoBehaviour
{
    // Adjust movement speed and jump force
    public float moveSpeed;

    // References to player components
    public Rigidbody2D rig; // Controls the character's physics
    public SpriteRenderer sr; // Manages the sprite visualization
    Animator animatorController; // Animation controller
    public Sprite[] Default;
    public Sprite[] Default_Walk;
    public Sprite[] Sombrero;
    public Sprite[] Sombrero_Walk;
    public Sprite[] Sombrero2;
    public Sprite[] Sombrero2_Walk;
    public Sprite[] Sombrero3;
    public Sprite[] Sombrero3_Walk;
    public Sprite[] Sombrero4;
    public Sprite[] Sombrero4_Walk;


    // Enumeración para identificar las skins disponibles
    public enum SkinType
    {
        Default,
        Sombrero,
        Sombrero2,
        Sombrero3,
        Sombrero4
    }

    // Variables para controlar la animación
    private SkinType currentSkin = SkinType.Default;
    private float animationTimer = 0f;
    private int currentSpriteIndex = 0;
    public float frameRate = 0.1f; // Tiempo entre cambios de sprite
    private bool isWalking = false;

    void Start()
    {
        // Obtener el componente Animator
        animatorController = GetComponent<Animator>();
        if (sr == null)
        {
            sr = GetComponent<SpriteRenderer>();
            Debug.LogWarning("SpriteRenderer no asignado, obteniendo automáticamente");
        }
        // Inicializar con el sprite por defecto
        UpdateSpriteBasedOnState();
    }

    void Update()
    {
        // Cambiar la dirección en función de la velocidad X
        if (rig.linearVelocity.x > 0)
        {
            sr.flipX = false;
        }
        else if (rig.linearVelocity.x < 0)
        {
            sr.flipX = true;
        }

        // Manejar la animación de sprites manualmente
        AnimateSprite();
    }

    private void FixedUpdate()
    {
        // Capturar entrada del jugador en el eje horizontal
        float xInput = Input.GetAxis("Horizontal");

        // Actualizar la velocidad del Rigidbody2D en el eje X
        rig.linearVelocity = new Vector2(xInput * moveSpeed, rig.linearVelocity.y);

        // Determinar si el personaje está caminando (velocidad X no es cero)
        bool shouldBeWalking = Mathf.Abs(xInput) > 0.01f;
        
        // Solo actualizar isWalking y la animación si hay un cambio
        if (isWalking != shouldBeWalking)
        {
            isWalking = shouldBeWalking;
            
            // Actualizar la animación basada en el estado
            UpdateAnimation(isWalking ? PlayerAnimation.walk : PlayerAnimation.idle);
            
            // Reiniciar el índice de sprite para evitar cambios bruscos
            currentSpriteIndex = 0;
        }
    }

    // Enum que define los estados de animación del jugador
    public enum PlayerAnimation
    {
        idle, walk, jump
    }

    // Método para actualizar la animación basada en el estado
    void UpdateAnimation(PlayerAnimation nameAnimation)
    {
        switch (nameAnimation)
        {
            case PlayerAnimation.idle:
                // Configurar parámetros del Animator para "idle"
                animatorController.SetBool("isWalking", false);
                break;

            case PlayerAnimation.walk:
                // Configurar parámetros del Animator para "walk"
                animatorController.SetBool("isWalking", true);
                break;

            case PlayerAnimation.jump:
                // Configurar parámetros del Animator para "jump"
                animatorController.SetBool("isWalking", false);
                break;
        }

        // Actualizar el sprite basado en el estado actual
        UpdateSpriteBasedOnState();
    }

    // Nuevo método para cambiar la skin del personaje
    public void ChangeSkin(int skinIndex)
    {
        if (skinIndex < 0 || skinIndex > 4)
        {
            Debug.LogWarning($"Índice de skin inválido: {skinIndex}. Debe estar entre 0 y 4.");
            return;
        }
        
        Debug.Log($"Cambiando skin a: {skinIndex}");
        
        // Desactivar temporalmente el Animator
        if (animatorController != null)
        {
            animatorController.enabled = false;
        }
        
        // Cambiar la skin actual
        currentSkin = (SkinType)skinIndex;
        
        // Reiniciar la animación
        currentSpriteIndex = 0;
        animationTimer = 0f;
        
        // Actualizar el sprite basado en el estado actual
        UpdateSpriteBasedOnState();
        
        Debug.Log($"Skin cambiada a: {currentSkin} - Sprite actual: {sr?.sprite?.name ?? "null"}");
    }


    // Método para actualizar el sprite basado en el estado y skin actual
    private void UpdateSpriteBasedOnState()
    {
        // Si no está caminando, usamos el sprite idle correspondiente
        if (!isWalking)
        {
            sr.sprite = GetCurrentIdleSprites()[0]; // Usar el primer sprite de idle
        }
        else
        {
            // Si está caminando, usamos el sprite walk correspondiente
            sr.sprite = GetCurrentWalkSprites()[0]; // Usar el primer sprite de walk
        }
    }

    // Método para animar los sprites manualmente
    private void AnimateSprite()
    {
        // Incrementar el timer
        animationTimer += Time.deltaTime;

        // Si ha pasado suficiente tiempo, cambiar al siguiente sprite
        if (animationTimer >= frameRate)
        {
            animationTimer = 0f;

            Sprite[] currentSprites = isWalking ? GetCurrentWalkSprites() : GetCurrentIdleSprites();
            
            // Verificar que el array tenga elementos
            if (currentSprites != null && currentSprites.Length > 0)
            {
                // Avanzar al siguiente sprite
                currentSpriteIndex = (currentSpriteIndex + 1) % currentSprites.Length;
                sr.sprite = currentSprites[currentSpriteIndex];
                
                // Reducir logs - solo mostrar cada 10 cambios de frame o cuando debugging
                if (Time.frameCount % 10 == 0)
                    Debug.Log($"Animando: {currentSkin} - Sprite: {sr.sprite.name}");
            }
            else
            {
                Debug.LogWarning($"Array de sprites vacío para skin {currentSkin}");
            }
        }
    }

    // Métodos para obtener el array de sprites correcto según la skin actual
    private Sprite[] GetCurrentIdleSprites()
    {
        Sprite[] result;
        switch (currentSkin)
        {
            case SkinType.Default:
                result = Default;
                break;
            case SkinType.Sombrero:
                result = Sombrero;
                break;
            case SkinType.Sombrero2:
                result = Sombrero2;
                break;
            case SkinType.Sombrero3:
                result = Sombrero3;
                break;
            case SkinType.Sombrero4:
                result = Sombrero4;
                break;
            default:
                result = Default;
                break;
        }
        
        if (result == null || result.Length == 0)
        {
            Debug.LogError($"Array idle vacío para skin {currentSkin}");
            return Default; // Usa Default como fallback
        }
        
        return result;
    }

    private Sprite[] GetCurrentWalkSprites()
    {
        Sprite[] result;
        
        switch (currentSkin)
        {
            case SkinType.Default:
                result = Default_Walk;
                break;
            case SkinType.Sombrero:
                result = Sombrero_Walk;
                break;
            case SkinType.Sombrero2:
                result = Sombrero2_Walk;
                break;
            case SkinType.Sombrero3:
                result = Sombrero3_Walk;
                break;
            case SkinType.Sombrero4:
                result = Sombrero4_Walk;
                break;
            default:
                result = Default_Walk;
                break;
        }
        
        if (result == null || result.Length == 0)
        {
            Debug.LogWarning($"Array walk vacío para skin {currentSkin}, usando Default_Walk");
            return Default_Walk; // Fallback a los sprites por defecto
        }
        
        return result;
    }
}