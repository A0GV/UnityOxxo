using UnityEngine;
using UnityEngine.UIElements;

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

    // Cuando hay un choque con el escenario cambia de dirección
    private void OnCollisionEnter2D(Collision2D collision)
    {   
        if(!isup){
            direccion = Vector3.up;
            UpdateAnimation(PlayerAnimation.upie);
            isup=true;
        }else{
            direccion = Vector3.right;
            isup=!isup;
        }
    }

    //Sirve para mover automaticamente al ajolote
    void Update()
    {
        //Mover al ajolote
        this.transform.position += direccion * Time.deltaTime * velocidad;
        if(direccion==Vector3.left){
            UpdateAnimation(PlayerAnimation.leftie);
        }else if(direccion==Vector3.right){
            UpdateAnimation(PlayerAnimation.rightie);
        }else if(direccion==Vector3.up){
            UpdateAnimation(PlayerAnimation.upie);
        }
        
        //Eliminar al ajolote
        if(transform.position.y<=-50){
            GameObject.Destroy(this.gameObject);
        }
    }

    //Lista de animaciones del ajolote
    public enum PlayerAnimation{
        leftie, rightie, upie, downie
    }
    //Cambios
    void UpdateAnimation(PlayerAnimation nameAnimation){
        switch(nameAnimation)
        {
            case PlayerAnimation.leftie:
                animatorController.SetBool("IsLeftie",true);
                animatorController.SetBool("IsRightie",false);
                animatorController.SetBool("IsUpie",false);
                animatorController.SetBool("IsDownie",false);
            break;
            case PlayerAnimation.rightie:
                animatorController.SetBool("IsLeftie",false);
                animatorController.SetBool("IsRightie",true);
                animatorController.SetBool("IsUpie",false);
                animatorController.SetBool("IsDownie",false);
            break;
            case PlayerAnimation.upie:
                animatorController.SetBool("IsLeftie",false);
                animatorController.SetBool("IsRightie",false);
                animatorController.SetBool("IsUpie",true);
                animatorController.SetBool("IsDownie",false);
            break;
            case PlayerAnimation.downie:
                animatorController.SetBool("IsLeftie",false);
                animatorController.SetBool("IsRightie",false);
                animatorController.SetBool("IsUpie",false);
                animatorController.SetBool("IsDownie",true);
            break;
        }
    }
}
