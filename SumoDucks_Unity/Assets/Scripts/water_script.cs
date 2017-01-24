using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class water_script : MonoBehaviour {

    float scale = 0.3f;
    public float speed;
    float noiseStrength = 1f;
    float noiseWalk = 1f;
    public float srcx1 = 0f;
    public float srcy1 = 0f;
    public Vector3 force;
    int eventIndexer = 0;

    public float srcx2 = 0f;
    public float srcy2 = 0f;
    public GameObject mother_blob222;

    private Vector3[] baseHeight;
    Mesh mesh;
    public List<float> eventTimes;
    public List<Vector3> eventPlaces;

    [Header("Plane sizes")]
    public int width = 1;
    public int height = 1;
    public float xSize, ySize = 1;
    public float eventTime;
    
    public float waveSize = 1f;

    public bool player1Wave, player2Wave;

    Vector3 eventPos;
    // Use this for initialization
    void Awake () {
        mesh = GetComponent<MeshFilter>().mesh = ParametricPlane.GeneratePlane(width, height, xSize / 2, ySize / 2, 0, xSize, ySize);
        waveSize = 1f;
        eventTimes.Add(Time.time);
        eventTimes.Add(Time.time);
        eventTimes.Add(Time.time);
        eventTimes.Add(Time.time);
        eventTimes.Add(Time.time);
        eventTimes.Add(Time.time);
        eventTimes.Add(Time.time);

        Vector3 a;
        a.x = 0;// srcx1;
        a.y = 0;// srcy1;
        a.z = 0;
        eventPlaces.Add(a);
        eventPlaces.Add(a);
        eventPlaces.Add(a);
        eventPlaces.Add(a);
        eventPlaces.Add(a);
        eventPlaces.Add(a);
        eventPlaces.Add(a);


    }



    // Update is called once per frame
    void Update () {


        if (baseHeight == null)
            baseHeight = mesh.vertices;

        Vector3[] vertices = new Vector3[baseHeight.Length];
        //for (int ii = 0; ii < eventTimes.Count; ii++)

        //{
            Vector3 eventPos1 = eventPlaces[0];
            float eventTime1 = eventTimes[0];

            Vector3 eventPos2 = eventPlaces[1];
            float eventTime2 = eventTimes[1];

            Vector3 eventPos3 = eventPlaces[2];
            float eventTime3 = eventTimes[2];

        Vector3 eventPos4 = eventPlaces[3];
        float eventTime4 = eventTimes[3];

         Vector3 eventPos5 = eventPlaces[4];
        float eventTime5 = eventTimes[4];

        Vector3 eventPos6 = eventPlaces[5];
        float eventTime6 = eventTimes[5];

        Vector3 eventPos7 = eventPlaces[6];
        float eventTime7 = eventTimes[6];
        //}
        float wavePosEvent1 = (Time.time - eventTime1) * speed;
        float wavePosEvent2 = (Time.time - eventTime2) * speed;
        float wavePosEvent3 = (Time.time - eventTime3) * speed;
        float wavePosEvent4 = (Time.time - eventTime4) * speed;
        float wavePosEvent5 = (Time.time - eventTime5) * speed;
        float wavePosEvent6 = (Time.time - eventTime6) * speed;
        float wavePosEvent7 = (Time.time - eventTime7) * speed;

        for (int i = 0; i < vertices.Length; i++)
            {
                Vector3 vertex = baseHeight[i];

            //    //vertex.y += Mathf.Sin(Time.time * speed + baseHeight[i].x + baseHeight[i].y + baseHeight[i].z) * scale;
            //    float distance1 = Mathf.Sqrt(Mathf.Pow(baseHeight[i].x - eventPos.x, 2) + Mathf.Pow(baseHeight[i].z - eventPos.y, 2));
            //    //   vertex.y += (Mathf.Sin(Time.time * -speed + 2f * distance1 + baseHeight[i].y * 0 ) * scale) *1 / (distance1 + 1f);

            //    //vertex.y += (Mathf.Sin((Time.time-eventTime) * -speed + 2f * (distance1- (Time.time - eventTime)) + baseHeight[i].y * 0) * scale) * 1 / (distance1 + 1f);
            //    float wavecentre = Mathf.Abs(((Time.time - eventTime) * speed) - distance1);
            //    vertex.y += Mathf.Cos(wavecentre * 3.14f) * (((wavecentre < 1) ? (1f - wavecentre) : 0) - 0) * waveSize;

            //    float distance2 = Mathf.Sqrt(Mathf.Pow(baseHeight[i].x - srcx2, 2) + Mathf.Pow(baseHeight[i].z - srcy2, 2));
            //  vertex.y += Mathf.Sin(Time.time * -speed + 3f*distance2 + baseHeight[i].y * 0) * scale *1/ (distance2 + 1f);

            vertex.y += Mathf.Cos(-3*baseHeight[i].x + 3* baseHeight[i].z + 4*Time.time ) *0.3f;

                float distance1 = Mathf.Sqrt(Mathf.Pow(baseHeight[i].x - eventPos1.x, 2) + Mathf.Pow(baseHeight[i].z - eventPos1.y, 2));
                float wavecentre = Mathf.Abs(wavePosEvent1 - distance1);
                vertex.y += Mathf.Cos(wavecentre * 3.14f) * Mathf.Max((1f - wavecentre),0)  * waveSize;

                 distance1 = Mathf.Sqrt( Mathf.Pow(baseHeight[i].x - eventPos2.x, 2) + Mathf.Pow(baseHeight[i].z - eventPos2.y, 2));
                 wavecentre = Mathf.Abs(wavePosEvent2 - distance1) ;
                vertex.y += Mathf.Cos(wavecentre * 3.14f) * Mathf.Max((1f - wavecentre), 0) * waveSize;

            distance1 = Mathf.Sqrt(Mathf.Pow(baseHeight[i].x - eventPos3.x, 2) + Mathf.Pow(baseHeight[i].z - eventPos3.y, 2));
            wavecentre = Mathf.Abs(wavePosEvent3 - distance1);
            vertex.y += Mathf.Cos(wavecentre * 3.14f) * Mathf.Max((1f - wavecentre), 0) * waveSize;

            distance1 = Mathf.Sqrt(Mathf.Pow(baseHeight[i].x - eventPos4.x, 2) + Mathf.Pow(baseHeight[i].z - eventPos4.y, 2));
            wavecentre = Mathf.Abs(wavePosEvent4 - distance1);
            vertex.y += Mathf.Cos(wavecentre * 3.14f) * Mathf.Max((1f - wavecentre), 0) * waveSize;

            distance1 = Mathf.Sqrt(Mathf.Pow(baseHeight[i].x - eventPos5.x, 2) + Mathf.Pow(baseHeight[i].z - eventPos5.y, 2));
            wavecentre = Mathf.Abs(wavePosEvent5 - distance1);
            vertex.y += Mathf.Cos(wavecentre * 3.14f) * (((wavecentre < 1) ? (1f - wavecentre) : 0) - 0) * waveSize;

            distance1 = Mathf.Sqrt(Mathf.Pow(baseHeight[i].x - eventPos6.x, 2) + Mathf.Pow(baseHeight[i].z - eventPos6.y, 2));
            wavecentre = Mathf.Abs(wavePosEvent6 - distance1);
            vertex.y += Mathf.Cos(wavecentre * 3.14f) * (((wavecentre < 1) ? (1f - wavecentre) : 0) - 0) * waveSize;

            distance1 = Mathf.Sqrt(Mathf.Pow(baseHeight[i].x - eventPos7.x, 2) + Mathf.Pow(baseHeight[i].z - eventPos7.y, 2));
            wavecentre = Mathf.Abs(wavePosEvent7 - distance1);
            vertex.y += Mathf.Cos(wavecentre * 3.14f) * (((wavecentre < 1) ? (1f - wavecentre) : 0) - 0) * waveSize;



            vertices[i] = vertex;
            //}
        }
        mesh.vertices = vertices;
        mesh.RecalculateNormals();
        if (player1Wave)
        {


        
            eventTimes.Add(Time.time);
            Vector3 a;// = new Vector3();// eventPlaces[eventIndexer];
            a.x = srcx1;
            a.y = srcy1;
            a.z = 0;
            eventPlaces.Add(a);
            if (eventPlaces.Count>7)
            eventPlaces.RemoveAt(0);
            if (eventTimes.Count > 7)
                eventTimes.RemoveAt(0);
        }


        if (player2Wave)
        {



            eventTimes.Add(Time.time);
            Vector3 a;// = new Vector3();// eventPlaces[eventIndexer];
            a.x = srcx2;
            a.y = srcy2;
            a.z = 0;
            eventPlaces.Add(a);
            if (eventPlaces.Count > 7)
                eventPlaces.RemoveAt(0);
            if (eventTimes.Count > 7)
                eventTimes.RemoveAt(0);
        }

        Vector3 z = new Vector3(mother_blob222.transform.position.x, mother_blob222.transform.position.y, mother_blob222.transform.position.z);
      //  force = waveForce(z);
      //  mother_blob222.transform.position = mother_blob222.transform.position + 1000 * force * Time.deltaTime;
        //print(force);
     
    }


   public Vector3 waveForce(Vector3 position)
    {
        for (int i= 0;i< eventTimes.Count; i++) {
            //Vector3 z = new Vector3(mother_blob2.position);
            eventPos = eventPlaces[i];
            eventTime = eventTimes[i];

            Vector3 force; //= position;
            float distance1 = Mathf.Sqrt(Mathf.Pow(position.x - eventPos.x, 2) + Mathf.Pow(position.z - eventPos.y, 2));
            float wavecentre = Mathf.Abs(((Time.time - eventTime) * speed) - distance1);
            float forceAmplitude = Mathf.Cos(wavecentre * 3.14f) * (((wavecentre < 1) ? (1f - wavecentre) : 0) - 0) * waveSize;
            force.x = position.x - eventPos.x;
            force.y = 0;
            force.z = position.z - eventPos.y;
            force = Vector3.Normalize(force);
            force = force * forceAmplitude;
        }

        return force;
    }
}

