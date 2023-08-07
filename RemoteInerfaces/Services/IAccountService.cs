using RemoteInerfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteInerfaces.Services
{
    public interface IAccountService
    {
        void createAdminAccount(Account account, House house);

        void createResidentAccount(Account account);

        void editAccount(Account account);

        void activateAccount(int id);

        void disactivateAccount(int id);

        Account getAccount(int id);

        List<Account> getHouseAccount(int house_id);

        List<FaceID> getAccountFacesWithIDs();

        void deleteAccount(int id);

        House getHouseByKey(string house_key);

    }
}
