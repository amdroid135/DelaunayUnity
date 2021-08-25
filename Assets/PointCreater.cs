using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

//[RequireComponent(typeof(MeshRenderer))]
//[RequireComponent(typeof(MeshFilter))]
public class PointCreater : MonoBehaviour
{
    //MeshFilter meshFilter;
    public int jumpIndex = 10;
    public int startIndex = 0;

    public List<Vector3> verties;

    void Awake()
    {
        //meshFilter = GetComponent<MeshFilter>();

        var directory = new DirectoryInfo(Application.streamingAssetsPath);
        var myFile = directory.GetFiles("*.csv")
                     .OrderByDescending(f => f.LastWriteTime).First();

        string textValue = File.ReadAllText(myFile.FullName);

        string[] lines = textValue.Split('\n');
        verties = new List<Vector3>(lines.Length);
        List<int> indexes = new List<int>(lines.Length);
        for (int i = startIndex, k = 0; i < lines.Length; i += jumpIndex)
        {
            string[] values = lines[i].Split(',');
            if (values.Length != 3)
                continue;
            if (float.Parse(values[1]) == 0f)
            {
                Debug.Log(verties.Count - 1 + " : " + values[1]);
                continue;
            }
            for (int x = 0; x < 226; x++)
            {
                verties.Add(new Vector3(float.Parse(values[0]),
                    float.Parse(values[1]), float.Parse(values[2])));
                indexes.Add(k++);
            }
            Debug.Log(verties.Count);
            Debug.Break();
        }

        GameObject.Find("Terrain Circle").GetComponent<DelaunayTerrain>().start = true;

        //meshFilter.mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        //meshFilter.mesh.SetVertices(verties);
        //meshFilter.mesh.SetIndices(indexes, MeshTopology.Points, 0);
    }
}
