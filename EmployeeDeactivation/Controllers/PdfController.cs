using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EmployeeDeactivation.Interface;
using Microsoft.AspNetCore.Mvc;
using Syncfusion.Pdf.Parsing;

namespace EmployeeDeactivation.Controllers
{
    public class PdfController : Controller
    {
        private readonly IEmployeeDataOperations _employeeDataOperation;

        public PdfController(IEmployeeDataOperations employeeDataOperation)
        {
            _employeeDataOperation = employeeDataOperation;
        }
        [HttpPost]
        public string Index(string gId)
        {
            var employeeData =_employeeDataOperation.RetrieveEmployeeDataBasedOnGid("G12");
            //Load the PDF document
            FileStream docStream = new FileStream("EmployeeDeAct.pdf", FileMode.Open, FileAccess.Read);
            PdfLoadedDocument loadedDocument = new PdfLoadedDocument(docStream);
            //Loads the form
            PdfLoadedForm form = loadedDocument.Form;
            //Fills the textbox field by using index
            (form.Fields[0] as PdfLoadedTextBoxField).Text = employeeData.Firstname;

            MemoryStream stream = new MemoryStream();
            loadedDocument.Save(stream);
            //If the position is not set to '0' then the PDF will be empty.
            stream.Position = 0;
            //Close the document.
            loadedDocument.Close(true);
            //Defining the ContentType for pdf file.
            string contentType = "application/pdf";
            //Define the file name.
            string fileName = "output.pdf";
            //Creates a FileContentResult object by using the file contents, content type, and file name.
            byte[] bytes = stream.ToArray();
            return "data:application/pdf;base64," + Convert.ToBase64String(bytes);


        }
    }
}