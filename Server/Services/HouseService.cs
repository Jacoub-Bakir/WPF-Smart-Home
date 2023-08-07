using Dapper;
using RemoteInerfaces.Entities;
using RemoteInerfaces.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class HouseService : MarshalByRefObject, IHouseService
    {
        DB_Connection connection;

        public HouseService()
        {
            connection = new DB_Connection();
        }

        public void createHouse(House house)
        {
            using (IDbConnection cnn = connection.get_connection())
            {
                cnn.ExecuteScalar("INSERT INTO house  (owner_first_name, owner_last_name, type, id_admin, descriptive_name, address, house_key) VALUES (@id, @owner_first_name, @owner_last_name, @type, @id_admin, @descriptive_name, @address, @house_key)", house);
                Console.WriteLine("house saved with success");
            }
        }

        public void editHouse(House house)
        {
            using (IDbConnection cnn = connection.get_connection())
            {
                cnn.ExecuteScalar("UPDATE house SET owner_first_name = @owner_first_name, owner_last_name = @owner_last_name, type = @type, id_admin = @id_admin, descriptive_name = @descriptive_name, address = @address WHERE (id = @id)", house);
                Console.WriteLine("house edited with success");
            }
        }

        public void deleteHouse(int id)
        {
            using (IDbConnection cnn = connection.get_connection())
            {
                cnn.ExecuteScalar($"DELETE FROM house WHERE id = '{id}'");
                Console.WriteLine("house deleted with success");
            }
        }

        public House getHouse(int id)
        {
            using (IDbConnection cnn = connection.get_connection())
            {
                House house= cnn.Query<House>($"SELECT * FROM house WHERE id = '{id}'").FirstOrDefault();
                return house;
            }
        }

    }
}
