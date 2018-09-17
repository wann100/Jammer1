using System;
using System.Collections.Generic;
using System.Text;

namespace Jammer_1.Models
{
    class superuser
    {
        
        About_user about_me;
        public About_user About_me { get { return about_me; } set { about_me = value; } }

        User current_user;
        public User Current_user { get { return current_user; } set { current_user = value; } }

    }
}
