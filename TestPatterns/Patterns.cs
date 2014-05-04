using System;
using System.Collections.Generic;
using System.Configuration;
using Helpers;

namespace Database
{
	public interface IPaymentRepository {
		IEnumerable<IPayment> GetAll ();
		void Add(IPayment payment);
	}
	public class InMemory : IPaymentRepository {
		public void Add(IPayment payment) {
			Config.payments.Add(payment.GetId(),payment);
		}
		public IEnumerable<IPayment> GetAll (){
			return Config.payments.Values;
		}
	}
	public class Postgres : IPaymentRepository {
		public void Add(IPayment payment) {
			//Connection.InsertRecord(payment);
		}
		public IEnumerable<IPayment> GetAll (){
			//Connection.GetAll<IPayment>();
			throw new NotImplementedException();
		}
	}
	public class RepositoryFactory {
		public static IPaymentRepository GetPaymentRepo ()
		{
			if (Config.configuration["database"] == "inmemory") {
				return new InMemory();
			} else {
				return new Postgres();
			}
		}
	}

	public class User {
		public static void Use() {
			var repo = RepositoryFactory.GetPaymentRepo();
			var payments = repo.GetAll();
		}
	}

}

