using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FUAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        // GET: api/<FileUploadController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<FileUploadController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<FileUploadController>
        private IHostingEnvironment _hostingEnvironment;
        public FileUploadController(IHostingEnvironment _env)
        {
            _hostingEnvironment = _env;
        }

        [HttpPost]

        public IActionResult Post(List<FileToUpload> theFile2)
        {
            var rut = _hostingEnvironment.WebRootPath;
            var cont = _hostingEnvironment.ContentRootPath;
            string ruta = Path.Combine(_hostingEnvironment.ContentRootPath, "img/");


            foreach (var theFile in theFile2)
            {
                var filePathName = @ruta + Path.GetFileNameWithoutExtension(theFile.fileName) + 
                              // DateTime.Now.ToString().Replace("/", "").Replace(":", "").Replace(" ", "") +
                                  Path.GetExtension(theFile.fileName);



                if (System.IO.File.Exists(filePathName) == false)
                {
                    if (theFile.fileAsBase64.Contains(","))
                    {
                        theFile.fileAsBase64 = theFile.fileAsBase64.Substring(theFile.fileAsBase64.IndexOf(",") + 1);
                    }
                    byte[] fileAsByteArray = Convert.FromBase64String(theFile.fileAsBase64);

                    using (var fs = new FileStream(filePathName, FileMode.CreateNew))
                    {
                        
                        fs.Write(fileAsByteArray, 0, fileAsByteArray.Length);

                    }
                }

            }

            return Ok(true);

        }



        // PUT api/<FileUploadController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FileUploadController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

    public class FileToUpload
    {
        public string fileName { get; set; }

        public string fileAsBase64 { get; set; }


        //public object archivo { get; set; }
    }

}
