using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    
    [SerializeField] public float runningSpeed = 40;
    [SerializeField] public float rotationSpeed = 40;
    private Vector3 direction;
    public RaycastHit hit;
    private Vector3 point;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdatePoint", 0f, 0.005f);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Debug.Log(transform.forward);
        direction = transform.position-point;
        Quaternion rot = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
        transform.eulerAngles = new Vector3(0,rot.eulerAngles.y,0);
        if (Input.GetKey("w"))
        {
            
            point.y = transform.position.y;
            transform.position = Vector3.MoveTowards(transform.position, point, Time.deltaTime * runningSpeed);

        }
        
    }
    private void UpdatePoint()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            point = hit.point;
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow);
        }
    }
}
