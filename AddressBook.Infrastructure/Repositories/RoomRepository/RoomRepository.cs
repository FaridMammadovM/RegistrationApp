using AddressBook.Domain.Dtos.Room;
using AddressBook.Domain.Models.Room;
using AddressBook.Infrastructure.Helpers;
using Dapper;
using DapperExtension;

namespace AddressBook.Infrastructure.Repositories.RoomRepository
{
    public sealed class RoomRepository : IRoomRepository
    {

        private string _conString;
        public RoomRepository(string conString)
        {
            _conString = conString;
        }


        public async Task<List<RoomModel>> GetAll()
        {
            using (var con = DbHelper.GetConn(_conString))
            {
                con.Open();

                var res = con.GetAll<RoomModel>().OrderBy(e => e.Name).ToList();

                con.Close();
                return await Task.FromResult(res.ToList());
            }
        }


        public async Task<GetByIdRoomWithEpiuqmentDto> GetById(int id)
        {
            string query = "SELECT * FROM \"AB_ROOM\".get_roomid_byequipment(@id)";

            using (var con = DbHelper.GetConn(_conString))
            {
                con.Open();

                var parameters = new { id = id };

                var result = await con.QuerySingleOrDefaultAsync<string>(query, parameters);

                if (result != null)
                {
                    var dto = Newtonsoft.Json.JsonConvert.DeserializeObject<GetByIdRoomWithEpiuqmentDto>(result);

                    return dto;
                }
                else
                {
                    return null;
                }
            }

        }
    }
}
