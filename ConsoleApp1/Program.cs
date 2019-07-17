using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using Model;
using System.IO;
using System.Collections.Generic;
using System.Reflection;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var strContract = File.ReadAllText(_contractFilePath);
            var strPayment = File.ReadAllText(_paymentFilePath);
            MakefxFFPayment(strContract, strPayment);            
        }

        private static string _solutionRootPath;
        private static string _projectRootPath;
        private static string _dataPath;
        private static string _contractFilePath;
        private static string _paymentFilePath;
        private static string _dbContractsFilePath;
        private static string _dbPaymentsFilePath;

        static Program()
        {
            _solutionRootPath = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(
                Path.GetDirectoryName(Directory.GetCurrentDirectory()))));
            _projectRootPath = Path.GetDirectoryName(Path.GetDirectoryName(
            Path.GetDirectoryName(Directory.GetCurrentDirectory())));
            _dataPath = _solutionRootPath + @"/XUnitTestProject/Data";
            _contractFilePath = _dataPath + @"/forward_contract_test.txt";
            _paymentFilePath = _dataPath + @"/payment_test.txt";
            _dbContractsFilePath = _projectRootPath + @"/data/contractDB.txt";
            _dbPaymentsFilePath = _projectRootPath + @"/data/paymentDB.txt";

        }

        private static void MakefxFFPayment(string jsonContract, string jsonPayment)
        {
            var contract = JsonConvert.DeserializeObject<FloatingForward>(jsonContract);
            var payment = JsonConvert.DeserializeObject<Payment>(jsonPayment);
            if (payment.IsConsistentWithContract(contract))
            {
                MakePayment(contract, payment);
                AddPaymentToDB(payment);
                AddContractToDB(contract);
            }
            else
            {
                throw new Exception("Payment is inconsistent with contract");
            }
            

        }
        private static void MakePayment(IContract contract, IPayment payment)
        {
            if (contract.RemainingAmount < payment.Amount.amount)
            {
                throw new Exception("There is not enough fund remaining");
            }
            else
            {
                contract.RemainingAmount -= payment.Amount.amount;
            }
        }

        private static void AddPaymentToDB(IPayment payment)
        {
            Type paymentType = payment.GetType();
            var paymentListType = typeof(List<>).MakeGenericType(paymentType);
            Type[] typesArray = new Type[] { typeof(int), paymentListType };
            var type = typeof(Dictionary<,>).MakeGenericType(typesArray);
            var totDict = File.ReadAllText(_dbPaymentsFilePath);
            var dictPay = JsonConvert.DeserializeObject(totDict, type);

            if (dictPay == null)
            {
                var dictCtors = type.GetConstructors();
                dictPay = dictCtors[0].Invoke(new object[] { });
            }

            object[] prs = new object[] { (int)payment.ContractId, null};
            var getList = type.GetMethod("TryGetValue");
            getList?.Invoke(dictPay, prs);

            var ctors = paymentListType.GetConstructors();
            var newList = prs[1] ?? ctors[0].Invoke(new object[] { });

            object[] prms = new object[] { payment};
            var addMethod = paymentListType.GetMethod("Add");
            addMethod?.Invoke(newList, prms);

            
            //remove the old item from the dictionary
            object[] prmsRemove = new object[] { (int)payment.ContractId };
            var removeMethod = type.GetMethod("Remove", new[] { type.GetGenericArguments()[0]});
            removeMethod?.Invoke(dictPay, prmsRemove);

            //add new item to the dictionary
            object[] prmsAdd = new object[] { (int)payment.ContractId, newList };
            var addToDictMethod = type.GetMethod("Add");
            addToDictMethod?.Invoke(dictPay, prmsAdd);

            var strDictContract = JsonConvert.SerializeObject(dictPay);
            File.WriteAllText(_dbPaymentsFilePath, strDictContract);

        }

        private static void AddContractToDB(IContract contract)
        {
            Type[] typesArray = new Type[] { typeof(int), contract.GetType() };
            var type = typeof(Dictionary<,>).MakeGenericType(typesArray);
            var totDict = File.ReadAllText(_dbContractsFilePath);
            var dictCont = JsonConvert.DeserializeObject(totDict, type);            

            object[] prms = new object[] { (int)contract.Id, contract };
            var addMethod = type.GetMethod("Add");
            addMethod?.Invoke(dictCont, prms);            
                
            var strDictContract = JsonConvert.SerializeObject(dictCont);
            File.WriteAllText(_dbContractsFilePath, strDictContract);
        }
        static ServiceProvider RegisterServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IContract, FloatingForward>();
            //services.AddTransient<HomeController>();
            return services.BuildServiceProvider();
        }
    }
}
