using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserPwdEncrypt.BAL;

namespace UserPwdEncrypt
{
    public class PasswordGenerator
    {
        public static void GeneratePassword()
        {
            PasswordGenerateBAL _obj = new PasswordGenerateBAL();
            _obj.UserDetails();
        }
    }
}
