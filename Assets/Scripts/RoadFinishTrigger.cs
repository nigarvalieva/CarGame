using UnityEngine;

public class RoadFinishTrigger : MonoBehaviour
{
    public Vector3 FinalPosition;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car")) { 
            other.GetComponent<CarController>().FinalPosition = FinalPosition;
        }
    }
}
