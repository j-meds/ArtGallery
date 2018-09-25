﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtGallery.ViewModels;
using ArtGallery.Services;

namespace ArtGallery.Controllers
{
    public class AppController : Controller
    {
        private readonly IMailService _mailService;

        public AppController( IMailService mailService)
        {
            _mailService = mailService;
        }

        public IActionResult Index()
        {

            //throw new InvalidOperationException("don't work");    
            return View();
        }

        [HttpGet("contact")]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost("contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            if(ModelState.IsValid)
            {
                // Send the email
                _mailService.SendMessage(model.Email, model.Subject, model.Message);
                ViewBag.UserMessage = "Mail Sent!";
                ModelState.Clear();
            }

            return View();
        }

        public IActionResult About()
        {
            ViewBag.Title = nameof(About);
            return View();
        }
    }
}
