using MySql.Data.MySqlClient;

namespace Server
{
    class DB_Connection
    {
        string server;
        string db;
        string user;
        string pass;
        MySqlConnection conn;

        public DB_Connection()
        {
            initialize();
        }

        public void initialize()
        {
            server = "localhost";
            db = "smart_home";
            user = "root";
            pass = "root1234";
            string connectionString = "DataSource=" + server + ";Database=" + db + ";User Id=" + user + ";Password=" + pass + ";SSL Mode=0";
            conn = new MySqlConnection(connectionString);
        }
        public MySqlConnection get_connection()
        {
            return this.conn;
        }
       
    }
}
