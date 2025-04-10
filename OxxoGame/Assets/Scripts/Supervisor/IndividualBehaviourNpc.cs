using System.Collections;
using UnityEngine;
using UnityEngine.U2D.IK;

public class IndividualBehaviourNpc : MonoBehaviour
{
    // Cambia a pública estática accesible desde fuera
    public static IndividualBehaviourNpc instance;
    
    private float moveSpeed = 5;
    private Animator animatorController;
    private bool isTalking = false; // Añade esta variable para controlar el estado de habla

    // Awake se ejecuta antes que Start, asegurando que la instancia esté disponible
    void Awake() 
    {
        // Initialize the singleton instance
        if (instance == null)
        {
            instance = this;
            // Opcional: si necesitas persistencia entre escenas
            // DontDestroyOnLoad(gameObject); 
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        animatorController = GetComponent<Animator>();
        // controlMovement = StartCoroutine("MoveToPosition");
    }

    // Update is called once per frame
    void Update()
    {
        // Si está hablando, no cambies la animación ni muevas el NPC
        if (isTalking)
        {
            return;
        }
        
        // Si no está hablando, comportamiento normal
        if (instance != null)
        {
            UpdateAnimation(NpcAnimation.Walking);
            this.transform.position += Vector3.left * Time.deltaTime * moveSpeed;
        }
        else if (Input.GetKey(KeyCode.T))
        {
            UpdateAnimation(NpcAnimation.Talking);
        }
        else
        {
            UpdateAnimation(NpcAnimation.Idle);
        }
    }

    public enum NpcAnimation
    {
        Idle, Talking, Walking
    }

    public void UpdateAnimation(NpcAnimation nameAnimation)
    {
        switch (nameAnimation)
        {
            case NpcAnimation.Idle:
                animatorController.SetBool("isWalking", false);
                animatorController.SetBool("isTalking", false);
                break;
            case NpcAnimation.Walking:
                animatorController.SetBool("isWalking", true);
                animatorController.SetBool("isTalking", false);
                break;
            case NpcAnimation.Talking:
                animatorController.SetBool("isWalking", false);
                animatorController.SetBool("isTalking", true);
                break;
        }
    }

    public void callTalk()
    {
        Debug.Log("Se llego a talk");
        UpdateAnimation(NpcAnimation.Talking);
        isTalking = true; // Marca que está hablando
        
        // Opcional: puedes usar una corrutina para volver a estado normal después de un tiempo
        StartCoroutine(ResetTalkingState(3.0f)); // 3 segundos de hablar
    }

    IEnumerator ResetTalkingState(float duration)
    {
        yield return new WaitForSeconds(duration);
        isTalking = false; // Vuelve a estado normal después de duration segundos
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Colisión con: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("AjoloteTerminator"))
        {
            Debug.Log("Colisión con AjoloteTerminator");
            GameObject.Destroy(this.gameObject);
        }
    }
}