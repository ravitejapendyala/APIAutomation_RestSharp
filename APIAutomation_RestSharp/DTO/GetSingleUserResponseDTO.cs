using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAutomation_RestSharp.DTO
{
    public  class GetSingleUserResponseDTO
    {
        public userData Data { get; set; }
        public SupportData Support { get; set; }
        public partial class userData
        {
            public long Id { get; set; }
            public string Email { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public Uri Avatar { get; set; }
        }

        public partial class SupportData
        {
            public Uri Url { get; set; }
            public string Text { get; set; }
        }
    }
}
