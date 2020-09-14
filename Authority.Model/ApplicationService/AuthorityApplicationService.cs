using Authority.Model.Domain.Entity;
using Authority.Model.Infrastructure;
using System.Collections.Generic;

namespace Authority.Model.ApplicationService
{
    public class AuthorityApplicationService
    {
        public List<AuthorityModel> GetAllAuthorities()
        {
            return DBAccess.GetAllAuthorities();
        }
    }
}