using Authority.Model.Domain.Entity;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace Authority.Model.Infrastructure
{
    internal static class DBAccess
    {
        static private readonly string connectionString_ = @"Data Source = ..\..\..\authority.db";

        public static List<AuthorityModel> GetAllAuthorities()
        {
            var table = new DataTable();
            using (var connection = new SQLiteConnection(connectionString_))
            {
                connection.Open();
                using (var command = new SQLiteCommand(connection))
                {
                    var sql = "SELECT * FROM authority";
                    command.CommandText = sql;
                    using (var adaptor = new SQLiteDataAdapter(command))
                    {
                        adaptor.Fill(table);
                    }
                }
            }
            return table.AsEnumerable().Select(x => new AuthorityModel
            {
                ProgramName = x["program_name"] as string,
                PCUser = x["pc_user"] as string,
                ControlFlg1 = x["control_flg1"] as int?,
                ControlFlg2 = x["control_flg2"] as int?
            }).ToList();
        }
    }
}