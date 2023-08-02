using System.Collections;
using UnityEngine;

public delegate void BulletHit(float damage, Vector2 hitPoint, Slime slime); 


public class Bullet : MonoBehaviour{
    public static event BulletHit OnBulletHit;


    public GameObject impact;
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    
    public float Damage;
    public float Speed;
    public int Penetration;
    public Color color = Color.white;

    public void Init(float _damage, float _speed, int _penetration){
        Damage = _damage;
        Speed = _speed;
        Penetration = _penetration;
    }
    void Start(){
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right*Speed);
        sr.color = color;
    }

    // Update is called once per frame
    void Update(){
        
    }
    
    void OnTriggerEnter2D(Collider2D collision){
        if (collision.isTrigger){
            return;
        }
        GameObject go = collision.gameObject;

        //simulate collision
        if(go.layer == 9){
            if (go.GetComponent<IFriendlyDamagable>() != null){
                go.GetComponent<IFriendlyDamagable>().TakeDamage(Damage);
            }
            BulletDestroy();
            return;
        }
        
        collision.attachedRigidbody?.AddForce((transform.position-collision.transform.position).normalized * rb.velocity.magnitude * rb.mass);
        
        if (go.GetComponent<IEnemyDamagable>() != null){
            if (go.GetComponent<IEnemyDamagable>().Health > 0){
                go.GetComponent<IEnemyDamagable>().TakeDamage(Damage);
                Slime s = go.GetComponent<Slime>();
                OnBulletHit?.Invoke(Damage, transform.position, s);
                collision.attachedRigidbody.velocity *= 0.5f;
                collision.attachedRigidbody.AddForce((transform.position - collision.transform.position).normalized *
                                                     -200);
                if (Penetration > 0){
                    Penetration--;
                    return;
                }
                else{
                    BulletDestroy();
                }
            }
        }
        else{
            BulletDestroy();
        }

    }

    void BulletDestroy(){
        Instantiate(impact, transform.position+ (Vector3)(rb.velocity*0.01f), transform.rotation);
        Destroy(gameObject);
    }
}
