using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Dapper;

namespace ApiServer.DTO
{
    public class RowEntity : Dictionary<string, object>
    {
        public RowEntity() { }
        public RowEntity(Dictionary<string, object> valuePairs) : base(valuePairs) { }
    }

}