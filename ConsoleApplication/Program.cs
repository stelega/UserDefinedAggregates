using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;


namespace ConsoleApplication
{
    class Program
    {

        static private SqlConnection connection;

        static void OpenConnection() 
        {
            string sqlconnection = "DATA SOURCE=MSSQLServer26;" + "INITIAL CATALOG=Project; INTEGRATED SECURITY=SSPI;";
            connection = new SqlConnection(sqlconnection);
            connection.Open();
        }

        static void PrintAggOptions() 
        {
            Console.WriteLine(
                @"
                     _______________________________________
                    |                                       |
                    |                                       |
                    | Prosze wybrac agregat ktory ma zostac |
                    | uzyty (nalezy wpisac cyfre):          |
                    |   0. Przerwij                         |
                    |   1. Mediana                          |
                    |   2. Srednia geometryczna             |
                    |   3. Srednia kwadratowa               |
                    |   4. Srednia ucinana                  |
                    |   5. Dominanta                        |
                    |   6. Srednia arytmetyczna             |
                    |                                       |
                    | Funkcja agregujaca 'sr arytmetyczna'  |
                    | nie zostala zdefiniowana przeze mnie, |
                    | pojawila sie tu w celu porownania     |
                    | wynikow z innych funkcji agregujacych.|
                    |_______________________________________|
                    
                ");
        }

        static void PrintDataOptions()
        {
            Console.WriteLine(
                @"
                     _______________________________________
                    |                                       |
                    |                                       |
                    | Teraz prosze wybrac atrybut na ktorym |
                    | ma zostac uzyty agregat(nalezy wpisac |
                    | cyfre):                               |
                    |   0. Powrot do wyboru agregatu        |
                    |   1. Temperatura                      |
                    |   2. Opady deszczu                    |
                    |                                       |
                    |_______________________________________|
                     
                    
                ");
        }


        static void PrintResult(int AggOption, int DataOption)
        {
            string Agg = "";
            string Data = "";
            string Description1 = "";
            string Description2 = "";
            string Description3 = "";
            switch (AggOption)
            {
                case 1:
                    Agg = "Median";
                    Description1 = "Mediana";
                    break;
                case 2:
                    Agg = "GeoMean";
                    Description1 = "Srednia geometryczna";
                    break;
                case 3:
                    Agg = "RMS";
                    Description1 = "Srednia kwadratowa";
                    break;
                case 4:
                    Agg = "TruncatedMean";
                    Description1 = "Srednia ucinana";
                    break;
                case 5:
                    Agg = "Mode";
                    Description1 = "Dominanta";
                    break;
                case 6:
                    Agg = "AVG";
                    Description1 = "Srednia arytmetyczna";
                    break;
                default:
                    break;
            }
            switch (DataOption)
            {
                case 1:
                    Data = "Temperature";
                    Description2 = "temperatury";
                    Description3 = " stopni Celciusza";
                    break;
                case 2:
                    Data = "Rainfall";
                    Description2 = "opadow deszczu";
                    Description3 = "mm na metr kwadratowy";
                    break;
                default:
                    break;
            }
            OpenConnection();
            try
            {
                SqlCommand command = new SqlCommand("SELECT dbo." + Agg + "(" + Data + ") FROM [Project].[dbo].[Cracow]", connection);
                SqlDataReader datareader = command.ExecuteReader();
                datareader.Read();
                Console.WriteLine(Description1 + " z " + Description2 + " w Krakowie w 2019r. wyniosla: ");
                Console.WriteLine(datareader[0] + Description3);
            }
            finally
            {
                connection.Close();
            }

        }

        static void Main(string[] args)
        {
            Console.WriteLine(
                @"
                     _______________________________________
                    |                                       |
                    |                                       |
                    |        Szymon Telega - Projekt        |
                    |                                       |
                    |                                       |
                    | Projekt przedstawia stworzone przeze  |
                    | mnie CLR-UDA. W bazie danych znajduje |
                    | sie 5 funkcji agregujacych:           |
                    |   1. Mediana                          |
                    |   2. Srednia geometryczna             |
                    |   3. Srednia kwadratowa               |
                    |   4. Srednia ucinana                  |
                    |   5. Dominanta                        |
                    |                                       |
                    | W bazie znajduje sie rowniez tabela   |
                    | zawierajaca dane dotyczace pogody w   |
                    | Krakowie w 2019 roku. Sklada sie ona  |
                    | z nastepujacych atrybutow:            |
                    |   1. Dzien                            |
                    |   2. Temperatura (w danym dniu)       |
                    |   3. Opady deszczu (w danym dniu)     |
                    |                                       |
                    |_______________________________________|  
                
                ");

            int AggOption;
            int DataOption;
            while (true)
            {
                PrintAggOptions();
                string input = Console.ReadLine();
                if (input != "0" && input != "1" && input != "2" && input != "3" && input != "4" && input != "5" && input != "6")
                {
                    Console.WriteLine("Prosze wybrac cyfre z zakresu 0-6");
                    continue;
                }
                else if (input == "0")
                {
                    Console.WriteLine("Zamykanie aplikacji...");
                    break;
                }
                else
                {
                    AggOption = int.Parse(input);
                    while (true)
                    {
                        PrintDataOptions();
                        string input2 = Console.ReadLine();
                        if (input2 != "0" && input2 != "1" && input2 != "2")
                        {
                            Console.WriteLine("Prosze wybrac cyfre z zakresu 0-2");
                            continue;
                        }
                        else if (input2 == "0")
                        {
                            break;
                        }
                        else
                        {
                            DataOption = int.Parse(input2);
                            Console.WriteLine(AggOption + " " + DataOption);
                            PrintResult(AggOption, DataOption);
                            break;
                        }
                    }
                }
            }
        }
    }
}
