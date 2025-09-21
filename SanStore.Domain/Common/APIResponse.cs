using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SanStore.Domain.Common
{
    public class APIResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = false;
        public Object Result { get; set; }  
        public string DisplayMessage { get; set; }
        public List<APIError> Errors { get; set; }
        public List<APIWarning> Warnings { get; set; }


        public void AddError(string errorMessage)
        {
            APIError error = new APIError(description: errorMessage);
            Errors.Add(error);
        }

        public void AddWarning(string warningMessage)
        {
            APIWarning error = new APIWarning(description: warningMessage);
            Warnings.Add(error);
        }

    }



}
