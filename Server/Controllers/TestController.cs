using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PemikFramework.Core.Web.Controllers;
using PemikFramework.Core.Web.Util;
using PemikFramework.DocumentGenerator.Document;
using PemikFramework.DocumentGenerator.Document.Entity;
using PemikFramework.DocumentGenerator.Event;
using PemikFramework.DocumentGenerator.Service;

namespace PannonBlazor.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TestController : BaseController
    {
        private IDocumentGeneratorService _documentGeneratorService;

        public TestController(IDocumentGeneratorService documentGeneratorService, IMimeMappingService mimeMappingService) : base(mimeMappingService)
        {
            _documentGeneratorService = documentGeneratorService;
        }

        [HttpGet]
        [Route("{type}")]
        [AllowAnonymous]
        public async Task<IActionResult> GenerateTestDocument(string type)
        {
            if (type.Equals("excel"))
            {
                var createEvent = new CreateRvdDocumentEvent()
                {
                    RvdDocument = new ExcelRvdDocument { RvdXmlDocument = new TestReportRvdXmlDocument() }
                };
                var result = await _documentGeneratorService.GenerateRvdDocument(createEvent, new RvdDocumentType() { TemplateFileName = "excel_test.xlsx" });
                return File(result.content, result.fileName);
            }
            else
            {
                var createEvent = new CreateRvdDocumentEvent()
                {
                    RvdDocument = new WordRvdDocument { RvdXmlDocument = new TestReportRvdXmlDocument() }
                };
                var result = await _documentGeneratorService.GenerateRvdDocument(createEvent, new RvdDocumentType() { TemplateFileName = "word_test.xml" });
                return File(result.content, result.fileName);
            }
        }
    }

    public class TestReportRvdXmlDocument : RvdXmlDocument
    {
        public override void PrepareData()
        {
            Report.AddSingleChrField("G_WORKDETAILS", "F_WORKERNAME", "Kakszi Lajos");
            Report.AddSingleChrField("G_WORKDETAILS", "F_WORKSITE", "Mucsaröcsöge");
            Report.AddSingleChrField("G_WORKDETAILS", "F_WORKDATE", "2020.06.16.");

            var repUnit = new RepUnit("R_TASKS");
            var tasks = new List<(int id, string name)>()
            {
                (1, "task1"),
                (2, "task2"),
                (3, "task3"),
                (4, "task4"),
                (5, "task5")
            };
            for (int i = 0; i < tasks.Count; i++)
            {
                var task = tasks[i];
                repUnit.AddRepChrField("G_TASK", i, "F_ID", task.id.ToString());
                repUnit.AddRepChrField("G_TASK", i, "F_NAME", task.name);
            }
            Report.AddRepUnit(repUnit);
        }
    }
}
