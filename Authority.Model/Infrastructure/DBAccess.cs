using Authority.Model.Domain.Entity;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;

namespace Authority.Model.Infrastructure
{
    internal static class DBAccess
    {
        private static readonly string connectionString_ = @"Data Source = ..\..\..\authority.db";

        public static List<AuthorityModel> GetAllAuthorities()
        {
            var table = new DataTable();
            table.Columns.Add("program_name", typeof(string));
            table.Columns.Add("pc_user", typeof(string));
            table.Columns.Add("control_flg1", typeof(int));
            table.Columns.Add("control_flg2", typeof(int));
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

        public static void UpdateAuthority(AuthorityModel before, AuthorityModel after)
        {
            using (var connection = new SQLiteConnection(connectionString_))
            {
                connection.Open();
                using (var command = new SQLiteCommand(connection))
                {
                    var sql = @"UPDATE authority
                                SET program_name = :after_program_name,
                                    pc_user = :after_pc_user,
                                    control_flg1 = :after_control_flg1,
                                    control_flg2 = :after_control_flg2
                                WHERE program_name = :before_program_name
                                    and pc_user = :before_pc_user";
                    command.CommandText = sql;
                    command.Parameters.Add(new SQLiteParameter(":after_program_name", after.ProgramName));
                    command.Parameters.Add(new SQLiteParameter(":after_pc_user", after.PCUser));
                    command.Parameters.Add(new SQLiteParameter(":after_control_flg1", after.ControlFlg1));
                    command.Parameters.Add(new SQLiteParameter(":after_control_flg2", after.ControlFlg2));
                    command.Parameters.Add(new SQLiteParameter(":before_program_name", before.ProgramName));
                    command.Parameters.Add(new SQLiteParameter(":before_pc_user", before.PCUser));

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}