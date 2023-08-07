using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RemoteInerfaces;
using RemoteInerfaces.Services.DoorAppServices;
using RemoteInerfaces.Entities;
using System.Collections.ObjectModel;

namespace Server.Services
{
    class DoorRequestService : MarshalByRefObject, IDoorRequestService
    {
        DB_Connection connection;
        DoorRequest lastRequest = null;
        DoorRequestsInterface doorRequestsService;
        public DoorRequestService()
        {
            doorRequestsService = (DoorRequestsInterface)Activator.GetObject(typeof(DoorRequestsInterface), "tcp://localhost:8092/RequestDoorService");
            connection = new DB_Connection();
        }

        public void saveDoorRequest(DoorRequest request)
        {
            using (IDbConnection cnn = connection.get_connection())
            {
                cnn.ExecuteScalar($"INSERT INTO door_requests_history (house_id, full_name, face_image, request_time, request_message) VALUES (@house_id, @full_name, @face_image, @request_time, @request_message)", request);
                int request_id = cnn.ExecuteScalar<int>("SELECT LAST_INSERT_ID()");
                lastRequest = request;
                lastRequest.id = request_id;
                Console.WriteLine("door request saved with success");
            }
        }

        public void respondToDoorRequest(int responce_by, Boolean request_responce, String responce_message)
        {
            using (IDbConnection cnn = connection.get_connection())
            {
                if(lastRequest != null)
                {
                    Console.WriteLine(lastRequest.id);
                    cnn.ExecuteScalar($"UPDATE door_requests_history SET request_responce = {request_responce}, responce_by = '{responce_by}', responce_message = '{responce_message}' WHERE id = '{lastRequest.id}'");

                    doorRequestsService.respondToRequest(lastRequest.id, responce_by, request_responce, responce_message);
                    lastRequest = null;
                    Console.WriteLine("request responce saved with success");
                }
                
            }
        }

        public void cancelDoorRequest()
        {
            using (IDbConnection cnn = connection.get_connection())
            {
                if (lastRequest != null)
                { 
                    string responce_message = "request canceled by the guest";
                    Boolean request_responce = false;
                    cnn.ExecuteScalar($"UPDATE door_requests_history SET request_responce = '{request_responce}', responce_message = '{responce_message}' WHERE id = '{lastRequest.id}'");
                    lastRequest = null;
                    Console.WriteLine("request canceled saved with success");
                }
            }
        }

        public DoorRequest getLastRequest()
        {
            if (lastRequest != null)
            {
                return getRequest(lastRequest.id);
            }
            return null;
        }

        public DoorRequest getRequest(int id)
        {
            using (IDbConnection cnn = connection.get_connection())
            {
                DoorRequest request = cnn.Query<DoorRequest>($"SELECT * FROM door_requests_history WHERE id = '{id}'").FirstOrDefault();
                return request;
            }
        }

        public List<DoorRequest> getHouseRequest(int house_id)
        {
            using (IDbConnection cnn = connection.get_connection())
            {
                List<DoorRequest> requests = cnn.Query<DoorRequest>($"SELECT * FROM door_requests_history WHERE house_id = '{house_id}'").ToList();
                return requests;
            }
        }

    }
}
