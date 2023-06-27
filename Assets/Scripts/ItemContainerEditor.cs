using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CustomEditor(typeof(ItemContainer))]
public class ItemContainerEditor : Editor
{
   public override void OnInspectorGUI()
{
   ItemContainer container = target as ItemContainer;
   if (GUILayout.Button("Clear container"))
   {
    for (int i = 0 ; i < container.slots.Count; i++)
    {
        container.slots[i].Clear();
    }

   }
   DrawDefaultInspector();

  }
}
