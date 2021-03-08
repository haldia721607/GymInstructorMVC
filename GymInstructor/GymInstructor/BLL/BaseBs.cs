using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BLL
{
    public class BaseBs
    {
        public UserBs userBs { get; set; }
        public SecurityBs securityBs { get; set; }
        public AdminBs adminBs { get; set; }
        public BaseBs()
        {
            userBs = new UserBs();
            securityBs = new SecurityBs();
            adminBs = new AdminBs();
        }
    }
}
