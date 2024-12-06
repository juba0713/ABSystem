using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABSystem.Resources.Constants
{
    public class MessageConstant
    {

        public const string ERROR = "Error";

        public const string NO_USER_LOGIN = "No user is logged in";

        /*
         *  INPUT
         */

        public const string LOGIN_ERROR = "These credentials do not match our records";

        public const string ALL_FIELDS_REQUIRED = "All fields are required";

        public const string EMAIL_REQUIRED = "Email address is required";

        public const string EMAIL_FORMAT = "Invalid email address format";

        public const string EMAIL_IN_USE = "Email in use";

        public const string PASSWORD_REQUIRED = "Password is required";

        public const string CONFIRM_PASSWORD_REQUIRED = "Confirm Password is required";

        public const string PASSWORD_LENGTH = "Password must be between 6 and 100 characters";

        public const string PASSWORD_MISMATCH = "Passwords do not match.";

        public const string FIRST_NAME_REQUIRED = "First name is required";

        public const string LAST_NAME_REQUIREED = "Last name is required";

        public const string ROLE_REQUIRED = "Role is required";

        public const string ROOM_NAME_REQUIRED = "Room Name is required";

        public const string ROOM_FACILITY_REQUIRED = "Facility is required";

        public const string ROOM_CAPACITY_REQUIRED = "Capacity is required";

        public const string ROOM_ADDRESS_REQUIRED = "Address is required";

        public const string ROOM_DESCRIPTION_REQUIRED = "Description is required";

        public const string BOOK_DATE_REQUIRED = "Complete all the required fields";


        /*
         *  AFTER ACTION
         */

        public const string ADDED_USER = "New user has been added successfully";

        public const string DELETED_USER = "A user has been deleted successfully";

        public const string EDITED_USER = "A user has been edited successfully";

        public const string ADDED_ROOM = "New room has been added successfully";

        public const string EDITED_ROOM = "A room has been edited successfully";

        public const string DELETED_ROOM = "A room has been deleted successfully";

        public const string BOOK_APPROVED = "The room booking has been approved successfully";

        public const string BOOK_REJECTED = "The room booking has been rejected";

        public const string BOOK_CANCELED = "The room booking has been canceled";

        public const string BOOK_COMPLETED = "The room booking has been completed successfully";


        /*
         * THROW MESSAGE
         */

        public const string ROOM_NOT_FOUND = "Room not found";

        public const string ROOM_ADDED_ERROR = "Error occurred while adding a room";

        public const string ROOM_EDITED_ERROR = "Error occurred while editing a room";

        public const string ROOM_DELETED_ERROR = "Error occured while deleting a room";

        public const string BOOK_ADDED_ERROR = "Error occured while adding a book";

        public const string BOOK_APPROVED_ERROR = "Error occured while approving a book";

        public const string BOOK_REJECTED_ERROR = "Error occured while rejecting a book";

        public const string BOOK_RETRIEVAL_ERROR = "Error occured while retrieving a book";

        public const string BOOK_CANCELED_ERROR = "Error occured while canceling a book";

        public const string NOTIFICATION_RETRIEVAL_ERROR = "Error occured while retrieving a notifications";
    }
}
