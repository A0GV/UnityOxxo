using System.Collections;
using UnityEngine;
using UnityEngine.U2D.IK;

public class IndividualBehaviourNpc : MonoBehaviour
{

    private float moveSpeed = 5;
    private Animator animatorController;

    public float position1;

    public bool goToSuicide;

    private Coroutine controlMovement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animatorController = GetComponent<Animator>();
        controlMovement = StartCoroutine("MoveToPosition");

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            // Debug.Log("W pressed");
            UpdateAnimation(NpcAnimation.Walking);
            this.transform.position += Vector3.left * Time.deltaTime * moveSpeed;

        }
        else if (Input.GetKey(KeyCode.T))
        {
            // Debug.Log("T pressed");
            UpdateAnimation(NpcAnimation.Talking);
        }
        else
        {
            // Debug.Log("No key pressed");
            UpdateAnimation(NpcAnimation.Idle);
        }
    }

    public enum NpcAnimation
    {
        Idle, Talking, Walking
    }

    void UpdateAnimation(NpcAnimation nameAnimation)
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Colisión con: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("AjoloteTerminator"))
        {
            Debug.Log("Colisión con AjoloteTerminator");
            GameObject.Destroy(this.gameObject);
        }
    }

    IEnumerator MoveToPosition()
    {
        while (goToSuicide && transform.position.x > position1)
        {
            transform.position += Vector3.left * Time.deltaTime * moveSpeed;
            yield return null; // Espera un frame antes de continuar
        }
    }

    public void restarMovement()
    {
        if (controlMovement != null)
        {
            StopCoroutine(controlMovement);
            controlMovement = null;
        }
    }

    public void stopMovement()
    {
        if (controlMovement==null)
        {
            controlMovement=StartCoroutine("MoveToPosition");
        }

    }



}
