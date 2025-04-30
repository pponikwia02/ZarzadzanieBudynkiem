using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IO
{
    public class UserTypeItem
    {
        public int Value { get; set; }
        public required string DisplayName { get; set; }

        public static List<UserTypeItem> GetUserTypes()
        {
            return new List<UserTypeItem>()
            {
                new UserTypeItem{Value = 0, DisplayName = "Wykładowca"},
                new UserTypeItem{Value = 1, DisplayName = "Administrator"}
            };
        }

    }
}
