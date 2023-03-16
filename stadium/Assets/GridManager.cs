using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public float x_Start, y_Start;
    public int colLen, rowLen;
    public float x_Space, y_Space;
    public GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < colLen * rowLen; i++)
        {
            Instantiate(prefab, new Vector3(x_Start + (x_Space * (i % colLen)), y_Start + (y_Space * (i / colLen))), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
