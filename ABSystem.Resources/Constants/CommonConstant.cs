using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABSystem.Resources.Constants
{
    public class CommonConstant
    {

        /*
         *  Folder Names
         */
        public const string RootFileName = "ABSystem";

        public const string RoomFileName = "Rooms";


        public static readonly string RootPath = Path.Combine(@"C:\", RootFileName);

        public static readonly string RoomPath = Path.Combine(RootPath, RoomFileName);

        /*
         *  Roles
         */

        public const string Super = "SuperAdmin";

        public const string Admin = "Admin";

        public const string User = "User";




        /*
         * HTML File Path and Redirect
         */

        public const string A_USERS_LIST_HTML = "~/Views/Admin/UsersList.cshtml";
        public const string A_USERS_ADD_HTML = "~/Views/Admin/UsersList.cshtml";
        public const string A_USERS_EDIT_HTML = "~/Views/Admin/UsersList.cshtml";

        public const string A_ROOMS_LIST_HTML = "~/Views/Admin/RoomsList.cshtml";
        public const string A_ROOMS_ADD_HTML = "~/Views/Admin/AddRoom.cshtml";
        public const string A_ROOMS_EDIT_HTML = "~/Views/Admin/EditRoom.cshtml";
    }
}
