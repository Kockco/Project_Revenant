using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockRotation : MonoBehaviour
{
    public bool isTarget;
    public Transform target;
    Quaternion rot;
    Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        rot = transform.localRotation;
        pos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(isTarget)
            transform.position = new Vector3(transform.position.x,target.position.y,transform.position.z);
        else
            transform.position = new Vector3(transform.position.x, pos.y, transform.position.z);

        //transform.rotation = Quaternion.Euler(transform.parent.rotation.x, transform.parent.rotation.y, transform.parent.rotation.z);
    }
}
