using EmployeeDeactivation.Interface;
using EmployeeDeactivation.Models;
using Syncfusion.Pdf.Parsing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace EmployeeDeactivation.BusinessLayer
{

    public class PdfDataOperation : IPdfDataOperation
    {
        private readonly IEmployeeDataOperation _employeeDataOperation;

        public PdfDataOperation(IEmployeeDataOperation employeeDataOperation)
        {
            _employeeDataOperation = employeeDataOperation;
        }

        public byte[] FillPdfForm(string GId)
        {
            var employeeData = _employeeDataOperation.RetrieveEmployeeDataBasedOnGid(GId);
            FileStream docStream = new FileStream("DeactivationFormPDF.pdf", FileMode.Open, FileAccess.Read);
            PdfLoadedDocument loadedDocument = new PdfLoadedDocument(docStream);
            PdfLoadedForm form = loadedDocument.Form;
            (form.Fields[11] as PdfLoadedTextBoxField).Text = employeeData.Firstname;
            (form.Fields[10] as PdfLoadedTextBoxField).Text = employeeData.Lastname;
            (form.Fields[13] as PdfLoadedTextBoxField).Text = employeeData.Email;
            (form.Fields[12] as PdfLoadedTextBoxField).Text = employeeData.GId;
            (form.Fields[4] as PdfLoadedTextBoxField).Text = employeeData.Date.ToString();
            string sponsorFullName = employeeData.SponsorName;
            string[] splitsponsorFullName = sponsorFullName.Split(' ');
            (form.Fields[16] as PdfLoadedTextBoxField).Text = splitsponsorFullName[0];
            (form.Fields[17] as PdfLoadedTextBoxField).Text = splitsponsorFullName[1];
            (form.Fields[18] as PdfLoadedTextBoxField).Text = employeeData.SponsorGId;
            (form.Fields[19] as PdfLoadedTextBoxField).Text = employeeData.Department;
            MemoryStream stream = new MemoryStream();
            loadedDocument.Save(stream);
            stream.Position = 0;
            loadedDocument.Close(true);
            byte[] bytes = stream.ToArray();
            return bytes;
        }

        public void SendPdfAsEmailAttachment(string memoryStream, string employeeName, string teamName)
        {
            var reportingManagerEmailId = _employeeDataOperation.GetReportingManagerEmailId(teamName);
            byte[] bytes = System.Convert.FromBase64String(memoryStream);
            var c = bytes;
            MemoryStream stream = new MemoryStream(bytes);
            Attachment file = new Attachment(stream, "Deactivation workflow_" + employeeName + ".pdf", "application/pdf");
            SendEmail(reportingManagerEmailId,employeeName,false,file);


        }


        public void SendReminderEmail()
        {
            var employeeDetails = _employeeDataOperation.SavedEmployeeDetails();
            foreach (var item in employeeDetails)
            {
                if (DateTime.Today == item.Date)
                {
                    var reportingManagerEmailId = _employeeDataOperation.GetReportingManagerEmailId(item.TeamName);
                    SendEmail(reportingManagerEmailId, item.Firstname + " " + item.Lastname, true, new Attachment(new MemoryStream(), " "));
                }
            }

        }
        private void SendEmail(string reportingManagerEmail , string employeeName, bool isReminderEmail, Attachment file )
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress("dontreplydeactivationworkflow@gmail.com");
            message.Sender = new MailAddress("dontreplydeactivationworkflow@gmail.com");
            message.To.Add(reportingManagerEmail);
            message.Subject = "Deactivation workflow initiated";
            if (isReminderEmail)
            { 
                message.Body = "Today is " + employeeName+"'s last working day please check if you have approved the deactivation workflow";
            }
            else {
                message.Attachments.Add(file);
            }
            
            message.IsBodyHtml = false;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("dontreplydeactivationworkflow@gmail.com", "Siemens@Banglore98");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Send(message);

        }
    }
}
