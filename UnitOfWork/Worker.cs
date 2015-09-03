using System;
using UnitOfWork.Repositories;

namespace UnitOfWork
{
    public class Worker : IDisposable
    {
        public Worker()
        {
            DbContext = new Models.NorthwindEntities();
        }

        public int SaveChanges()
        {
            return DbContext.SaveChanges();
        }
        public void Dispose()
        {
            if (DbContext != null)
            {
                DbContext.Dispose();
            }
            GC.SuppressFinalize(this);
        }
        public void RefreshContext()
        {
            this.DbContext.Dispose();
            this.DbContext = null;
            this.DbContext = new Models.NorthwindEntities();
        }

        public Models.NorthwindEntities DbContext { get; private set; }


        private CategoryRepository _categoryRepository;
        public CategoryRepository CategoryRepository
        {
            get
            {
                return _categoryRepository ?? (_categoryRepository = new CategoryRepository { Worker = this });
            }
        }


        private ProductRepository _productRepository;
        public ProductRepository ProductRepository
        {
            get
            {
                return _productRepository ?? (_productRepository = new ProductRepository { Worker = this });
            }
        }


        private OrderRepository _orderRepository;
        public OrderRepository OrderRepository
        {
            get
            {
                return _orderRepository ?? (_orderRepository = new OrderRepository { Worker = this });
            }
        }
        
    }
}