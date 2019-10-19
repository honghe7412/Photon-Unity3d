using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGameServer.Model
{
    class Runoob_tbl
    { 
        public virtual int ID { set; get; }
        public virtual string Username { set; get; }
        public virtual string Password { set; get; }
        public virtual DateTime RegisterDate { set; get; }
    }
}
