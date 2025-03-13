using UnityEngine;

public class AjolotediaControl : MonoBehaviour
{
    public float velocidad;
    Animator animatorController;
    private Vector3 direccion = Vector3.left;
    bool isup=false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animatorController = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {   
        if(!isup){
            direccion = Vector3.up;
            isup=true;
        }else{
            direccion = Vector3.right;
            isup=!isup;
        }
    }
    void Update()
    {
        //Mover al ajolote
        this.transform.position += direccion * Time.deltaTime * velocidad;

        //Eliminar al ajolote
        if(transform.position.y<=-50){
            GameObject.Destroy(this.gameObject);
        }
    }
}
