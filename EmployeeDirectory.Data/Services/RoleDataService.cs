//using EmployeeDirectory.Data.Data.Services;
//namespace EmployeeDirectory.Data.Services
//{
//    public class RoleDataService : IRoleDataService
//    {
//        private IDbConnection dbConnection;
//        private ICommonDataService commonDataServices;
//        public RoleDataService(IDbConnection dbConnection, ICommonDataService commonDataServices)
//        {
//            this.dbConnection = dbConnection;
//            this.commonDataServices = commonDataServices;
//        }

//        public string GenerateNewLocationId(string lastLocId)
//        {
//            string prefix = "LOC";
//            string numericPart = lastLocId.Substring(prefix.Length);

//            if (int.TryParse(numericPart, out int numericId))
//            {
//                int newNumericId = numericId + 1;
//                return prefix + newNumericId.ToString("D3");
//            }
//            else
//            {
//                throw new ArgumentException("Invalid location ID format.");
//            }
//        }

//    }
//}