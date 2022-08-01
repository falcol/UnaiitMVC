using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace UnaiitMVC.Areas.Identity.Views.Role
{
    public class _StatusMessage : PageModel
    {
        private readonly ILogger<_StatusMessage> _logger;

        public _StatusMessage(ILogger<_StatusMessage> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}