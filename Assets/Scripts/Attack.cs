using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{ 
    public void Fire(Health characterHealth)
    {
        characterHealth.Damage(1);
    }
}
