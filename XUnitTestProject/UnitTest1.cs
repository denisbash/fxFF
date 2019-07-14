using Model;
using Newtonsoft.Json;
using System;
using System.IO;
using Xunit;

namespace XUnitTestProject
{
    public class UnitTest1
    {
        
        [Fact]
        public void InitializeFloatingForward()
        {
            var rootDir = Path.GetDirectoryName(Path.GetDirectoryName(
                Path.GetDirectoryName(Directory.GetCurrentDirectory())));
            var filePath = rootDir + @"/Data/forward_contract_test.txt";
            var str = File.ReadAllText(filePath);
            str = str.Replace("\n", string.Empty);
            var obj = JsonConvert.DeserializeObject<FloatingForward>(str);
        }
        [Fact]
        public void MatchPaymentToContract()
        {
            var rootDir = Path.GetDirectoryName(Path.GetDirectoryName(
                Path.GetDirectoryName(Directory.GetCurrentDirectory())));
            var fileContract = rootDir + @"/Data/forward_contract_test.txt";
            var strContr = File.ReadAllText(fileContract);
            strContr = strContr.Replace("\n", string.Empty);            
            var contract = JsonConvert.DeserializeObject<FloatingForward>(strContr);

            var filePayment = rootDir + @"/Data/payment_test.txt";
            var strPay = File.ReadAllText(filePayment);
            strPay = strPay.Replace("\n", string.Empty);
            var payment = JsonConvert.DeserializeObject<Payment>(strPay);

            var isSuccessful = payment.IsConsistentWithContract(contract);
            Assert.True(isSuccessful);
        }
    }
}
