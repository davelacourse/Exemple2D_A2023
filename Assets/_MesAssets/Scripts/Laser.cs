using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float _vitesseLaser = 20f;
    
    private void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * _vitesseLaser);

        if (transform.position.y > 9f)
        {
            if(transform.parent == null)
            {
                Destroy(gameObject);
            }
            else
            {
                Destroy(transform.parent.gameObject);
            }
            
        }
    }
}
