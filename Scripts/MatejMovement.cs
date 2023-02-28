using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatejMovement : MonoBehaviour
{

    public GameObject player;

    public bool active = false;
    bool safetyOn = false;
    bool safetyOn2 = false;

    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate(){
        if(active && safetyOn && safetyOn2){
            Vector3 targetDirection = new Vector3(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y, player.transform.position.z - transform.position.z);
            rb.MoveRotation(Quaternion.LookRotation(targetDirection * 100));
            //Debug.DrawRay(transform.position, targetDirection * 15, Color.blue);

            
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(player.transform.position,transform.position) > 4){
            safetyOn2 = true;
        }else{
            safetyOn2 = false;
        }

        if(active && safetyOn && safetyOn2){
            var step = 1f * Time.deltaTime * Mathf.Clamp(Vector3.Distance(player.transform.position,transform.position), 0, 25); // calculate distance to move
                if(player.transform.position.y - transform.position.y < 0){
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, transform.position.y,player.transform.position.z), step);
                }else{
                    transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x,player.transform.position.y,player.transform.position.z), step);
                }
            
        }

        //Debug.DrawRay(transform.position, transform.forward * 10, Color.green);
        Vector3 forwardDir = transform.forward;

        
        //Debug.DrawRay(transform.position, Vector3.up * 10, Color.green);
        Vector3 upDir = Vector3.up;

        //Debug.Log(Vector3.SignedAngle(forwardDir, upDir, transform.right));
        if(Vector3.SignedAngle(forwardDir, upDir, transform.right) > -3 && Vector3.SignedAngle(forwardDir, upDir, transform.right) < 3 || Vector3.SignedAngle(forwardDir, upDir, transform.right) < -177 && Vector3.SignedAngle(forwardDir, upDir, transform.right) > 177){
            safetyOn = false;
        }else{
            safetyOn = true;
        }
    }
}
