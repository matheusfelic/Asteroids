using UnityEngine;

public class Wrapper : MonoBehaviour
{
    private void Update()
    {
        Vector3 vpPosition = Camera.main.WorldToViewportPoint(transform.position);
        
        Vector3 goThruMovement = Vector3.zero;
        if (vpPosition.x < 0)
        {
            goThruMovement.x += 1;
        }
        else if (vpPosition.x > 1)
        {
            goThruMovement.x -= 1;
        }
        else if (vpPosition.y < 0)
        {
            goThruMovement.y += 1;
        }
        else if (vpPosition.y > 1)
        {
            goThruMovement.y -= 1;
        }

        transform.position = Camera.main.ViewportToWorldPoint(vpPosition + goThruMovement);
    }

}