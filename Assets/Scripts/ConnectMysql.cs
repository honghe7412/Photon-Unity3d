using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;

public class ConnectMysql : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string connectStr = "server = localhost;port = 3306;database = test;user = root;password = 123456;";
        MySqlConnection conn = new MySqlConnection(connectStr);

        try
        {
            conn.Open();
            string sql = "select * from runoob_tbL";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader read = cmd.ExecuteReader();
            read.Read();
            //read.Read();
            Debug.Log("连接成功"+ read[0].ToString()+ read[1].ToString()+ read[2].ToString()+ read[3].ToString());
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
        }
        finally
        {
            conn.Close();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
