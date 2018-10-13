using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using viSearch.Models;

namespace viSearch.Controllers
{
    [Route("api/[controller]")]
    public class SearchController : Controller
    {

        #region properties
        #endregion

        // GET api/search
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/search/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/search/test
        [HttpPost("test")]
        public IActionResult Test([FromBody]string value)
        {
            return Ok(new { Result = true, Message="success" });
        }

        // POST api/search/test
        [HttpPost("save")]
        public void Save([FromBody]SearchEngine searchEngine)
        {
        }


        // POST api/search/test
        [HttpPost("generate")]
        public IActionResult GenerateKeyAndSecret()
        {
            return Ok(new { Key = GetUniqueKey(12), Secret = Guid.NewGuid().ToString("N") });
        }

        // POST api/search/delete
        [HttpPost("delete")]
        public void Delete([FromBody]SearchEngineDeleteInput input)
        {
        }

        private static string GetUniqueKey(int maxSize)
        {
            char[] chars = new char[62];
            chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[1];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetNonZeroBytes(data);
                data = new byte[maxSize];
                crypto.GetNonZeroBytes(data);
            }
            StringBuilder result = new StringBuilder(maxSize);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }
    }
}
