using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgoritmWeb.DBlayer.Enum
{
    public enum StatusCode
    {
        OK = 200,
        EinternalServerError = 500,
        NotAuthorized = 401,
        NoDataInDB = 404
    }
}
