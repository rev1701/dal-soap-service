﻿using LMS1701.EA.SOAPAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace ExamassessmentWCF
{
    public class progam
    {
        static ServiceHost host = null;

        static void StartService()
        {
            host = new ServiceHost(typeof(Service1));
 
              /*if you don't want to use App.Config for the web service host, 
                 just uncomment below:
 
                 host.AddServiceEndpoint(new ServiceEndpoint(
                 ContractDescription.GetContract(typeof(IStudentEnrollmentService)),
                 new WSHttpBinding(), 
                 new EndpointAddress("http://localhost:8732/awesomeschoolservice"))); */

            host.Open();
        }

        static void CloseService()
        {
            if (host.State != CommunicationState.Closed)
            {
                host.Close();
            }
        }

        static void Main(string[] args)
        {
            StartService();

            Console.WriteLine("Student Enrollment Service is running....");
            Console.ReadKey();

           // CloseService();
        }

    }
}