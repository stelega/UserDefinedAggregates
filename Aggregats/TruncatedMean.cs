using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Collections.Generic;
using System.Runtime.InteropServices;


[Serializable]
[Microsoft.SqlServer.Server.SqlUserDefinedAggregate(Format.UserDefined, IsNullIfEmpty = true, MaxByteSize = 8000)]
[StructLayout(LayoutKind.Sequential)]
public struct TruncatedMean : IBinarySerialize
{
    public void Init()
    {
        list = new List<double>();
    }

    public void Accumulate(SqlDouble Value)
    {
        if (Value.IsNull)
            return;
        double tmp = double.Parse(Value.ToString());
        list.Add(tmp);
    }

    public void Merge(TruncatedMean Group)
    {
        // Put your code here
    }

    public SqlDouble Terminate()
    {
        list.Sort();
        int percent = list.Count / 10;
        double sum = 0;
        int counter = 0;
        for (int i = percent; i < list.Count-percent; i++) 
        {
            sum += list[i];
            counter++;
        }


        return new SqlDouble(sum/counter);
    }

    public void Read(System.IO.BinaryReader r)
    {
        this.list = new List<double>();
        int j = r.ReadInt32();
        for (int i = 0; i < j; i++)
            this.list.Add(r.ReadDouble());
    }

    public void Write(System.IO.BinaryWriter w)
    {
        w.Write(this.list.Count);
        foreach (double d in this.list)
            w.Write(d);
    }



    private List<double> list;
}
