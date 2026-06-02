using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //Libreria para poder acceder al "NavMeshAgent"

public class EnemyMovement : MonoBehaviour
{
    GameObject player; //Variable que hace referencia al player, para que el enemigo pueda saber a quien debe seguir
    NavMeshAgent agent; //Variable para acceder a este, y poder controlar al enemigo
    Animator anim; //Variable para las animaciones

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); //Guardamos la informacion del player, en la variable "player"
        agent= GetComponent<NavMeshAgent>(); //Accedemos por medio del GetComponent al NavMeshAgent y lo guardamos en "agent"
        anim = GetComponent<Animator>(); 
    }

    void Update()
    {
        if (player != null) //Si player no es nulo, entonces que lo siga y vaya a la posicion de este
        {
            agent.SetDestination(player.transform.position); 
        }

        Animating(); //Llmada a la funcion
    }

    void Animating() //Funcion para controlar el movimiento del enemigo
    {
        if(agent.velocity.magnitude != 0) //A traves de su magnitud, sabemos si se está moviendo o no
        {
            anim.SetBool("IsMoving", true);
        }
        else
        {
            anim.SetBool("IsMoving", false);
        }
    }
}
