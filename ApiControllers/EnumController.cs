using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using teleRDV.Models;

namespace teleRDV.Controllers
{
    [Route("api/[controller]")]
    public class EnumController : Controller
    {
        [Route("address")]
        [HttpGet]
        public ActionResult GetAddress()
        {
            var enumVals = new List<object>();

            foreach (var item in Enum.GetValues(typeof(AddressType)))
            {
                enumVals.Add(new
                {
                    key = (int)item,
                    value = item.ToString()
                });
            }

            return Ok(enumVals);
        }

        [Route("info")]
        [HttpGet]
        public ActionResult GetInfo()
        {
            var enumVals = new List<object>();

            foreach (var item in Enum.GetValues(typeof(InfoType)))
            {
                enumVals.Add(new
                {
                    key = (int)item,
                    value = item.ToString()
                });
            }

            return Ok(enumVals);
        }

        [Route("phone")]
        [HttpGet]
        public ActionResult GetPhone()
        {
            var enumVals = new List<object>();

            foreach (var item in Enum.GetValues(typeof(PhoneType)))
            {
                enumVals.Add(new
                {
                    key = (int)item,
                    value = item.ToString()
                });
            }

            return Ok(enumVals);
        }

        [Route("weekdays")]
        [HttpGet]
        public ActionResult GetWeekDays()
        {
            var enumVals = new List<object>();
            foreach (var item in Enum.GetValues(typeof(DayOfWeek)))
            {
                enumVals.Add(new
                {
                    key = (int)item,
                    value = item.ToString()
                });
            }

            return Ok(enumVals);
        }
    }
}