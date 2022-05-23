using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;
using UnityEngine.SceneManagement;

public class SaveResultDB : MonoBehaviour
{
    // Start is called before the first frame update
    public string connString = @"";
    public Timer globalTimer;
    SqlConnection connection;
    public void getConnection()
    {
        try
        {
            connection = new SqlConnection(connString);
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            string sql = "Insert into Table1 (login, lvl, seconds) "
                                                 + " values (@login, @lvl, @seconds) ";
            cmd.CommandText = sql;

            cmd.Parameters.Add("@login", SqlDbType.Text).Value = PlayerPrefs.GetString("login");
            cmd.Parameters.Add("@lvl", SqlDbType.Int).Value = SceneManager.GetActiveScene().buildIndex;
            cmd.Parameters.Add("@seconds", SqlDbType.Float).Value = globalTimer.endTime;
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e);
            Console.WriteLine(e.StackTrace);
        }
        finally
        {
            connection.Close();
            connection.Dispose();
            connection = null;
        }
    }
    
}
