using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public int speed;  //Variable publica para guardar la velocidad 
    public LayerMask layerFloor; // Variable publica que nos permitir� crear la capa en el suelo para que no haya problema con las interacciones

    Rigidbody rb; //Variable tipo rigibody, para poder controlar el movimiento a traves de las fisicas 
    Vector3 movement; //Con este, guardamos la direccion de movimiento de nuestro player
    float horizontal;
    float vertical;  //Estas son las que guardaran los inputs, es decir, los movimientos que se hagan las presionar W-A-S-D

    bool isAiming= false; //Variable que controla el apuntado, y por defecto est� as�, debido a que el personaje no apunta apenas se ejecuta el juego

    Animator anim; //Variable para poder controlar cada uno de los estados 
 
    void Start()
    {
        rb = GetComponent<Rigidbody>(); //Con esto hacemos referencia a nuestra variable, es decir, que accedemos a ella a traves del "GetComponent" a su rigidbody
        anim = GetComponent<Animator>(); //Accedemos con el GetComponent al Animator, para poder controlar los estados
    }

    
    void Update()
    {
        InputPlayer();  //Hacemos llamado a la funcion, para que se ejecute una vez vada frame como se hace en el Update
        Debug.Log("Horizontal: " + horizontal + " | Vertical: " + vertical);
    }

    void FixedUpdate() //Este se ejecuta (cada 0.02 seg) de manera constante para que la fuerza y fisicas sean constantes, es decir, que sin importar los frames, no se desiguale el movimiento
    {
        Move();
        Turning(); 
        Animating();
        HandleAiming();
    }

    void InputPlayer() //Este nos va a permitir guardar el movimiento de nuestras variables a traves de los inputs  
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    void Move() //Funcion que nos permitir� mover al "Player"
    {
        movement= new Vector3(horizontal,0,vertical);
        movement.Normalize(); //Normalizamos el vector,para que su modulo sea igual a 1, y no se mueva mas rapido de lo que su velocidad determina
        rb.MovePosition(transform.position+(movement*speed*Time.deltaTime)); //Esto es lo que nos permite movernos
    }

    void Turning()  //Funcion que nos permite girar al "Player"
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //Esto es lo que nos permite crear una direccion de mouse en la pantalla, siendo que a traves de los inputs permite la direccion de movimiento
        //Debug.DrawRay(ray.origin, ray.direction * 1000, Color.yellow);
        RaycastHit hit; //Recordar que esta variale nos permite guardar informacion del choque del Raycast con el GameObject


        //NOTA: En Unity crearemos un suelo, con la capa "LayerFloor", que nos permita mover al "Player" sin ningun problema. Es decir, que las colisiones del RayCast, solo detecten el suelo creado y no los demas objetos, para que pueda girar sin problema y no detectar mas objetos

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerFloor))
        {
            Vector3 playerToMouse = hit.point - transform.position;  //El playerToMouse, equivale al vector que hay entre el punto de impacto y la posicion del "Player"
            playerToMouse.y = 0; //Este hace que no tengamos movimientos extra�os en ese eje

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse); //Calculamos la rotacion, es decir, que con respecto al movimiento del mouse (vector), lo alinee con la rotacion del personaje 

            rb.MoveRotation(newRotation); //Aplicamos la rotacion, es decir, mover el objeto 

            Debug.Log("Raycast hit: " + hit.collider.gameObject.name);
        }
        else
        {
            Debug.Log("Raycast no golpe� nada"); 
        }
    }

    void Animating() // M�todo para saber si nos estamos moviendo o no, o si estamos apuntando o no
    {
        if (horizontal != 0 || vertical != 0) // El que controla nuestro movimiento son los "inputs", vertical y horizontal, que al ponerlos como condicionales, diremos si cambia o no la transici�n
        {
            anim.SetBool("IsMoving", true); // Con el "setBool", le damos valor al par�metro booleano que tenemos en el animator
        }
        else
        {
            anim.SetBool("IsMoving", false);
        }

        // Ahora tambi�n cambiamos el estado de la animaci�n al apuntar
        anim.SetBool("IsAiming", isAiming); // Cambia el valor de "IsAiming" en el Animator
    }

    void HandleAiming() //Metodo para saber si disparamos o no
    {
        if (Input.GetButtonDown("Fire2")) 
        {
            isAiming = true;
        }
        else if (Input.GetButtonUp("Fire2"))
        {
            isAiming = false;
        }
    }
}
