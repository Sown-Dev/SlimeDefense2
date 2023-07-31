namespace DefaultNamespace;

public class GoldDrop : MonoBehaviour
{
    public float amount;

    public bool PickedUp;
    public Transform Goto;

    void Start()
    {
        Goto = transform.position;
    }

    void Update()
    {
        if (PickedUp)
        {
            transform.position = Mathf.Lerp(transform.position, Goto.position, Time.deltaTime * 10);
            if (Vector2.distance(transform.position, Goto.position) < 0.5f)
            {
                Destroy(gameObject);
            }
        }
    }
}