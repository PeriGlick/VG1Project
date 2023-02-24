using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerController : MonoBehaviour
{
    public float moveSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject stand = GameObject.Find("Lemonade Stand");
        var step = moveSpeed * Time.deltaTime;
        var standPosition = stand.transform.position + new Vector3(0, -1f, 0);
        transform.position = Vector3.MoveTowards(transform.position, standPosition, step);
    }
}
