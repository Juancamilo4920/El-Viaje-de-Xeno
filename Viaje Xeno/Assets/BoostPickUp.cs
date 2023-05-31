using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostPickUp : MonoBehaviour
{
    public float multi = 1.5f;
    public Vector3 spinRotationSpeed = new Vector3(0, 180, 0);
  
    PlayerController vivo;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController vivo = collision.GetComponent<PlayerController>();

        if(vivo)
        {
            vivo.boost(multi);
            Destroy(gameObject);
        }

    }
    private void Update()
    {
        transform.eulerAngles += spinRotationSpeed * Time.deltaTime;
    }
}
