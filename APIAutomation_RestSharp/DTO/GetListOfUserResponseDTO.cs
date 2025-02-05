using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAutomation_RestSharp.DTO
{
    public class GetListOfUserResponseDTO
    {
       
            public long Page { get; set; }
            public long PerPage { get; set; }
            public long Total { get; set; }
            public long TotalPages { get; set; }
            public List<UserData> Data { get; set; }
         

        public partial class UserData
        {
            public long Id { get; set; }
            public string Email { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public Uri Avatar { get; set; }
        }
    }
}
