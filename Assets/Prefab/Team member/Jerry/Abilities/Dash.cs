using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
  private Rigidbody2D rb;
  public float dashSpeed;
  private float dashTime;
  public float startTime;
  private int direction;

  void Start(){
      rb = GetComponent<Rigidbody2D>();
      dashTime = startTime;
  }

  void Update(){
      if(direction == 0){
          if(Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.D) ){
              direction = 2;
          }
          else if (Input.GetKeyDown(KeyCode.LeftShift) && Input.GetKey(KeyCode.A)){
              direction = 1;
          }

      }
      else{
          if(dashTime <= 0){
              direction = 0;
              dashTime = startTime;
              rb.velocity = Vector2.zero;
          }
          else{
              dashTime -= Time.deltaTime;

              if(direction == 1){
                  rb.AddForce(Vector2.left * dashSpeed);
              }
              else if(direction == 2){
                  //rb.velocity = Vector2.right * dashSpeed;
                  rb.AddForce(Vector2.right * dashSpeed);
              }
          }
      }
  }
}
