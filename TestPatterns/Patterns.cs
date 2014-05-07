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
		public Dictionary<int, IPayment> payments = new Dictionary<int, IPayment>();
		public void Add(IPayment payment) {
			payments.Add(payment.GetId(),payment);
		}
		public IEnumerable<IPayment> GetAll (){
			return payments.Values;
		}
	}
	public class Postgres : IPaymentRepository {
		public void Add(IPayment payment) {
			throw new NotImplementedException();
		}
		public IEnumerable<IPayment> GetAll (){
			throw new NotImplementedException();
		}
	}
	public interface IDBConnection { }
	public class DBConnection : IDBConnection{ }
	public class RepositoryFactory {
		IDBConnection con;
		public RepositoryFactory(IDBConnection con) {
			this.con = con;
		}
		public RepositoryFactory() : this(new DBConnection()) { }
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

