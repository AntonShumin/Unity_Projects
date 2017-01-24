using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DucklingsGenerator : MonoBehaviour
{
    public GameObject ducklingContainer;
    public Material m_duck_material;

    public int desiredAmount, currentAmount;

    public GameObject ducklingPrefab;
    public List<GameObject> ducklings;

    public bool removeDuckling;
    public bool addDuckling;

    public int at;

    // Use this for initialization
    void Start()
    {
        ducklings.Clear();

        for (int d = 0; d < desiredAmount; d++)
        {
            GameObject duckling = Instantiate(ducklingPrefab, this.transform.position, Quaternion.identity);
            ducklings.Add(duckling);
            duckling.transform.parent = ducklingContainer.transform;
        }

        for (int d = 0; d < ducklings.Count; d++)
        {
            if (d - 1 >= 0)
                ducklings[d].GetComponent<FollowScript>().toFollow = ducklings[d - 1];
            else
                ducklings[d].GetComponent<FollowScript>().toFollow = this.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if(transform.position.y < -700f) {

            gameObject.GetComponent<Rigidbody>().useGravity = false;
            /*Vector3 new_position = gameObject.transform.position;
          new_position.y = -300f;
           gameObject.transform.position = new_position;*/
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        } 

        if (desiredAmount != currentAmount)
        {
            DestroyDucklings();
            InstantiateDucklings();
            reloadReloadFollowOrder();

            currentAmount = desiredAmount;
        }

        if (removeDuckling)
        {
            RemoveDuckling(at);
            removeDuckling = false;
        }

        if (addDuckling)
        {
            AddDuckling(at);
            addDuckling = false;
        }

    }

    public void RemoveDuckling(int i) {
        Destroy(ducklings[i]);
        ducklings.RemoveAt(i);
        reloadReloadFollowOrder();
    }

    public void AddDuckling(int i)
    {
        GameObject duckling = Instantiate(ducklingPrefab, this.transform.position, Quaternion.identity);
        ducklings.Add(duckling);
        duckling.transform.parent = ducklingContainer.transform;
        reloadReloadFollowOrder();

    }

    public void InstantiateDucklings() {
        for (int d = 0; d < desiredAmount; d++)
        {
            GameObject duckling = Instantiate(ducklingPrefab, this.transform.position, Quaternion.identity);
            ducklings.Add(duckling);
            duckling.transform.parent = ducklingContainer.transform;
            //duckling.GetComponent<Renderer>().material = m_duck_material;
        }
    }

    public void reloadReloadFollowOrder() {
        for (int d = 0; d < ducklings.Count; d++)
        {
            if (d - 1 >= 0)
                ducklings[d].GetComponent<FollowScript>().toFollow = ducklings[d - 1];
            else
                ducklings[d].GetComponent<FollowScript>().toFollow = this.gameObject;
        }
    }

    public void DestroyDucklings() {
        ducklings.Clear();

        foreach (GameObject d in GameObject.FindGameObjectsWithTag("Duckling"))
        {
            if (d.transform.parent == ducklingContainer.transform)
                Destroy(d);
        }
    }
}
