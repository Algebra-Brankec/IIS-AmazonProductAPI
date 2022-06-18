using IIS_AmazonProductAPI.Engine;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace IIS_AmazonProductAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductReviewController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post(IFormFile xmlFile, [FromHeader] bool isXsd)
        {
            //https://docs.microsoft.com/en-us/dotnet/standard/data/xml/xml-schema-xsd-validation-with-xmlschemaset
            (bool, string) result;


            XmlXsdValidator xmlXsdValidator = new XmlXsdValidator();
            XmlRngValidator xmlRngValidator = new XmlRngValidator();

            result = isXsd ? xmlXsdValidator.IsValid(xmlFile.OpenReadStream()) : xmlRngValidator.IsValid(xmlFile.OpenReadStream());

            if (result.Item1)
            {
                return Ok();
            }

            return BadRequest(result.Item2);
        }
    }
}
