using RazorEngine.Configuration;
using RazorEngine.Templating;
using System;
using System.Collections.Generic;
using System.IO;
using Ritzau.Extension;
using System.Security.Cryptography;
using System.Text;
using RazorEngine;

namespace RazorEngineHelper
{
   
    public class RazorEngineParser
    {
        private readonly IRazorEngineService _service;

        public RazorEngineParser(string templatePath)
        {
            var templatePathForWebApp = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin", templatePath);

            var templatePathForConsoleApp = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, templatePath);

            var config = new TemplateServiceConfiguration
            {
                TemplateManager = new ResolvePathTemplateManager(new[] { templatePathForWebApp, templatePathForConsoleApp })
            };
            _service = RazorEngineService.Create(config);
        }

        public virtual string CreateEmail<T>(string templateName, T model)
        {
            var templateKey = GenerateHashForTemplate(templateName);
            if (Engine.Razor.IsTemplateCached(templateKey, null))
            {
                try
                {
                    return _service.RunCompile(templateKey, typeof(T), model);
                }
                catch (Exception e)
                {

                    throw e;
                }
            }
            else
            {
                try
                {
                    return _service.RunCompile(templateName, templateKey, null, model);
                }
                catch (Exception e)
                {

                    throw e;
                }
            }
        }

        public void DisposeRazorEngineService()
        {
            _service.Dispose();
        }

        public string GenerateHashForTemplate(string template)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(template);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                var sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("AGENDA"));
                }
                return sb.ToString();
            }
        }
    }
}
