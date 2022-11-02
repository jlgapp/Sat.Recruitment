using Sat.Recruitment.Application.Contracts;
using Sat.Recruitment.Application.Models.Common;
using Sat.Recruitment.Domain.Users;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Sat.Recruitment.Infrastructure.Users
{
    public class HandleUser : IHandleUser
    {
        public Result CreateUser(User newUser)
        {
            var reader = ReadUsersFromFile();
            try
            {

                List<User> _users = new List<User>();
                string money = newUser.Money.ToString();
                
                if (newUser.UserType == "Normal")
                {
                    if (decimal.Parse(money) > 100)
                    {
                        var percentage = Convert.ToDecimal(0.12);
                        //If new user is normal and has more than USD100
                        var gif = decimal.Parse(money) * percentage;
                        newUser.Money = newUser.Money + gif;
                    }
                    if (decimal.Parse(money) < 100)
                    {
                        if (decimal.Parse(money) > 10)
                        {
                            var percentage = Convert.ToDecimal(0.8);
                            var gif = decimal.Parse(money) * percentage;
                            newUser.Money = newUser.Money + gif;
                        }
                    }
                }
                if (newUser.UserType == "SuperUser")
                {
                    if (decimal.Parse(money) > 100)
                    {
                        var percentage = Convert.ToDecimal(0.20);
                        var gif = decimal.Parse(money) * percentage;
                        newUser.Money = newUser.Money + gif;
                    }
                }
                if (newUser.UserType == "Premium")
                {
                    if (decimal.Parse(money) > 100)
                    {
                        var gif = decimal.Parse(money) * 2;
                        newUser.Money = newUser.Money + gif;
                    }
                }




                //Normalize email
                var aux = newUser.Email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

                var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

                aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

                newUser.Email = string.Join("@", new string[] { aux[0], aux[1] });

                while (reader.Peek() >= 0)
                {
                    var line = reader.ReadLineAsync().Result;
                    var user = new User
                    {
                        Name = line.Split(',')[0].ToString(),
                        Email = line.Split(',')[1].ToString(),
                        Phone = line.Split(',')[2].ToString(),
                        Address = line.Split(',')[3].ToString(),
                        UserType = line.Split(',')[4].ToString(),
                        Money = decimal.Parse(line.Split(',')[5].ToString()),
                    };
                    _users.Add(user);
                }

                try
                {
                    var isDuplicated = false;
                    foreach (var user in _users)
                    {
                        if (user.Email == newUser.Email
                            ||
                            user.Phone == newUser.Phone)
                        {
                            isDuplicated = true;
                        }
                        else if (user.Name == newUser.Name)
                        {
                            if (user.Address == newUser.Address)
                            {
                                isDuplicated = true;
                                throw new Exception("User is duplicated");
                            }

                        }
                    }

                    if (!isDuplicated)
                    {
                        Debug.WriteLine("User Created");

                        return new Result()
                        {
                            IsSuccess = true,
                            Errors = "User Created"
                        };
                    }
                    else
                    {
                        Debug.WriteLine("The user is duplicated");

                        return new Result()
                        {
                            IsSuccess = false,
                            Errors = "The user is duplicated"
                        };
                    }
                }
                catch
                {
                    Debug.WriteLine("The user is duplicated");
                    return new Result()
                    {
                        IsSuccess = false,
                        Errors = "The user is duplicated"
                    };
                }

                return new Result()
                {
                    IsSuccess = true,
                    Errors = "User Created"
                };
            }
            catch (Exception ex)
            {

                return new Result()
                {
                    IsSuccess = false,
                    Errors = "Error " + ex.Message
                };
            }
            finally
            {
                reader.Close();
            }
        }

        public StreamReader ReadUsersFromFile()
        {
            var path = Directory.GetCurrentDirectory() + "/Files/Users.txt";

            FileStream fileStream = new FileStream(path, FileMode.Open);

            StreamReader reader = new StreamReader(fileStream);
            return reader;
        }
    }
}
