using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealht : MonoBehaviour
{
    public int maxHealth; //Vida total
    public int currenthealth; //Vida actual
    public float sinkSpeed; // Variable para el hundimiento del player
    public int ScoreValue; //Para saber los puntos que gana el player cuando mata al enemigo
    public bool isDead; //Para saber si el enemigo está muerto o no
    Animator anim; //Variable para controlar las animaciones 
    bool isSinKing; //Para saber si el enemigo se está hundiendo


    void Start() //Al inicio la vida actual, será igual a la vida maxima
    {
        currenthealth = maxHealth;
        anim = GetComponent<Animator>(); //Con esto accedemos al componente animator y lo guardamos en la variable 
    }


    void Update()
    {

    }

    public void TakeDamage(int amount) //Esta se encargará de llamar al script de disparo de player, para que a traves de este, pueda saber que debe hacerle daño al enemigo
    {                                  //El "int amount" es una varibale local, a la que se le da valor por medio de la llamada a la funcion

        if (isDead) return; //Recordar que el "return" hace que nos salgamos de la funcion, si se cumple su condicion
        currenthealth -= amount;

        if (currenthealth <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;
        Destroy(gameObject);
    }
}
