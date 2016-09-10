using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using akizuki;

namespace teruzuki
{
    public class TwitterManager : MonoBehaviour
    {
        void Start()
        {
            Akizuki akizuki = new Akizuki();
            Debug.Log(Akizuki.Test());
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
