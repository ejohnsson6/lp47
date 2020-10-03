using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{

    [SerializeField] private float ttd;

    private float count = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        count += Time.deltaTime;

        if (count > ttd)
        {
            Object.Destroy(transform.gameObject);
        }
    }
}
