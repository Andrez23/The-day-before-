using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot; //Da�o por disparo
    public float timeBetweenBullets; //Timempo de disparo
    public float range; //Loguitud del Rycast, es decir, la loguitud a la que podremos disparar
    public LayerMask shootableMask; //Variable para la capa a la que podremos disparar

    float timer; //Temporizador
    Ray ray; //Creacion Rycast
    RaycastHit hit; //Donde impacta el rayo, o bueno, con que choca
    LineRenderer lineRenderer;//Variable con la que podremos acceder a este, mediante el GetComponent
    float effectsDisplayTime = 0.2f; //Variable que determinar� el tiempo en pantalla de los efectos
    Light gunLight; //Variable para la luz

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>(); //Hacemos referencia a este, para que accedamos por medio del GetComponent
        gunLight = GetComponent<Light>(); //Hacemos referencia a esta, y accedemos mediante el GetComponent
    }

    void Update()
    {
        timer += Time.deltaTime; //Contador de tiempo

        if (Input.GetMouseButtonDown(0) && timer >= timeBetweenBullets)  //Al presionar un boton, llamamos a shoot, para que se ejecute
        {
            Shoot(); //Llamada a la funcion 
        } 
        if(timer  >= timeBetweenBullets * effectsDisplayTime) //Cuando el tiempo entre balas sea mayor a los efectos, estos se desactivan
        {
            lineRenderer.enabled= false;
            gunLight.enabled= false;
        }
    }

    void Shoot() //Esta gestiona el disparo. Es decir, que har� y como el rayo
    {
        timer = 0;
        lineRenderer.enabled = true; //Con esto habilitamos el componente, es decir, la linea que actua como mira
        gunLight.enabled = true; //Con esto activamos la luz del arma

        lineRenderer.SetPosition(0, transform.position); //Establecemos el primer punto, y de donde saldr� el rayo

        ray.origin= transform.position;
        ray.direction= transform.forward;

        if(Physics.Raycast(ray, out hit, range, shootableMask)) //COn esto, lanzamos el rayo
        {
            GameObject _object = hit.collider.gameObject; //Variable local que guarda el gameObject con el que choca el rayo

            if (_object.GetComponent<EnemyHealht>()) //Le dice, que si contiene el script de vida de enemigo, esta autorizado a hacerle daño
            {
                _object.GetComponent<EnemyHealht>().TakeDamage(damagePerShot); //Llamamos a la funcion del enemigo, y a traves de la cantidad de daño por tiro, es que matamos al enemigo
            }

            lineRenderer.SetPosition(1, hit.point); //Establecemos el segundo punto, es decir, el punto de choque del lineRenderer
        }
    }
}
