using be.roegiersbvba.Customers.Commands;
using be.roegiersbvba.Customers.Dto;
using System;
using System.Reflection.Metadata;

namespace be.roegiersbvba.Customers.Application
{
    public interface IHandle<U, out T> where U : ICommand
        where T : IDto
    {
        void Handle(U command);
        T HandleAndReturn(U command);
    }
}