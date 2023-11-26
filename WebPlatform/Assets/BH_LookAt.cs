using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BH_LookAt : MonoBehaviour
{
    public GameObject mLookObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(mLookObject.transform.position);
    }
}
