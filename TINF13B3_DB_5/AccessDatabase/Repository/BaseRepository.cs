using System.Collections.Generic;
using System.Linq;
using AccessDatabase.Interface;
using Dapper;

namespace AccessDatabase.Repository
{
    public class BaseRepository:IRepository
    {
        protected List<T> Query<T>(string sql)
        {   
            return Query<T>(sql, new {});
        }

        protected List<T> Query<T>(string slq, object parameter)
        {
            using (var connection = ConnectionFactory.Get())
            {
                return connection.Query<T>(slq, parameter).ToList();
            }
        }

        protected int Execute<T>(string sql, object parameter)
        {
            int affectedRows = 0;
            using (var connection = ConnectionFactory.Get())
            {
                affectedRows = connection.Execute(sql, parameter);
            }
            return affectedRows;
        }
    }
}