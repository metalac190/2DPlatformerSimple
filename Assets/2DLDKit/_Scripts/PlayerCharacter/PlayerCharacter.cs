using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    [field: SerializeField] public Inventory Inventory { get; private set; } = null;
    [field: SerializeField] public Health Health { get; private set; } = null;
}
