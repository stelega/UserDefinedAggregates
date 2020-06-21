using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;


[Serializable]
[Microsoft.SqlServer.Server.SqlUserDefinedAggregate(Format.Native)]
public struct RMS
{
    public void Init()
    {
        sum = 0;
        counter = 0;
    }

    public void Accumulate(SqlDouble Value)
    {
        if (Value.IsNull)
            return;
        double tmp = double.Parse(Value.ToString());
        sum += tmp * tmp;
        counter++;
    }

    public void Merge(RMS Group)
    {
        // Put your code here
    }

    public SqlDouble Terminate()
    {
        double tmp = (double)(sum / counter);
        double res = Math.Sqrt(tmp);
        return new SqlDouble(res);
    }

    // This is a place-holder member field
    private double sum;
    private long counter;

}
