using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendCase.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BackendCase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InformationController : ControllerBase
    {
        private readonly ILogger _logger;

        public InformationController(ILogger<InformationController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public ActionResult<Information> PostInformation(Information information)
        {
            int currentTimestampInEpoch = ConvertDateToEpoch(DateTime.Now);
            int informationTimestampInEpoch = ConvertDateToEpoch(information.Timestamp);

            // Validates the information and proceeds if valid, else return a 400 status code
            TryValidateModel(information);

            // Most likely spam if honeypot has been filled out
            if (information.Honeypot != null)
            {
                _logger.LogInformation("Information has been received, but honeypot was filled out");
                return BadRequest("Likely to be spam");
            }

            // Most like spam if form was submitted within two seconds of the form loading
            //if ((currentTimestampInEpoch - informationTimestampInEpoch) < 2)
            //{
            //    _logger.LogInformation("Information has been received, but the form was submitted too fast");
            //    return BadRequest("Likely to be spam");
            //}

            // The information should be valid and most likely not spam, return information and send notification email to io@nettbureau.no
            return information;
        }

        private int ConvertDateToEpoch(DateTime date)
        {
            return (int)(date.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }
    }
}