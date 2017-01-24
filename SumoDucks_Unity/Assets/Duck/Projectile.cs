using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public projectileMode projectileMode;

    private Rigidbody rb;
    public float scaleSpeed;
    public float scale;
    public float lifeTime;
    public GameObject generatedFrom;
    public float force;
    public float speed;



    // Use this for initialization
    void Start()
    {
        //rb = this.GetComponent<Rigidbody>();
        Destroy(this.gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (projectileMode == projectileMode.Projectile)
            transform.Translate(-Vector3.forward * speed * Time.deltaTime);

        //rb.AddRelativeForce(-Vector3.forward * speed);
        scale = scale + scaleSpeed * Time.deltaTime;
        transform.localScale = new Vector3(scale, 3.0f, scale);
    }

    void OnTriggerStay(Collider c)
    {

        if (projectileMode == projectileMode.Projectile)
        {
            if (c.GetComponent<Rigidbody>() != null)
            {
                c.GetComponent<Rigidbody>().AddExplosionForce(force * 50, this.transform.position, scale);
            }
            //Destroy(this.gameObject);

        }
        else if (projectileMode == projectileMode.Center)
        {
            //Push away from center
            Vector3 dir = c.transform.position - transform.position;
            dir = -dir.normalized;
            c.GetComponent<Rigidbody>().AddForce(-dir * force);
        }
    }
}

public enum projectileMode
{
    Projectile,
    Center
}