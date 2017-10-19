using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DinkToPdf.Contracts;
using DinkToPdf;

namespace WebHtmlToPdfSample.Controllers
{
    [Route("api/Convert")]
    public class ConvertController : Controller
    {
        private IConverter _converter;

        public ConvertController(IConverter converter)
        {
            _converter = converter;
        }

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    PaperSize = PaperKind.A4Plus,
                    Orientation = Orientation.Portrait,
                },

                Objects = {
                    new ObjectSettings()
                    {
                        PagesCount=true,
                         FooterSettings = {  Right = "Page [page] of [toPage]", Line = true, Spacing = 2.812 },
                        Page = "http://google.com/",
                    },
                     new ObjectSettings()
                    {
                        PagesCount=true,
                         FooterSettings = {  Right = "Page [page] of [toPage]", Line = true, Spacing = 2.812 },
                        Page = "http://www.color-hex.com/",

                    }
                }
            };

            byte[] pdf = _converter.Convert(doc);


            return new FileContentResult(pdf, "application/pdf");
        }
    }
}