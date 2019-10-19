using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;
using MyGameServer.Model;

namespace MyGameServer.Manage
{
    class UserManage : IUserManage
    {
        public void Add(Runoob_tbl user)
        {
            using (ISession session = NhibernateHelper.OpenSession()) //using 代表用完session就释放
            {
                using (ITransaction transcation = session.BeginTransaction())
                {
                    session.Save(user);
                    transcation.Commit();
                }
            }
        }

        public ICollection<Runoob_tbl> GetAllUser()
        {
            using (ISession session = NhibernateHelper.OpenSession()) //using 代表用完session就释放
            {
                ICriteria criteria = session.CreateCriteria(typeof(Runoob_tbl));
                return criteria.List<Runoob_tbl>();
            }
        }

        public Runoob_tbl GetById(int id)
        {
            using (ISession session = NhibernateHelper.OpenSession()) //using 代表用完session就释放
            {
                using (ITransaction transcation = session.BeginTransaction())
                {
                    Runoob_tbl user=session.Get<Runoob_tbl>(id);
                    transcation.Commit();
                    return user;
                }
            }
        }

        public Runoob_tbl GetByUserName(string userName) //查询userName
        {
            using (ISession session = NhibernateHelper.OpenSession()) //using 代表用完session就释放
            {
                ICriteria criteria = session.CreateCriteria(typeof(Runoob_tbl));
                criteria.Add(Restrictions.Eq("Username", userName));
                return criteria.UniqueResult<Runoob_tbl>();
            }
        }

        public void Remove(Runoob_tbl user)
        {
            using (ISession session = NhibernateHelper.OpenSession()) //using 代表用完session就释放
            {
                using (ITransaction transcation = session.BeginTransaction())
                {
                    session.Delete(user);
                    transcation.Commit();
                }
            }
        }

        public void Update(Runoob_tbl user)
        {
            using (ISession session = NhibernateHelper.OpenSession()) //using 代表用完session就释放
            {
                using (ITransaction transcation = session.BeginTransaction())
                {
                    session.Update(user);
                    transcation.Commit();
                }
            }
        }

        public bool VerifiyUser(string userName, string password)
        {
            using (ISession session = NhibernateHelper.OpenSession()) //using 代表用完session就释放
            {
                Runoob_tbl user= session
                                .CreateCriteria(typeof(Runoob_tbl))
                                .Add(Restrictions.Eq("Username", userName))
                                .Add(Restrictions.Eq("Password", password))
                                .UniqueResult<Runoob_tbl>();

                if (user == null)
                    return false;
                else
                    return true;
            }
        }
    }
}
