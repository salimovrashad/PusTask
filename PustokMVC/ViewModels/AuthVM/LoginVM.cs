﻿namespace PustokMVC.ViewModels.AuthVM
{
	public class LoginVM
	{
        public string UsernameOrEmail { get; set; }
		public string Password { get; set; }
		public bool IsRemember { get; set; }
    }
}
