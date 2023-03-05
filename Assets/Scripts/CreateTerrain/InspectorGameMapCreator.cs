using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(GameMapCreator))]
public class InspectorGameMapCreator : Editor
{
    bool buttonPressedYellow = false;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GameMapCreator creator = (GameMapCreator)target;
        CreateTemplate(creator);

        if (GUILayout.Button("Clear Map Info"))
        {
            ClearMap(creator);
        }
        if (GUILayout.Button("Build Terrain"))
        {
            creator.SpawnTerrain();
        }
        if (GUILayout.Button("Show writed info"))
        {
            creator.ShowDictionary();
        }
    }

    private void CreateTemplate(GameMapCreator creator)
    {
        int count = 0;
        GUILayout.BeginHorizontal("box");
        for (int i = 0; i < creator.verticalCount; i++)
        {
            GUILayout.BeginVertical("box");
            for (int j = 0; j < creator.horizontalCount; j++)
            {
                var previousColor = GUI.color;

                creator.fields.TryGetValue(count, out bool stateBtn);
                if (stateBtn)
                    GUI.color = Color.green;


                if (GUILayout.Button(count.ToString(), GUILayout.Width(30), GUILayout.Height(30)))
                {
                    if (creator.fields.ContainsKey(count) && stateBtn == false)
                    {
                        creator.fields[count] = !stateBtn;
                    }
                    else if (creator.fields.ContainsKey(count) && stateBtn == true)
                    {
                        creator.fields[count] = !stateBtn;
                    }
                    else
                        creator.fields.Add(count, true);
                    
                }
            
                count++;
                GUI.color = previousColor;
            }
            GUILayout.EndVertical();
        }
        GUILayout.EndHorizontal();

    }

    private void ClearMap(GameMapCreator creator)
    {
        Debug.Log("Clear Map");
        creator.fields.Clear();
    }
}
#endif

