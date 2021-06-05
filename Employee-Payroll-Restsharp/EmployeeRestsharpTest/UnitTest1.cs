using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using RestSharp;
using System.Net;
using Employee_Payroll_Restsharp;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace EmployeeRestsharpTest
{
    [TestClass]
    public class UnitTest1
    {
        RestClient client;

        [TestInitialize]
        public void SetUp()
        {
            //Initialize the base URL to execute requests made by the instance
            client = new RestClient("http://localhost:3000");
        }
        //Interface Class come Restsharp library
        private IRestResponse GetEmployeeList()//For adding EmployeeList
        {
            //Arrange
            //Initialize the request object with proper method and URL
            //RestRequest for the request 
            //RestMethod to use when making request
            RestRequest request = new RestRequest("Employees", Method.GET);
            //Act
            // Execute the request
            IRestResponse response = client.Execute(request);
            return response;
        }


        // UC1 Retrieve all employee details in the json file 
        [TestMethod]
        public void OnCallingGetAPI_ReturnEmployeeList()
        {
            IRestResponse response = GetEmployeeList();
            // Check if the status code of response equals the default code for the method requested
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            // Convert the response object to list of employees
            List<Employee> employeeList = JsonConvert.DeserializeObject<List<Employee>>(response.Content);
            Assert.AreEqual(6, employeeList.Count);
            foreach (Employee emp in employeeList)
            {
                Console.WriteLine("Id: " + emp.Id + "\t" + "Name: " + emp.Name + "\t" + "Salary: " + emp.Salary);
            }
        }
    }
}