using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Payu_inter
{
    public class PayUClass
    {
        private static Random random = new Random();

        public static string RandomString()
        {
            const string Chars = "01234567891112131415161718192021222324252627282930";
            return new string(Enumerable.Repeat(Chars, 9)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public OnlinePayment GetpayuData(decimal amount, string name)
        {
            OnlinePayment _data = new OnlinePayment();
            string strCRN = string.Empty;
            string strAMT = string.Empty;
            string strRID = string.Empty;

            _data.Amount = amount;
            _data.Name = name;

            strCRN = RandomString();                    ////    "213851359"; // random generating
            strRID = RandomString();

            _data.OnlineTrackId = strRID;
            string strHash = Generatehash512(strRID + DateTime.Now);
            _data.ReferenceNumber = strHash.ToString().Substring(0, 20);
            _data.CRN = strCRN;

            _data.MerchantID = "test"; //// Your merchantid 
            _data.Encryptedstring = "test"; ////Your salt
            _data.TransactionDesc = "Application";

            var hashVarsSeq = "key|txnid|amount|productinfo|firstname|email|udf1|udf2|udf3|udf4|udf5|udf6|udf7|udf8|udf9|udf10".Split('|'); // spliting hash sequence from config
            var hash_string = _data.MerchantID + "|" + _data.OnlineTrackId + "|" + _data.Amount.ToString("g29") + "|" + _data.TransactionDesc + "|" + _data.Name + "|";
            hash_string += _data.Email + "|" + _data.CRN + "||||||||||";
            hash_string += _data.Encryptedstring; //// appending SALT

            _data.Checksum = Generatehash512(hash_string).ToLower();

            return _data;
        }

        public class OnlinePayment
        {
            public long? Id { get; set; }

            public int ApplicantId { get; set; }

            public string RegistationNumber { get; set; }

            public string ReferenceNumber { get; set; }

            public decimal Amount { get; set; }

            public string ChelanNo { get; set; }

            public string OnlineTrackId { get; set; }

            public string CRN { get; set; }

            public DateTime? RemitanceDate { get; set; }

            public int PaymentStatus { get; set; }

            public string Encryptedstring { get; set; }

            public string Tranactionsurl { get; set; }

            public string Name { get; set; }

            public string MobileNumber { get; set; }

            public string Address { get; set; }

            public string City { get; set; }

            public string Pincode { get; set; }

            public string Email { get; set; }

            public string State { get; set; }

            public string Checksum { get; set; }

            public bool IsSucess { get; set; }

            public string Status { get; set; }

            public string Returnurl { get; set; }

            public long MangmntNo { get; set; }

            public long? MangmntNo1 { get; set; }

            public long? MangmntNo2 { get; set; }

            public int GateWayId { get; set; }

            public int Preferance { get; set; }

            public string MerchantID { get; set; }

            public bool Worldline { get; set; }

            public bool HDFC { get; set; }

            public bool PayU { get; set; }

            public string TransactionDesc { get; set; }

            public bool ExeFlag { get; set; }

            public bool FacultyApplication { get; set; }

            public int OtherFeeId { get; set; }

            public int TranscriptId { get; set; }

            public int ProvisionalId { get; set; }

            public decimal College { get; set; }

            public decimal University { get; set; }

            public int Isexist { get; set; }

            public int Programid { get; set; }

            public string Imageurl { get; set; }

            public int Typeid { get; set; }

            public string CreatedBy { get; set; }

            public string ErrorMessage { get; set; }
        }

        public string Generatehash512(string text)
        {
            byte[] message = Encoding.UTF8.GetBytes(text);

            UnicodeEncoding uE = new UnicodeEncoding();
            byte[] hashValue;
            SHA512Managed hashString = new SHA512Managed();
            string hex = string.Empty;
            hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hex += string.Format("{0:x2}", x);
            }

            return hex;
        }
    }
}
