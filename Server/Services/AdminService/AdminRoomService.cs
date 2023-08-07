using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RemoteInerfaces.Entities;
using RemoteInerfaces.Services.AdminServices;

namespace Server.Services.AdminService
{
    class AdminRoomService : MarshalByRefObject, IAdminRoomService
    {
        DB_Connection connection;
       

        public AdminRoomService()
        {
            connection = new DB_Connection();
        }

        public void createNewRoom(Room room, List<RoomAccessors> roomAccessors)
        {
            using (IDbConnection cnn = connection.get_connection())
            {
 
                cnn.ExecuteScalar($"INSERT INTO room (id_house, descriptive_name, background_image) VALUES (@id_house, @descriptive_name, @background_image)", room);
                int room_id = cnn.ExecuteScalar<int>("SELECT LAST_INSERT_ID()");
                Console.WriteLine(room_id);
                foreach(RoomAccessors roomAccessor in roomAccessors)
                {
                    roomAccessor.id_room = room_id;
                    cnn.ExecuteScalar($"INSERT INTO room_accessors (id_room, id_account) VALUES (@id_room, @id_account)", roomAccessor);
                }
                Console.WriteLine("room created with success");
            }
        }

        public List<RoomWithDevieces> getAdminRooms(int id_house)
        {
            using (IDbConnection cnn = connection.get_connection())
            {
                List<Room> rooms = cnn.Query<Room>($"SELECT * FROM room WHERE id_house = '{id_house}'").ToList();
                List<RoomWithDevieces> roomsWithDevices = new List<RoomWithDevieces>();
                foreach(Room room in rooms)
                {
                    RoomWithDevieces roomWithDevieces = new RoomWithDevieces();
                    roomWithDevieces.room = room;
                    List<Device> devices = cnn.Query<Device>($"SELECT * FROM device WHERE id_room = '{room.id_room}'").ToList();
                    roomWithDevieces.room_devices = devices;
                    roomsWithDevices.Add(roomWithDevieces);
                }

                return roomsWithDevices;
            }
        }

        public List<RoomWithDevieces> getResidentRooms(int id_account)
        {
            using (IDbConnection cnn = connection.get_connection())
            {
                List<int> account_rooms_id = cnn.Query<int>($"SELECT id_room FROM room_accessors WHERE id_account = '{id_account}'").ToList();

                List<RoomWithDevieces> roomsWithDevices = new List<RoomWithDevieces>();
                foreach (int id_room in account_rooms_id)
                {
                    Room room = cnn.Query<Room>($"SELECT * FROM room WHERE id_room = '{id_room}'").FirstOrDefault();

                    List<Device> devices = cnn.Query<Device>($"SELECT * FROM device WHERE id_room = '{id_room}'").ToList();
                    RoomWithDevieces roomWithDevieces = new RoomWithDevieces();
                    roomWithDevieces.room = room;
                    roomWithDevieces.room_devices = devices;
                    roomsWithDevices.Add(roomWithDevieces);
                }

                return roomsWithDevices;
                
            }
        }
    }
}
