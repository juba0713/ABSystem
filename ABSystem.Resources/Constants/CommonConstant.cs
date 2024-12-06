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
         * Book Status
         */
        public const string PENDING = "Pending";

        public const string ACCEPTED = "Accepted";

        public const string REJECTED = "Rejected";

        public const string CANCELED = "Canceled";

        public const string COMPLETED = "Completed";

        public const string NO_RECURRENCE = "NoRecurrence";

        public const string WEEKLY_RECURRENCE = "WeeklyRecurrence";

        public const string MONTHLY_RECURRENCE = "MonthlyRecurrence";

        public const string YEARLY_RECURRENCE = "YearlyRecurrence";



        /*
         * HTML File Path and Redirect
         */

        public const string A_USERS_LIST_HTML = "~/Views/Admin/UsersList.cshtml";
        public const string A_USERS_ADD_HTML = "~/Views/Admin/AddUser.cshtml";
        public const string A_USERS_EDIT_HTML = "~/Views/Admin/EditUser.cshtml";

        public const string A_ROOMS_LIST_HTML = "~/Views/Admin/RoomsList.cshtml";
        public const string A_ROOMS_ADD_HTML = "~/Views/Admin/AddRoom.cshtml";
        public const string A_ROOMS_EDIT_HTML = "~/Views/Admin/EditRoom.cshtml";

        public const string A_BOOKS_LIST_HTML = "~/Views/Admin/BooksList.cshtml";
        public const string A_BOOK_DETAILS_HTML = "~/Views/Admin/BookDetails.cshtml";
        

        public const string U_ROOMS_DETAILS_HTML = "~/Views/User/RoomDetails.cshtml";
        public const string U_BOOKS_LIST_HTML = "~/Views/User/BooksList.cshtml";
        public const string U_BOOK_DETAILS_HTML = "~/Views/User/BookDetails.cshtml";
        public const string U_CALENDAR_HTML = "~/Views/User/Calendar.cshtml";
    }
}
