using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRUDApi.Data;
using CRUDApi.Services;
using Microsoft.Extensions.Options;
using CRUDApi.EmailHelper;
using CRUDApi.Models.EmailModels;

namespace CRUDApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmailModelsController : ControllerBase
    {
        private readonly EmailContext _context;
        private readonly EmailSender _emailSender;

        public EmailModelsController(EmailContext context, IOptions<EmailConfiguration> EmailOption)
        {
            _context = context;
            _emailSender = new EmailSender(EmailOption);
        }

        // GET: api/EmailModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmailModel>>> GetEmailModels()
        {
            return await _context.EmailModels.ToListAsync();
        }

        // GET: api/EmailModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmailModel>> GetEmailModel(string id)
        {
            var emailModel = await _context.EmailModels.FindAsync(id);

            if (emailModel == null)
            {
                return NotFound();
            }

            return emailModel;
        }

        //post to Email

        [HttpPost("send_email")]
        public async Task<ActionResult> Sender(EmailModel emailModelP)
        {
            if (!ModelState.IsValid) return BadRequest();
            _context.EmailModels.Add(emailModelP);
            await _context.SaveChangesAsync();

            var message = new Message(new string[] { emailModelP.Email }, "Test email", "This is the content from our email.");
            await _emailSender.SendEmail(message);

            return NoContent();
        }

        /*[HttpPost("Forgot-password")]
        public async Task<ActionResult> SendForgotPassword(EmailModel emailModelP)
        {
            if (!ModelState.IsValid) return BadRequest();
            _context.EmailModels.Add(emailModelP);
            await _context.SaveChangesAsync();

            var message = new Message(new string[] { emailModelP.Email }, "Test email", "This is the content from our email.");
            await _emailSender.SendEmail(message);

            return NoContent();
        }*/
    }
}
