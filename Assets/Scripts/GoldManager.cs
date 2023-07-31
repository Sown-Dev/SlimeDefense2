namespace DefaultNamespace;

public class GoldManager :MonoBehaviour
{
    public GameObject gold;
    
    public void SpawnGold(Vector2 pos, float amount)
    {
        GameObject goldGO = Instantiage(gold, pos, Quaternion.Identity);
        goldGO.GetComponent<GoldDrop>().amount = amount;
        
    }
}