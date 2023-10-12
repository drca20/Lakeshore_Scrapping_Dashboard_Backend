using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCSampleModel.Common
{
    public class Constants
    {
        public static class ErrorMessages
        {
            public const string LoginFailed = "Login Failed";
            public const string InvalidGrantType = "Invalid Grant Types";
            public const string UnAuthorized = "Not Authorized";
            public const string NotFound = "Not Found";
            public const string UnAuthorizedToAccessTenant = "You are not authorized to view account details. Kinly contact your system administrator.";
            public const string IsInUsed = "Is In Use";
            public const string SortingNotAllowedOnUniqueKey = "Sorting is not allowed on the unique key or json field";
            public const string NotValidFormat = "is not a valid format";
            public const string Invalid = "invalid";
            public const string InvalidFilterPassed = "Passed filter is/are not in proper filter format. Filter shoGlo have the format either [{key:'',value:''}] or [{key:'',from:'',to:''}]";
            public const string NotAllowed = "NotAllowed";
        }

        public static class ApiResponseMessage
        {
            public const string AddedSuccessfully = "{0} added successfully.";
            public const string UpdatedSuccessfully = "{0} updated successfully.";
            public const string DeletedSuccessfully = "{0} delete successfully.";
            public const string FetchedSuccessfully = "{0} fetched successfully.";
            public const string SentSuccessfully = "{0} successfully.";
        }
        public class SearchParameters
        {
            public const string PageSize = "PageSize";

            public const string ShowMy = "ShowMy";
            public const string ShowAll = "ShowAll";
            public const string ModifiedAfter = "ModifiedAfter";
            public const string RequiredFields = "RequiredFields";
            public const string Filters = "Filters";
            public const string ContinuationToken = "ContinuationToken";
            public const string SortOrder = "SortOrder";
            public const string SortColumn = "SortColumn";
            public const string SearchText = "SearchText";
            public const string PageStart = "Page";
            public const string Conjuction = "Conjuction";

        }
        public static class DatabaseErrorCodes
        {
            public const string NotExist = "51000";
            public const string NotAllowed = "52000";
        }
        public enum DBStatus
        {
            Active = 1,
            Delete = 2
        }
        public enum DBUnitStatus
        {
            Draft = 5,
            Paid = 6,
            ApplyForCancel = 4,
            Deleted = 3,
        }
        public static class Role
        {
            public const string Admin = "Admin";
            public const string User = "User";
        }

        public static class MassMessgeGroup
        {
            public const string ALLUsers = "all_users";
            public const string ActiveLeaseUsers = "user_with_active_lease";
            public const string NonActiveLeaseUsers = "user_with_non_active_lease";
        }

        public enum ComplainStatus
        {
            New = 1,
            Replied = 2,
            Delete = 3,
            Compeleted = 4
        }

        public enum EnrollType
        {
            Individual = 1,
            Teacher = 2
        }

        public enum Compititor
        {
            LKS =1,
            KPL =2,
            DSS =3
        }

        public enum ProductComparisonType
        {
            notmatch = 3,
            partialmatch = 4,
            perfectmatch = 5
        }

    }
}
