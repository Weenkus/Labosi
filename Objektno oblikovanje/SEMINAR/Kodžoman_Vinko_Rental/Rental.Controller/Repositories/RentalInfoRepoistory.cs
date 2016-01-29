﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental
{
    public class RentalInfoRepository : IRentalInfoRepository
    {
        private static RentalInfoRepository instance;
        private IList<RentalInformation> _rentalInfoList = new List<RentalInformation>();

        private RentalInfoRepository() { }

        public static RentalInfoRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RentalInfoRepository();
                }
                return instance;
            }
        }

        private void LoadData()
        {
            using (var session = NHibernateService.SessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    _rentalInfoList = session.CreateCriteria(typeof(RentalInformation)).List<RentalInformation>();
                }
            }
        }

        public int Count()
        {
            LoadData();
            return _rentalInfoList.Count;
        }

        public RentalInformation Get(int id)
        {
            LoadData();
            return _rentalInfoList.Where(p => p.Id == id).SingleOrDefault();
        }

        public RentalInformation Get(RentalInformation rentalInfo)
        {
            LoadData();
            return _rentalInfoList.Where(p => p.Equals(rentalInfo)).SingleOrDefault();
        }

        public IList<RentalInformation> GetAll()
        {
            return _rentalInfoList;
        }

        public RentalInformation GetByIndex(int index)
        {
            LoadData();
            if (index < _rentalInfoList.Count)
                return _rentalInfoList.ElementAt(index);
            else
                return null;
        }

        public void Add(RentalInformation rentalInfo)
        {
            using (var session = NHibernateService.SessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(rentalInfo);
                    transaction.Commit();
                }
            }
        }

        public void Remove(int id)
        {
            LoadData();
            if (_rentalInfoList.Any(x => x.Id == id))
            {
                using (var session = NHibernateService.SessionFactory.OpenSession())
                {
                    using (var transaction = session.BeginTransaction())
                    {
                        session.Delete(_rentalInfoList.Where(x => x.Id == id).SingleOrDefault());
                        transaction.Commit();
                    }
                }
            }
        }

        public void Remove(RentalInformation rentalInfo)
        {
            LoadData();
            if (_rentalInfoList.Any(x => x.Id == rentalInfo.Id))
            {
                using (var session = NHibernateService.SessionFactory.OpenSession())
                {
                    using (var transaction = session.BeginTransaction())
                    {

                        session.Delete(_rentalInfoList.Where(x => x.Id == rentalInfo.Id).SingleOrDefault());
                        transaction.Commit();
                    }
                }
            }
        }

        public void Clear()
        {
            LoadData();
            _rentalInfoList.Clear();
        }

        public bool Contains(RentalInformation rentalInfo)
        {
            LoadData();
            return _rentalInfoList.Any(p => p.Equals(rentalInfo));
        }

        public void Update(RentalInformation rentalInfo)
        {
            using (var session = NHibernateService.SessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {

                    session.SaveOrUpdate(rentalInfo);
                    transaction.Commit();
                }
            }
        }
    }
}
