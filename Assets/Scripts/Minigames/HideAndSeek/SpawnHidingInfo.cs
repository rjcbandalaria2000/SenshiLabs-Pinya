using System;
using UnityEngine;

[Serializable]
public class SpawnHidingInfo {

    [field: SerializeField] public GameObject HidingObject { get; set; }

    [field: SerializeField] public HidingSpot HidingSpot { get; set; }
}
