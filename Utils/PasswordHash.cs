using System.Text;
using System.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace online_recruitment.Utils
{
	public class PasswordHash
	{
		public static string GetSHA(string pwd)
		{
			var crypt = new SHA256Managed();
			var hash = new StringBuilder();

			byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(pwd));

			foreach (var item in crypto)
			{
				hash.Append(item.ToString("x2"));
			}
            return hash.ToString();
		}
	}
}