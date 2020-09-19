using Authority.Model.Domain.Entity;
using Authority.Model.Infrastructure;
using System.Collections.Generic;

namespace Authority.Model.ApplicationService
{
    public class AuthorityApplicationService
    {
        public FlagDetail GetDetail(string programName)
        {
            return DetailParser.GetDetail(programName);
        }

        public List<AuthorityModel> GetAllAuthorities()
        {
            return DBAccess.GetAllAuthorities();
        }

        public void UpdateAuthority(AuthorityModel before, AuthorityModel after)
        {
            DBAccess.UpdateAuthority(before, after);
        }

        public void InsertAuthority(AuthorityModel authority)
        {
            DBAccess.InsertAuthority(authority);
        }

        public void DeleteAuthority(AuthorityModel authority)
        {
            DBAccess.DeleteAuthority(authority);
        }

        /* details.json作成メソッド */
        //public void SaveDetails()
        //{
        //    var details = new List<FlagDetail>
        //    {
        //        new FlagDetail
        //        {
        //            ProgramName="価格データ管理",
        //            ControlFlg1="編集権限",
        //            ControlFlg2="追加削除権限"
        //        },
        //        new FlagDetail
        //        {
        //            ProgramName="ローラーチェックシート",
        //            ControlFlg1="データ追加権限",
        //            ControlFlg2="データ編集権限"
        //        }
        //    };
        //    var json = JsonConvert.SerializeObject(details, Formatting.Indented);
        //    File.WriteAllText(@"..\..\..\details.json", json);
        //}
    }
}