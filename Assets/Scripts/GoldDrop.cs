
using UnityEngine;

public class GoldDrop : MonoBehaviour
{
    public float amount;

    public bool PickedUp;
    public Transform Goto;



    void Update()
    {
        if (PickedUp)
        {
            transform.position = Vector2.Lerp(transform.position, Goto.position, Time.deltaTime * 10);
            if (Vector2.Distance(transform.position, Goto.position) < 0.5f)
            {
                Destroy(gameObject);
            }
        }
    }
}