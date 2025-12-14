using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityPractice
{
    internal class CRUDCLASS
    {
        dbo_dbEntities1 db1 = new dbo_dbEntities1();

        public void showemployee()
        {
            //it should display all employee

            var res = from e in db1.Employees select e;
            foreach(var e in res)
            {
                Console.WriteLine($"{e.EmpID},{e.EmpName},{e.DateOfJoin},{e.DeptID},{e.Salary}");
                Console.WriteLine("------------------------------------------------------------");
            }
        }
        public void searchrecord()
        {
            var res = from e in db1.Employees
                      where e.EmpName.Contains("e") select e;
            foreach (var e in res)
            {
                Console.WriteLine($"{e.EmpID},{e.EmpName},{e.DateOfJoin},{e.DeptID},{e.Salary}");
                Console.WriteLine("------------------------------------------------------------");
            }

        }
        public void Addrecord()
        {
            //create object of the table which you want to add(employee)

            // step 1 initialize the properties
           
            Employee eob= new Employee() { EmpName="Rakesh",Salary=340000,DateOfJoin=DateTime.Parse("2025-01-01"),DeptID=10};

            //step 2 attach the object to property

            db1.Employees.Add(eob);

            //step 3 update the change to database

            int i = db1.SaveChanges(); //update all change to database

            Console.WriteLine($"Total rows updated : {i}");

        }
        public void Removerecord()
        {
            //step 1 search record u want to remove

            Console.WriteLine("Enter empid for removed");
            int eid=int.Parse(Console.ReadLine());

            var res = (from e in db1.Employees
                      where e.EmpID == eid
                      select e).First();

            // step 2 remove that row from table

            db1.Employees.Remove(res);

            //update the changes in database

            int i = db1.SaveChanges();

            Console.WriteLine($"Total rows deleted : {i}");

            Console.WriteLine($"{res.EmpID},{res.EmpName},{res.DateOfJoin},{res.Salary},{res.DeptID}");
            Console.WriteLine("------------------------------------------------------------------------");
        }
        public void updaterecord()
        {
            // step 1
            Console.WriteLine("enter the employee id for update the record");
            int id = int.Parse(Console.ReadLine());

            var res= (from e in db1.Employees
                     where e.EmpID == id select e).First();
            //step 2

            res.Salary = 500000;

            // step 3

            int i = db1.SaveChanges();

            Console.WriteLine($"total rows updated : {i}");
        }

        // Questions

        public void Question1()
        {
            //display matching records of employee and department  from databse

            var res = from e in db1.Employees
                      join d in db1.Departments
                      on e.DeptID equals d.DeptID
                      select new { e, d };

            
        }
        static void Main(string[] args)
        {
            CRUDCLASS cc=new CRUDCLASS();
            //Question1
            Console.WriteLine("All employee record:\n");
            cc.showemployee();

            //Question2
            Console.WriteLine("\nShow filter employee:");
            cc.searchrecord();

            //QUestion3
            Console.WriteLine("\nAdded record");
            cc.Addrecord();

            //Question4
            Console.WriteLine("\nremoved record");
            cc.Removerecord();

            //Question5
            Console.WriteLine("\nupdated record");
            cc.updaterecord();

            //QuestionAssign1
            Console.WriteLine("MAtching record :");
            cc.Question1();

            Console.ReadLine();

        }
    }
}
