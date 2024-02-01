using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventAdder : MonoBehaviour
{
    /// <summary>
    /// Add an event to the target object.
    /// The event name should be the class name.
    /// The event will be added only if the class name is found and the event is not added.
    /// </summary>
    public static void AddEvent(GameObject target, string eventName) {
        // If the class name is not found, return
        if (System.Type.GetType(eventName) == null) return;
        // If the event is already added, return
        if (target.GetComponent(eventName) != null) return;
        // Add the event to the target
        target.AddComponent(System.Type.GetType(eventName));
    }


    public static void AddEvent(GameObject target, string eventName, string[] paramNames, object[] paramValues) {
        // If the class name is not found, return
        if (System.Type.GetType(eventName) == null) return;
        // If the event is already added, return
        if (target.GetComponent(eventName) != null) return;
        // Add the event to the target
        target.AddComponent(System.Type.GetType(eventName));
        // Set the parameters
        for (int i = 0; i < paramNames.Length; i++) {
            target.GetComponent(eventName).GetType().GetField(paramNames[i]).SetValue(target.GetComponent(eventName), paramValues[i]);
        }
    }
}
