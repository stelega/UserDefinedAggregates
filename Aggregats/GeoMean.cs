using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;


[Serializable]
[Microsoft.SqlServer.Server.SqlUserDefinedAggregate(Format.Native)]
public struct GeoMean
{
    public void Init()
    {
        sum = 1;
        counter = 0;
    }

    public void Accumulate(SqlDouble Value)
    {
        sum *= double.Parse(Value.ToString());
        counter++;
    }

    public void Merge(GeoMean Group)
    {
        // Put your code here
    }

    public SqlDouble Terminate()
    {
        double index = 1/(double)counter;
        sum = Math.Pow(sum, index);

        return new SqlDouble(sum);
    }

    // This is a place-holder member field
    private double sum;
    private int counter;

}
