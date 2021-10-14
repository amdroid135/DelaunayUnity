using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Unity.Collections;
using Unity.Mathematics;

public class WriteFileBinary
{
    const string fileName = "PointList.dat";
    
    [HideInInspector]
    public List<Vector3> points = new List<Vector3>();
    public NativeList<float3> float3s = new NativeList<float3>();
    
    public BinaryWriter writer;

    public void OpenWriter(string fileName = "PointList.dat")
    {
        writer = new BinaryWriter(File.Open(fileName, FileMode.Create));
    }

    public void CloseWriter()
    {
        writer.Close();
        writer.Dispose();
    }

    public void WriteFile(List<float> _points)
    {
        foreach (float t in _points)
        {
            writer.Write(t);
        }
    }

    public void WriteFile(List<int> _points)
    {
        foreach (int t in _points)
        {
            writer.Write(t);
        }
    }

    public void WriteFile(List<byte> _points)
    {
        foreach (byte t in _points)
        {
            writer.Write(t);
        }
    }

    public void WriteFile(List<Vector3> _points)
    {
        points = _points;

        foreach (Vector3 v in points)
        {
            writer.Write(v.x);
            writer.Write(v.y);
            writer.Write(v.z);
        }
    }

    public void WriteFile(List<Vector3> _points, string fileName = "PointList.dat")
    {
        points = _points;
        using (BinaryWriter writer = new BinaryWriter(File.Open(fileName, FileMode.Create)))
        {
            foreach(Vector3 v in points)
            {
                writer.Write(v.x);
                writer.Write(v.y);
                writer.Write(v.z);
            }
        }
    }

    BinaryReader reader;

    public void OpenReader(string fileName = "PointList.dat")
    {
        if (File.Exists(fileName))
        {
            reader = new BinaryReader(File.Open(fileName, FileMode.Open));
        }
    }

    public void CloseReader()
    {
        reader.Close();
        reader.Dispose();
    }

    private int length = 0;

    public void LoadFile(out int length)
    {
        length = reader.ReadInt32();
        this.length = length;
    }

    public List<Vector3> LoadVector3(int step = 1)
    {
        List<Vector3> points = new List<Vector3>(100_000_000);
        List<Vector3> pointsBin = new List<Vector3>(100_000_000);

        for (int i = 0; i < length; i++)
        {
            Vector3 v = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
            pointsBin.Add(v);
        }

        for (int i = 0; i < pointsBin.Count; i += step)
        {
            points.Add(pointsBin[i]);
        }

        return points;
    }

    public void LoadFile(out List<Vector3> points)
    {
        points = new List<Vector3>();

        for (int i = 0; i < length; i++)
        {
            points.Add(new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle()));
        }
    }

    public void LoadFile(out List<float> points)
    {
        points = new List<float>();

        for (int i = 0; i < length; i++)
        {
            points.Add(reader.ReadSingle());
        }
    }

    public void LoadFile(out List<int> points)
    {
        points = new List<int>();

        for (int i = 0; i < length; i++)
        {
            points.Add(reader.ReadInt32());
        }
    }

    public void LoadFile(out List<byte> points)
    {
        points = new List<byte>();

        for (int i = 0; i < length; i++)
        {
            points.Add(reader.ReadByte());
        }
    }

    public void LoadFiles(string fileName = "PointList.dat", int step = 1)
    {
        points.Clear();
        points = new List<Vector3>(100_000_000);
        List<Vector3> pointsBin = new List<Vector3>(100_000_000);

        if (File.Exists(fileName))
        {
            using (BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open)))
            {
                try
                {
                    while(true)
                    {
                        var v = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                        pointsBin.Add(v);
                    }
                }
                catch(Exception e)
                {

                }
            }

            for (int i = 0; i < pointsBin.Count; i += step)
            {
                points.Add(pointsBin[i]);
            }
        }
    }
}
