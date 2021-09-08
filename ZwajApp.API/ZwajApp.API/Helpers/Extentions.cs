using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZwajApp.API.Helpers
{
    //  هذه لكلاس حتى اعمل اي اكستنشن ميثود بدي ياها بضعها هنا 
    public static class Extentions
    {

       // معني ذيس   تعني الاكستنشن نفسها للرسبونس هيدر
        public static void AddApplicationError(this HttpResponse Response,string message)
        {
            //انا هنا ببني الرسبونس هيدر 
            Response.Headers.Add("Application-Error", message);
            // تعريف بالابلكيشن ايرور
            Response.Headers.Add("Access-Control-Expose-Header", "Application-Error");
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
        }

        public static int CalculateAge(this DateTime datetime)
        {
            var age = DateTime.Today.Year - datetime.Year;
            if (datetime.AddYears(age) > DateTime.Today)
            {
                age--;
            }
            return age;
        }
    }
}
