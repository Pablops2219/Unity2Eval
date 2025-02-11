using UnityEngine;

public class MovimientoWASD : MonoBehaviour
{
    public float velocidad = 2f;
    public float rotacion = 200f;
    private Animator animator; // Referencia al Animator

    void Start()
    {
        animator = GetComponent<Animator>(); // Obtiene el Animator del personaje
    }

    void Update()
    {
        movimiento();
    }

    void movimiento()
    {
        float moverZ = Input.GetAxis("Vertical");
        float rotarX = Input.GetAxis("Horizontal");

        // Mueve el personaje
        transform.Translate(Vector3.forward * moverZ * velocidad * Time.deltaTime);
        transform.Rotate(Vector3.up * rotarX * rotacion * Time.deltaTime);

        // Actualiza el parámetro "isMoving" en el Animator
        if (animator != null)
        {
            animator.SetBool("isMoving", Mathf.Abs(moverZ) > 0);
        }
    }
}