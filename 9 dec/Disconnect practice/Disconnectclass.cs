using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Disconnect_practice
{
    internal class Disconnectclass
    {

        public void showemployeebyRC()
        {

            SqlConnection con = new SqlConnection("Integrated security=true;database=dbo_db;server=(localdb)\\MSSQLLocalDB");
            // no need to open and close connection



            SqlDataAdapter da = new SqlDataAdapter("select * from employee", con);

            SqlDataAdapter da1 = new SqlDataAdapter("select * from department", con);

            DataSet ds = new DataSet();// can hold the output (many output)

            da.Fill(ds, "emp");//now ds contains output of employee table

            da1.Fill(ds, "dept");// now ds contains emp + dept

            Console.WriteLine(ds.Tables["emp"].Rows[1][1]);

            Console.WriteLine(ds.Tables["dept"].Rows[1][1]);

        }
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();// can hold the output (many output)
        SqlDataAdapter da;
        public void showAllEmployee()
        {
            SqlConnection con = new SqlConnection("Integrated security=true;database=dbo_db;server=(localdb)\\MSSQLLocalDB");
            // fill : run the query + store the output in dataset

            // no need to open and close connection

            da = new SqlDataAdapter("select * from employee", con);

            //da.MissingSchemaAction = MissingSchemaAction.AddWithKey;

            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;

            SqlCommandBuilder cmd= new SqlCommandBuilder(da);
            da.Fill(ds, "emp");//now ds contains output of employee table   + it knows which column is primary key  



            dt = ds.Tables["emp"];//now dt contains the output of emp


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Console.WriteLine(dt.Rows[i][0]);
                Console.WriteLine(dt.Rows[i][1]);
                Console.WriteLine(dt.Rows[i][2]);
                Console.WriteLine(dt.Rows[i][3]);
                Console.WriteLine(dt.Rows[i][4]);
            }
        }

        public void SearchEmployees()
        {
            SqlConnection con = new SqlConnection("Integrated security=true;database=dbo_db;server=(localdb)\\MSSQLLocalDB");
            da = new SqlDataAdapter("select * from employee", con);

            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;

            

            da.Fill(ds, "emp");

            dt = ds.Tables["emp"];//now dt contains the output of emp


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Console.WriteLine(dt.Rows[i][0]);
                Console.WriteLine(dt.Rows[i][0]);
                Console.WriteLine(dt.Rows[i][2]);
                Console.WriteLine(dt.Rows[i][3]);
                Console.WriteLine(dt.Rows[i][4]);
            }

            // search employee by id

            Console.WriteLine("enter the id");

            int id = int.Parse(Console.ReadLine());
            DataRow drr = dt.Rows.Find(id);// search happens from datatable not from database

            if (drr != null)
            {
                Console.WriteLine(drr[0]);
                Console.WriteLine(drr[1]);
                Console.WriteLine(drr[2]);
                Console.WriteLine(drr[3]);
                Console.WriteLine(drr[4]);
            }
            else
            {
                Console.WriteLine("No Such Key Exists!!");
            }
        }
        public void AddEmployee()
        {
            // new employee details

            // all crud operation are done using datable

            // dt.Rows.Count :total rows present in datatable
            //dt.Rows.Add : adding new rows
            // dt.Rows.Find:search row by primary key
            //dt.Rows.Remove:delete existing row

            dt.Rows.Add(null, "Raj1", 30000, "1-1-2000", 10);
            dt.Rows.Add(null, "vijay1", 31000, "1-1-2001", 20);// a new rows is added to datatable

            // if database is availbe only then i will call update method
            // if its not available all changes i will store in xml file

            //how to update this changes to database?

            int rowsaffected = da.Update(dt);// all changes will update to database
            Console.WriteLine("total rows inserted is " + rowsaffected);

        }
        public void DeleteEmployee()
        {
            // search a row which u want to delete
            Console.WriteLine("enter the id");
            int id = int.Parse(Console.ReadLine());
            DataRow drr = dt.Rows.Find(id);// search happens from datatable not from database


            drr.Delete();// this will remove row from datatable



            int rowsaffected = da.Update(dt);// all changes will update to database
            Console.WriteLine("total rows Deleted is " + rowsaffected);

        }
        public void UpdateEmployee()
        {
            Console.WriteLine("enter the id");
            int id = int.Parse(Console.ReadLine());
            DataRow drr = dt.Rows.Find(id);// search happens from datatable not from database

            drr[2] = 65000;

            int rowsaffected = da.Update(dt);// all changes will update to database
            Console.WriteLine("total rows updated is " + rowsaffected);


        }

        public void FilterEmployee()
        {

            // how can u search non primary key column

            Console.WriteLine("Rows after filter is as follows ");
            Console.WriteLine("===================================================");
            DataView dv = new DataView(dt);

            //  dv.RowFilter = "salary > 40000 and Deptid = 10";
            dv.RowFilter = "EmpName like 'M%'";
            foreach (DataRowView item in dv)
            {
                Console.WriteLine(item[0]);
                Console.WriteLine(item[1]);
                Console.WriteLine(item[2]);
                Console.WriteLine(item[3]);
                Console.WriteLine(item[4]);


            }
        }
        public void StoreinXML()
        {


            // ds.ReadXml(); reads the xml file and stores in dataset
            // ds.WriteXml(); creates xml file and write all dataset records to xml

            // ds.WriteXml("d:\\employee.xml");

            dt.Rows.Add(null, "Raj1", 30000, "1-1-2000", 10);
            dt.Rows.Add(null, "vijay1", 31000, "1-1-2001", 20);// a new rows is added to datatable



            ds.WriteXml("C:\\Users\\deeptisa\\OneDrive - Infinite Computer Solutions (India) Limited\\Desktop\\training_session_2\\dec\\dbo.net\\practice\\9 dec\\employee1.xml", XmlWriteMode.DiffGram);// shows which rows inserted, deleted or updated
            Console.WriteLine("Created Successfully");


        }

        public void changes()
        {


            // 27 records in datatable
            // show me only those records from datatable where new changes has been taken place


            //dt.Rows.Add(null, "Raj1", 30000, "1-1-2000", 10);
            //dt.Rows.Add(null, "vijay1", 31000, "1-1-2001", 20);// a new rows is added to datatable
            Console.WriteLine("============================");
            Console.WriteLine("Following are new changes : ");
            if (ds.HasChanges())
            {
                DataSet newds = ds.GetChanges();// newds contains only 2 rows

                for (int i = 0; i < newds.Tables["emp"].Rows.Count; i++)
                {

                    Console.WriteLine(newds.Tables["emp"].Rows[i][0]);
                    Console.WriteLine(newds.Tables["emp"].Rows[i][1]);
                    Console.WriteLine(newds.Tables["emp"].Rows[i][2]);
                    Console.WriteLine(newds.Tables["emp"].Rows[i][3]);
                    Console.WriteLine(newds.Tables["emp"].Rows[i][4]);
                }

            }
            else
            {
                Console.WriteLine("No Changes has happened in datatable ");
            }
            // ds.GetChanges

        }

        static void Main(string[] args)
        {
            // obj of showemployeebyRC

            Disconnectclass dc = new Disconnectclass();
            //dc.showemployeebyRC();

            // obj of showallemployee
            dc.showAllEmployee();


            //dc.SearchEmployees();


            //dc.AddEmployee();


            //dc.DeleteEmployee();

            //dc.UpdateEmployee();

            //dc.FilterEmployee();

            //dc.StoreinXML();

            dc.changes();

        }
    }
}
