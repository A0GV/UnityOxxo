using UnityEngine;

public class IndividualBehaviourNpc : MonoBehaviour
{

    public float moveSpeed;
    private Animator animatorController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animatorController = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Debug.Log("W pressed");
            UpdateAnimation(NpcAnimation.Walking);
            this.transform.position += Vector3.left * Time.deltaTime * moveSpeed;
            if (transform.position.x < -50)
            {
                GameObject.Destroy(this.gameObject);
            }

        }
        else if (Input.GetKey(KeyCode.T))
        {
            Debug.Log("T pressed");
            UpdateAnimation(NpcAnimation.Talking);
        }
        else
        {
            Debug.Log("No key pressed");
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
}
