using UnityEngine;

public class AjolotediaControl : MonoBehaviour
{
    public float velocidad;
    Animator animatorController;
    public SpriteRenderer sr;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animatorController = GetComponent<Animator>();  
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("escenario")){
            velocidad *= -1;
        }
    }
    void Update()
    {
        //Mover al ajolote
        this.transform.position += Vector3.left * Time.deltaTime * velocidad;

        //Eliminar al ajolote
        if(transform.position.y<=-50){
            GameObject.Destroy(this.gameObject);
        }
    }
}
