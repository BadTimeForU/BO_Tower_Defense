using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public static Transform[] points;   
    public static Transform endWaypoint; 

    void Awake()
    {
        
        points = new Transform[transform.childCount];
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);
        }

        
        GameObject endObj = GameObject.FindGameObjectWithTag("EndWaypoint");
        if (endObj != null)
        {
            endWaypoint = endObj.transform;
        }
        else
        {
            Debug.LogWarning("⚠️ Geen GameObject gevonden met tag 'EndWaypoint'! Voeg dit toe aan je laatste waypoint.");
        }
    }
}