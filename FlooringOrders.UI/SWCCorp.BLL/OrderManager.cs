using SWCCorp.Models;
using SWCCorp.Models.Interfaces;
using SWCCorp.Models.Responses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCCorp.BLL
{
    public class OrderManager
    {
        private IOrderRepository _orderRepository;
        private IProductRepo _productRepository;
        private ITaxRepo _taxRepository;

        public OrderManager(IOrderRepository orderRepository, IProductRepo productRepository, ITaxRepo taxRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _taxRepository = taxRepository;
        }

        public TaxLookupResponse LoadTaxes()
        {
            TaxLookupResponse taxResponse = new TaxLookupResponse();
            taxResponse.Taxes = _taxRepository.GetTaxes();

            if (taxResponse.Taxes == null)
            {
                taxResponse.Success = false;
                taxResponse.Message = "Is not a state we sell in";
            }
            else
            {
                taxResponse.Success = true;
            }
            return taxResponse;
        }

        public ProductLookupResponse LoadProducts()
        {
            ProductLookupResponse productResponse = new ProductLookupResponse();
            productResponse.Products = _productRepository.GetProducts();

            if(productResponse.Products == null)
            {
                productResponse.Success = false;
                productResponse.Message = "Is not a valid product type";
            }
            else
            {
                productResponse.Success = true;
            }
            return productResponse;
        }

        public OrderDateLookupResponse OrderLookupDate(DateTime orderDate)
        {
            OrderDateLookupResponse orderDateResponse = new OrderDateLookupResponse();
            orderDateResponse.ListOfOrders = _orderRepository.GetAllOrdersActual(orderDate);

            if (orderDateResponse.ListOfOrders == null || orderDateResponse.ListOfOrders.Count() == 0)
            {
                orderDateResponse.Success = false;
                orderDateResponse.Message = $"There were no orders in {orderDate.ToString("MM/dd/yyyy")}";
            }
            else
            {
                orderDateResponse.Success = true;
            }
            return orderDateResponse;
        }

        public DeleteOrderResponse DeleteOrder(DateTime orderDate, int orderNumber)
        {
            DeleteOrderResponse deleteOrderResponse = new DeleteOrderResponse();
            deleteOrderResponse.DeletedOrder = _orderRepository.LoadOrder(orderDate, orderNumber);
            deleteOrderResponse.Success = _orderRepository.Delete(orderDate, orderNumber);

            if (!deleteOrderResponse.Success)
            {
                deleteOrderResponse.Message = "The Order was not successfully removed. Please try again.";
            }
            return deleteOrderResponse;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _productRepository.GetProducts();
        }

        public AddAnOrderResponse AddOrder(DateTime orderDate, string customerName, Tax state, Product productType, decimal area, int orderNumber)
        {
            AddAnOrderResponse addOrderResponse = new AddAnOrderResponse();

            addOrderResponse.StateTax = _taxRepository.LoadTaxes(state.Abbreviation);
            addOrderResponse.ProductType = _productRepository.LoadProducts(productType.Name);

            if (addOrderResponse.StateTax == null)
            {
                addOrderResponse.Success = false;
                addOrderResponse.Message = $"{state} is not a state we sell.";
            }
            else if (addOrderResponse.ProductType == null)
            {
                addOrderResponse.Success = false;
                addOrderResponse.Message = $"{productType} is not a valid product type.";
            }
            else
            {
                addOrderResponse.Success = true;
            }

            if (addOrderResponse.Success)
            {
                addOrderResponse.AddedOrder = new Order();

                addOrderResponse.AddedOrder.OrderDate = orderDate;
                addOrderResponse.AddedOrder.CustomerName = customerName;
                addOrderResponse.AddedOrder.State = state.Abbreviation;

                addOrderResponse.AddedOrder.TaxRate = addOrderResponse.StateTax.TaxRate;
                addOrderResponse.AddedOrder.ProductType = productType.Name;
                addOrderResponse.AddedOrder.CostPerSquareFoot = addOrderResponse.ProductType.MaterialUnitCost;
                addOrderResponse.AddedOrder.LaborCostPerSquareFoot = addOrderResponse.ProductType.LaborUnitCost;

                addOrderResponse.AddedOrder.Area = area;
                addOrderResponse.AddedOrder.OrderNumber = orderNumber;
            }

            return addOrderResponse;
        }
        public void AddToOrderRepo(Order order)
        {
            _orderRepository.SaveOrder(order);
        }

        public EditOrderResponse EditOrder(Order order)
        {
            EditOrderResponse editOrderResponse = new EditOrderResponse();
            editOrderResponse.EditedOrder = _orderRepository.LoadOrder(order.OrderDate, order.OrderNumber);

            editOrderResponse.StateTax = _taxRepository.LoadTaxes(order.State);
            editOrderResponse.ProductType = _productRepository.LoadProducts(order.ProductType);

            if (editOrderResponse.StateTax == null)
            {
                editOrderResponse.Success = false;
                editOrderResponse.Message = $"{order.State} is not a state we sell.";
            }
            else if (editOrderResponse.ProductType == null)
            {
                editOrderResponse.Success = false;
                editOrderResponse.Message = $"{order.ProductType} is not a valid product type.";
            }
            else
            {
                editOrderResponse.Success = true;
                order.CostPerSquareFoot = editOrderResponse.ProductType.MaterialUnitCost;
                order.LaborCostPerSquareFoot = editOrderResponse.ProductType.LaborUnitCost;
                order.TaxRate = editOrderResponse.StateTax.TaxRate;

                _orderRepository.Edit(order);
            }
            return editOrderResponse;
        }
    }
}
