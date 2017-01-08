using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DineConnectXF.Model
{
    public class User
    {
        #region Response data
        public int UserId { get; set; }
        public int TenantId { get; set; }
        public bool Success { get; set; }
        public string Result { get; set; }
        public Error Error { get; set; }
        public bool UnauthorizedRequest { get; set; }
        #endregion

        #region Request data
        public string Url { get; set; }
        public string TenacyName { get; private set; }
        public string UsernameOrEmail { get; private set; }
        public string Password { get; private set; }
        #endregion

        public User(string tenacyName, string userOremail, string pass)
        {
            TenacyName = tenacyName;
            UsernameOrEmail = userOremail;
            Password = pass;
        }
    }
}
