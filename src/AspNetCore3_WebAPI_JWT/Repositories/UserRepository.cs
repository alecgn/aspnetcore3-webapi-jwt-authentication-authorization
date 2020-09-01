using AspNetCore3_WebAPI_JWT.Interfaces;
using AspNetCore3_WebAPI_JWT.Entities;
using Microsoft.Extensions.Configuration;
using MultiDBHelper;
using MultiDBHelper.Enums;
using System;
using System.Collections.Generic;

namespace AspNetCore3_WebAPI_JWT.Repositories
{
    public class UserRepository : IUserRepository
    {
        #region fields

        private IConfiguration _configuration;
        private RDBMSProvider _rdbmsProvider;
        private string _connectionString;

        #endregion fields


        #region constructors

        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _rdbmsProvider = _configuration.GetValue<RDBMSProvider>("AppSettings:MainRDBMSType");
            _connectionString = _configuration.GetValue<string>("AppSettings:MainRDBMSConnectionString");
        }

        #endregion constructors


        #region public methods

        public User Login(string username, string password)
        {
            var user = Database.QueryFirstOrDefault<User>(_rdbmsProvider, _connectionString,
                "select * from Users where Username = @Username;", new { Username = username }
            );

            if (user != null)
            {
                if (!new CryptHash.Net.Hash.BCrypt().VerifyHash(password, user.Password).Success)
                    user = null;
            }

            return user;
        }


        #region not implemented

        public User SelectById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> SelectAll()
        {
            throw new NotImplementedException();
        }

        public bool Insert(User entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(User entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public bool DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        #endregion not implemented


        #endregion public methods
    }
}