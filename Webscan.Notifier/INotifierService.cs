using MimeKit;
using System.Net.Http;
using System.Threading.Tasks;

namespace Webscan.Notifier
{
    public interface INotifierService
    {
        /// <summary>
        /// SendTextEmail sends an email of plaintext, specifically designed for plain text emails.
        /// </summary>
        /// <param name="from">From Email Address</param>
        /// <param name="to">Who this email is to</param>
        /// <param name="subject">Subject line of the email</param>
        /// <param name="text">Text Body of the email</param>
        /// <returns></returns>
        Task SendTextEmail(string from, string to, string subject, string text);


        /// <summary>
        /// SendTextEmail sends an email of plaintext, specifically designed for plain text emails.
        /// </summary>
        /// <param name="from">From Email Address</param>
        /// <param name="to">Who this email is to</param>
        /// <param name="subject">Subject line of the email</param>
        /// <param name="html">HTML Body of the email</param>
        Task SendHtmlEmail(string from, string to, string subject, string html);

    }
}
