using UnityEngine;
using Com.LuisPedroFonseca.ProCamera2D;

public class Looker : BasePC2D
{
    [SerializeField] private float ViewDistance = 2f;

    void Update()
    {
        if (GetComponent<ControlsReader>().MovementValue.y < 0)
        {
            ProCamera2D.ApplyInfluence(new Vector2(0, -ViewDistance));
        }
    }
}