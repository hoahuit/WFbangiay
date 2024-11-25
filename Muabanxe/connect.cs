using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace Muabanxe
{
    public class connect
    {
        public  SqlConnection conn = new SqlConnection("Data Source=minhhoa;Initial Catalog=QL_MBXE;Integrated Security=True");
    }
}
