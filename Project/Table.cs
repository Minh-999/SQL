using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caffee_Cub_Management
{
    public class Table
    {
        private string id;

        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string status;
        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public Table(string id, string name, string status)
        {
            this.id = id;
            this.name = name;
            this.status = status;
        }

        public Table(DataRow row)
        {
            this.id = row["IDBan"].ToString();
            this.name = row["TenBan"].ToString();
            this.status = row["TrangThai"].ToString();
        }
    }
}
