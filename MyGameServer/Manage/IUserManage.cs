using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyGameServer.Model;

namespace MyGameServer.Manage
{
    interface IUserManage
    {
        void Add(Runoob_tbl user);
        void Update(Runoob_tbl user);
        void Remove(Runoob_tbl user);
        Runoob_tbl GetById(int id);
        Runoob_tbl GetByUserName(string userName);
        ICollection<Runoob_tbl> GetAllUser();
        bool VerifiyUserAndPassword(string userName, string password);
    }
}
