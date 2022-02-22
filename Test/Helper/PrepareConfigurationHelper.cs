using Microsoft.Extensions.Configuration;
using System.Collections.Generic;


namespace Test.Helper
{
    internal class PrepareConfigurationHelper
    {
        public readonly IConfiguration Config;
        public PrepareConfigurationHelper()
        {
            var myConfiguration = new Dictionary<string, string>
            {
                {"JWT:secret", "asdfwner7654345D&df3"},
                {"SqlConnectionString", ""},
                {"MailParams:SendGridKey", ""},
                {"MailParams:FromMail", ""},
                {"MailParams:FromMailDescription", ""},
                {"MailParams:PathTemplate", ""},
                {"MailParams:ReplaceMailTitle", "{mail_title}"},
                {"MailParams:ReplaceMailBody", "{mail_body}"},
                {"MailParams:ReplaceMailContact", "{mail_contact}"},
                {"MailParams:WelcomeMailTitle", "Bienvenido a Ong Somos Mas!"},
                {"MailParams:WelcomeMailBody", "<p>¡Te damos la bienvenida a Ong Somos Mas!</p><p>Ahora puedes acceder a nuestro sitio, conocer nuestro trabajo, actividades y a nuestros colaboradores.</p>"},
                {"MailParams:WelcomeMailContact", "somosfundacionmas@gmail.com"},
                {"SendGridAPIKey","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"}
            };

            Config = new ConfigurationBuilder()
                .AddInMemoryCollection(myConfiguration)
                .Build();
        }
    }
}
