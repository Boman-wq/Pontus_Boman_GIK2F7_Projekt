using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Dtos;
using Catalog.Models;
using Catalog.Reposotories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace Catalog.Controllers
{
    [ApiController]
    [Route("form")]
    public class FormController : ControllerBase
    {
        private readonly IFormRepository repository;
        private readonly ILogger<FormController> logger;
        public FormController(IFormRepository repository, IWebHostEnvironment environmnet, ILogger<FormController> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }
        /// <summary>
        /// Get
        /// /form
        /// gets all forms in database
        /// </summary>
        /// <returns></returns>
        //GET/form
        [HttpGet]
        public async Task<IEnumerable<FormDto>> GetForms()
        {
            var forms = (await repository.GetForms()).Select(form => form.AsDto());
            logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Retrived {forms.Count()} forms");
            return forms;
        }

        /// <summary>
        /// Get
        /// form/{id}
        /// gets spesific form by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //Get/form/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<FormDto>> GetForm(Guid id)
        {
            var form = await repository.GetForm(id);
            if (form is null){
                logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")} GetRequest: {NotFound()}");
                return NotFound();
            }
            logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")} returned {form.Id}");
            return form.AsDto();
        }

        /// <summary>
        /// POST
        /// /form
        /// Creates new form
        /// </summary>
        /// <param name="formDto"></param>
        /// <returns></returns>
        //POST/form
        [HttpPost]
        public async Task<ActionResult<FormDto>> CreateForm (CreateFormDto formDto)
        {
            Form form = new Form()
            {
                Id = Guid.NewGuid(),
                Kurs = formDto.Kurs,
                Grupp = formDto.Grupp,
                Moment = formDto.Moment,
                Lärare = formDto.Lärare,
                Lokal = formDto.Lokal,
                Information = formDto.Information,
                StartTid = formDto.StartTid,
                SlutTid = formDto.SlutTid,
                Bild = formDto.Bild
            };
            await repository.CreateForm(form);
            logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")} Form {form.Id} created");
            return CreatedAtAction(nameof(GetForm), new { id = form.Id}, form.AsDto());
        }

        /// <summary>
        /// Put
        /// /form/{id}
        /// Update existing form
        /// </summary>
        /// <param name="id"></param>
        /// <param name="formDto"></param>
        /// <returns></returns>
        //PUT/form/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateForm(Guid id, UpdateFormDto formDto)
        {
            var existingForm = await repository.GetForm(id);
            if (existingForm is null)
                return NotFound();

            existingForm.Kurs = formDto.Kurs;
            existingForm.Grupp = formDto.Grupp;
            existingForm.Moment = formDto.Moment;
            existingForm.Lärare = formDto.Lärare;
            existingForm.Lokal = formDto.Lokal;
            existingForm.Information = formDto.Information;
            existingForm.StartTid = formDto.StartTid;
            existingForm.SlutTid = formDto.SlutTid;
            existingForm.Bild = formDto.Bild;
            
            await repository.UpdateForm(existingForm);
            return NoContent();
        }
        
        /// <summary>
        /// Delete
        /// /form/{id}
        /// deletes form from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //Delete/form/id
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteForm(Guid id)
        {
            var existingForm = await repository.GetForm(id);
            if (existingForm is null){
                logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")} DeleteRequest {NotFound()}");
                return NotFound();
            }
            await repository.DeleteForm(id);
            logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")} Form {id} deleted");
            return NoContent();
        }

        /// <summary>
        /// Get
        /// /form/search
        /// Search for Lärare in databsae
        /// </summary>
        /// <param name="lärare"></param>
        /// <returns></returns>

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Form>>> Search(string lärare)
        {
           var forms = (await repository.Search(lärare)).Select(form => form.AsDto());
            if (forms is null){
                logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")} SearchRequest:{NotFound()}");
                return NotFound();
            }
            logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")} Retrived {forms.Count()} SearchRequests");
            return Ok(forms);
        }
    }
}