﻿using Library.Models;
using Library.Repositories.Abstract;

namespace Library.Repositories.Implementation
{
    public class AuthorService : IAuthorService
    {
        private readonly DatabaseContext context;
        public AuthorService(DatabaseContext context)
        {
            this.context = context;
        }
        public bool Add(Author model)
        {
            try
            {
                context.Author.Add(model);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var data = this.FindById(id);
                if (data == null)
                    return false;
                context.Author.Remove(data);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Author FindById(int id)
        {
            return context.Author.Find(id);
        }

        public IEnumerable<Author> GetAll()
        {
            return context.Author.ToList();
        }

        public bool Update(Author model)
        {
            try
            {
                context.Author.Update(model);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
