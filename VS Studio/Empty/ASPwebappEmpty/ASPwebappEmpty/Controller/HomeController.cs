using ASPwebappEmpty.Model;
using ASPwebappEmpty.ViewData;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ASPwebappEmpty
{
    public class HomeController: Controller
    {
        private IEmployeeInterface _employeeRepository;
        private IHostingEnvironment _hostingEnvironment;
        private ILogger<HomeController> _logger;

        public HomeController(IEmployeeInterface employeeRepository, IHostingEnvironment hostingEnvironment, ILogger<HomeController> logger)
        { 
            _employeeRepository = employeeRepository;
            _hostingEnvironment = hostingEnvironment;
            _logger = logger;
        }
        public ViewResult Index(string? returnValue)
        {
            var _returnValue = "";
            if (returnValue != null)
            {
                _returnValue = returnValue;
            }
            ViewModelDTO pagedata = new ViewModelDTO()
            {
                PageTitle = "Index Page",
                empDetails = _employeeRepository.GetAllData(),
                returnValue = _returnValue,
                wwwpath = Path.Combine(_hostingEnvironment.WebRootPath, "Files\\")
            };
            return View(pagedata);
        }

        public ViewResult Details(int id)
        {
            //throw new Exception("Exception in details page");
            Employee empDetail = _employeeRepository.GetEmployee(id);
            if (empDetail != null)
            {
                ViewBag.Title = "Details Page";
                return View(_employeeRepository.GetEmployee(id));
            }
            else
            {
                return View("ErrorPage");
            }
        }

        public ViewResult DTOViewData()
        {
            ViewModelDTO pagedata = new ViewModelDTO()
            {
                PageTitle = "DTO Page View",
                empDetails = _employeeRepository.GetAllData()
            };
            return View(pagedata);
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(HomeCreateViewModel newemployee)
        {
            if(ModelState.IsValid)
            {
                string UniqueFileName = null;
                List<string> fileNames = new List<string>();
                if(newemployee.FileName != null && newemployee.FileName.Count > 0)
                {
                    string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "Files");
                    foreach(var file in newemployee.FileName)
                    {
                        UniqueFileName = Guid.NewGuid() + "_" + file.FileName;
                        string filePath = Path.Combine(uploadsFolder, UniqueFileName);
                        file.CopyTo(new FileStream(filePath, FileMode.Create));
                        fileNames.Add(UniqueFileName);
                    }
                    //UniqueFileName = Guid.NewGuid() + "_" + newemployee.FileName.ForEach;
                    //string filePath = Path.Combine(uploadsFolder, UniqueFileName);
                    //newemployee.FileName.CopyTo(new FileStream(filePath, FileMode.Create));
                }
                Employee empValue = _employeeRepository.AddEmployee(new Employee { 
                     Name = newemployee.Name,
                     Department = newemployee.Department,
                     FileName = fileNames.Count() > 0 ? String.Join(';', fileNames) : null
                });

                return new RedirectToActionResult("Details", "Home", new { id = empValue.ID });
            }

            return View();
        }

        public IActionResult Delete(int id)
        {
            if(id > 0)
            {
                var returnValue = _employeeRepository.DeleteEmployee(id);
                return new RedirectToActionResult("Index","Home", new{ returnValue = returnValue });
            }
            return View();
        }

        public IActionResult Download(string link)
        {
            if (System.IO.File.Exists(link))
            {
                var net = new System.Net.WebClient();
                var data = net.DownloadData(link);
                var content = new System.IO.MemoryStream(data);
                var contentType = "APPLICATION/octet-stream";
                string[] temps = link.Split('_').ToArray();
                var fileName = temps.Last();
                return File(content, contentType, fileName);
            }
            return View();
            //return new RedirectToActionResult("Index", "Home", new { returnValue = "" });
        }

        public IActionResult DeleteFile(string link, int id)
        {
            if (System.IO.File.Exists(link))
            {
                System.IO.File.Delete(link);
                Employee employee = _employeeRepository.GetEmployee(id);
                string removedFileNames = "";
                if(employee.FileName.Split(';').ToArray().Length > 1)
                {
                    string[] filenames = employee.FileName.Split(';').ToArray();
                    int counter = 0;
                    foreach(var filename in filenames)
                    {
                        bool flag = false;
                        if (filename.Contains(link.Split('_').LastOrDefault()))
                        {
                            flag = true;
                        }
                        if (flag)
                        {
                            break;
                        }
                        counter++;
                    }
                    filenames = filenames.Where((value, index) => index != counter).ToArray();
                    removedFileNames = String.Join(';', filenames);
                }
                else if (employee.FileName.Contains(link.Split('_').LastOrDefault()))
                {
                    removedFileNames = null;
                }
                employee.FileName = removedFileNames;
                _employeeRepository.UpdateEmployee(employee);
            }

            return new RedirectToActionResult("EditForm", "Home", new { id = id });
            //return Redirect($"{Request.Path.ToString()}{Request.QueryString.Value.ToString()}");
        }

        [HttpGet]
        public IActionResult EditForm(int id)
        {
            
            Employee employee = _employeeRepository.GetEmployee(id);
            if (employee != null)
            {
                List<string> filenames = new List<string>();
                if (employee.FileName != null)
                {
                    filenames = employee.FileName.Split(';').ToList<string>();
                }
                EditViewModel editViewModel = new EditViewModel()
                {
                    ID = employee.ID,
                    Name = employee.Name,
                    Department = employee.Department,
                    wwwpath = Path.Combine(_hostingEnvironment.WebRootPath, "Files\\"),
                    UploadedFiles = filenames.ToList<string>()
                };

                return View(editViewModel);
            }
            else
            {
                _logger.LogError($"The item does not exists");
                return View("ErrorPage");
            }
        }

        [HttpPost]
        public IActionResult EditForm(EditViewModel newemployee)
        {
            if (ModelState.IsValid)
            {
                Employee employee = _employeeRepository.GetEmployee(newemployee.ID);
                string UniqueFileName = null;
                List<string> fileNames = new List<string>();
                if (newemployee.FileName != null && newemployee.FileName.Count > 0)
                {
                    string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "Files");
                    foreach (var file in newemployee.FileName)
                    {
                        UniqueFileName = Guid.NewGuid() + "_" + file.FileName;
                        string filePath = Path.Combine(uploadsFolder, UniqueFileName);
                        file.CopyTo(new FileStream(filePath, FileMode.Create));
                        fileNames.Add(UniqueFileName);
                    }
                }
                if(employee.FileName != null)
                {
                    fileNames.Add(employee.FileName);
                }
                employee.Name = newemployee.Name;
                employee.Department = newemployee.Department;
                employee.FileName = fileNames.Count() > 0 ? String.Join(';', fileNames) : null;

                _employeeRepository.UpdateEmployee(employee);

                return new RedirectToActionResult("Details", "Home", new { id = employee.ID });
            }

            return View();
        }
    }
}
