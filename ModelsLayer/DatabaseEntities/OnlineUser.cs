using System;
using System.Collections.Generic;
using System.Text;

namespace ModelsLayer.DatabaseEntities
{
    public class OnlineUser
    {
        //ova tablica nije povezana sa niti jednom jer ionako će se isprazniti ako nema online korisinika
        public string Id { get; set; }
        public string UserName { get; set; }
        public DateTime LastOnline { get; set; }
    }
}
