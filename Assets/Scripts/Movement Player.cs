//Librerias - Funciones Prestadas de otros scripts
using UnityEngine;

//Public - Da permiso de usar su informacion
    //class - forma de declararla
        //MovementPlayer - Nombre del Script
            // : - Herencia, permite usar las funciones y variables de MonoBehaviour
public class MovementPlayer : MonoBehaviour
{
    int count = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        print("Juan");
    }

    // Update is called once per frame
    void Update()
    {
        print(count);
        count++;
    }
}
