using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Runtime.InteropServices;
using Microsoft.SqlServer.Server;
using System.Collections.Generic;


[Serializable]
[Microsoft.SqlServer.Server.SqlUserDefinedAggregate(Format.UserDefined,IsNullIfEmpty = true,MaxByteSize = 8000)]
[StructLayout(LayoutKind.Sequential)]
public struct Mode : IBinarySerialize
{
    public void Init()
    {
        arr = new List<double>();
    }

    public void Accumulate(SqlDouble Value)
    {
        if (Value.IsNull)
            return;
        double tmp = double.Parse(Value.ToString());
        arr.Add(tmp);
    }

    public void Merge(Mode Group)
    {
        // Put your code here
    }

    public SqlDouble Terminate()
    {
        arr.Sort();
        size = 1;
        dom = arr[0];
        int tmp = 1;
        for (int i = 1; i < arr.Count; i++)
        {

            if (arr[i] == arr[i - 1])
            {
                tmp++;
            }
            else
            {
                if (tmp > size)
                {
                    size = tmp;
                    dom = arr[i - 1];
                }
                tmp = 1;
            }
        }

        return new SqlDouble(dom);
    }

    #region IBinarySerialize Members
    // Własna metoda do wczytywania serializowanych danych.
    public void Read(System.IO.BinaryReader r)
    {
        this.arr = new List<double>();
        int j = r.ReadInt32();
        for (int i = 0; i < j; i++)
        {
            this.arr.Add(r.ReadDouble());
        }
    }
    
    // Własna metoda do zapisywania serializowanych danych.
    public void Write(System.IO.BinaryWriter w)
    {
        w.Write(this.arr.Count);
        foreach (double d in this.arr)
        {
            w.Write(d);
        }
    }
    #endregion

    // This is a place-holder member field
    private List<double> arr;
    private double dom;
    private int size;

}
