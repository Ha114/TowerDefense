using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameMapCreator : MonoBehaviour
{
    [SerializeField] public GameObject prefabGrass;
    [SerializeField] public GameObject prefabPath;
    [SerializeField] public GameObject ground;
    [SerializeField] public int verticalCount = 5;
    [SerializeField] public int horizontalCount = 5;
    
    public Dictionary<int, bool> fields = new Dictionary<int, bool>();

    public Transform parentGrass;
    public Transform parentPath;


    public void ShowDictionary()
    {
        foreach (KeyValuePair<int, bool> f in fields)
        {
            Debug.Log("Count = " + f.Key + ", value = " + f.Value);     
        }
    }

    public void SpawnTerrain()
    {
        int count = 0;
        Vector3 setNewPos = Vector3.zero;

        for (int i = 0; i < verticalCount; i++)
        {
            for (int j = 0; j < horizontalCount; j++)
            {
                if (fields.TryGetValue(count, out bool state))
                {
                    if (state)
                        InstantiateField(prefabPath, parentPath, -0.32f).GetComponent<PathField>().id = count;
                    else
                    {
                        InstantiateField(prefabGrass, parentGrass).GetComponent<GrassField>().id = count;
                    }
                }
                else
                {
                    InstantiateField(prefabGrass, parentGrass).GetComponent<GrassField>().id = count;
                }

                setNewPos.z -= (float)1.11;
                count++;

            }
            setNewPos.x += (float)1.11;
            setNewPos.z = 0;
        }


        GameObject InstantiateField(GameObject prefabField, Transform parent, float yPos = 0)
        {
            setNewPos.y = yPos;
            GameObject go = Instantiate(prefabField, setNewPos, Quaternion.identity);
            go.transform.parent = parent.transform;
            return go;
        }

        SetGround();
    }

    private void SetGround()
    {
        Vector3 averagePosition = Vector3.zero;
        Transform[] allChildrenGrass = parentGrass.GetComponentsInChildren<Transform>();
        Transform[] allChildrenPath = parentPath.GetComponentsInChildren<Transform>();

        foreach (Transform child in allChildrenGrass)
        {
            if (parentGrass != child)
                averagePosition += child.localPosition;
        }
        foreach (Transform child in allChildrenPath)
        {
            if (parentGrass != child)
                averagePosition += child.localPosition;
        }

        averagePosition /= (horizontalCount * verticalCount);
        ground.transform.position = new Vector3(averagePosition.x, -0.806f, averagePosition.z);
        ground.transform.localScale = new Vector3(verticalCount + 1f, 0.6f, horizontalCount + 1f);

    }
}
