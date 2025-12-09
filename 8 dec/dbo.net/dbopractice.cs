using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbo.net
{
    internal class dbopractice
    {
        static void Main(string[] args)
        {
            // 1st object

            dbopractice db =new dbopractice();
            //db.AddEmployees("", 0, "", 0);

            //db.Employeeshow();

            // 2nd object

            //db.AddEmployee();


            // objecy for dynamic method

            //3rd object
            //db.DeleteEmployee();

            //4th object
            //db.UpdateEmployee();


            // obj of procedures
            //db.showprocedure();

            // obj of transaction
            db.EmpTransactions();
            Console.ReadLine();



        }
        public void Employeeshow()
        {
            //display all record from employee table

            SqlConnection con = new SqlConnection("Integrated security=true;database=dbo_db;server=(localdb)\\MSSQLLocalDB");
            con.Open();  //create new connection

            SqlCommand cmd = new SqlCommand("Select * from employee", con);
            SqlDataReader dr= cmd.ExecuteReader();

            while(dr.Read())
            {
                Console.WriteLine($"{dr[0]}  {dr[1]}  {dr[2]}  {dr[3]}");
            }


            con.Close();
        }

        public void AddEmployee()
        {
            SqlConnection con = new SqlConnection("Integrated security=true;database=dbo_db;server=(localdb)\\MSSQLLocalDB");
            con.Open();  //create new connection
           

            SqlCommand cmd = new SqlCommand("insert into employee values('Raj',23000,'2025-10-02',30)", con);
            int rowaffected = cmd.ExecuteNonQuery();

            Console.WriteLine("Total record inserted is " + rowaffected);

            con.Close();
        }
        public  void AddEmployees(String Empname,decimal salary,string dateofjoin,int deptid)
        {
            SqlConnection con = new SqlConnection("Integrated security=true;database=dbo_db;server=(localdb)\\MSSQLLocalDB");
            con.Open();

            Console.WriteLine("Enter Employee Name :");
            string empname = Console.ReadLine();
            Console.WriteLine("Enter Salary ");
            int sal = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the dateofjoin");
            string doj = Console.ReadLine();
            Console.WriteLine("Enter dpetid:");
            int deptId = Convert.ToInt32(Console.ReadLine());
            //string query ="Insert into Employee(Empname,Salary,Dateofjoin,deptid)" +
            //    "values($'{empname } {sal} { doj} { deptId}')";

            SqlCommand cmd = new SqlCommand($"insert into Employee values('{empname}' ,'{sal}' , '{doj}' , '{deptId}')",con);
            int rowaffected=cmd.ExecuteNonQuery();
            con.Close();
            Console.WriteLine($"{rowaffected} record inserted ");
        }
        public void DeleteEmployee()
        {
            SqlConnection con = new SqlConnection("Integrated security=true;database=dbo_db;server=(localdb)\\MSSQLLocalDB");
            con.Open();  //create new connection
           
            //SqlCommand cmd = new SqlCommand("delete from Employee where EmpId=26", con);
            //int rowaffected = cmd.ExecuteNonQuery();

            //Console.WriteLine("Total deleted record is " + rowaffected);

            // dynamic
            Console.WriteLine("Enter Empid for deleting the record :");
            int EmpId = Convert.ToInt32(Console.ReadLine());
            SqlCommand cmd = new SqlCommand($"delete from Employee where EmpId= '{EmpId}'", con);
            int rowaffected = cmd.ExecuteNonQuery();

            Console.WriteLine("Total deleted record is " + rowaffected);
            con.Close();
        }
        public void UpdateEmployee()
        {
            SqlConnection con = new SqlConnection("Integrated security=true;database=dbo_db;server=(localdb)\\MSSQLLocalDB");
            con.Open();  //create new connection

            SqlCommand cmd = new SqlCommand("update employee set empname='Raj' where empid=24", con);

            int rowaffected = cmd.ExecuteNonQuery();

            Console.WriteLine("Total updated record is " + rowaffected);

            con.Close();
        }

        public void showprocedure()
        {
            //display all record from employee table

            SqlConnection con = new SqlConnection("Integrated security=true;database=dbo_db;server=(localdb)\\MSSQLLocalDB");
            con.Open();  //create new connection

            // for without parameter in procedure

            //SqlCommand cmd = new SqlCommand("pr_emp", con);
            //cmd.CommandType = CommandType.StoredProcedure;
            //SqlDataReader dr = cmd.ExecuteReader();


            // for with parameters

            SqlCommand cmd = new SqlCommand("pr_empbycond", con);

            SqlParameter p1 = new SqlParameter("@empid", 2);
            SqlParameter p2 = new SqlParameter("@sal", 23000);
            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);

            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Console.WriteLine($"{dr[0]}  {dr[1]}  {dr[2]}  {dr[3]}");
            }


            con.Close();
        }
        public void EmpTransactions()
        {
            SqlTransaction tr = null;

            try
            {
                //display all record from employee table

                SqlConnection con = new SqlConnection("Integrated security=true;database=dbo_db;server=(localdb)\\MSSQLLocalDB");
                con.Open();  //create new connection


                tr = con.BeginTransaction();

                SqlCommand cmd1 = new SqlCommand("insert into employee values('Deep',23000,'2025-12-12',30)", con);
                SqlCommand cmd2 = new SqlCommand("insert into employee values('Deep',23000,'2025-12-12',30)", con);

                cmd1.Transaction = tr;
                cmd2.Transaction = tr;

                int rowaffected =cmd1.ExecuteNonQuery();
                int rowaffected2 =cmd2.ExecuteNonQuery();

                Console.WriteLine("Total record inserted "+rowaffected);
                Console.WriteLine("Total record inserted " + rowaffected2);

                tr.Commit();
                con.Close();
            }
            catch (Exception ex)
            {
                tr.Rollback();
                Console.WriteLine(ex.Message);
            }
           
        }
    }
}
