using UnityEngine;

public class PlayerInput : MonoBehaviour
{
   
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
           Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
           RaycastHit2D hit = Physics2D.Raycast(ray.origin,ray.direction);

            if(hit.collider != null)
            {
                if (hit.collider.tag == "zombie")
                {
                    gameObject.GetComponent<gamemanager>().KillZombie();// this lower case gameObject means refrence this gamemanager script
                    //Debug.Log("we hit something");
                }
            } 
           
        }
    }
}
