﻿using DiamondAPI.DTOs.Order;
using DiamondAPI.Models;
using Microsoft.AspNetCore.StaticFiles;

namespace DiamondAPI.Mappers
{
    public static class OrderMappers
    {
        public static Order ToOrderFromCreateDTO(this CreateOrderRequestDTO createOrderRequestDTO)
        {
            return new Order
            {
                OrderId = Guid.NewGuid(),
                CustomerId = createOrderRequestDTO.CustomerId,
                OrderDate = DateTime.Now,
                TotalAmount = createOrderRequestDTO.TotalAmount,
                OrderFirstName = createOrderRequestDTO.OrderFirstName,
                OrderLastName = createOrderRequestDTO.OrderLastName,
                OrderEmail = createOrderRequestDTO.OrderEmail,
                OrderPhone = createOrderRequestDTO.OrderPhoneNumber,
            };
        }

        public static OrderRequestDTO ToOrderRequestDTO(this Order order)
        {
            return new OrderRequestDTO
            {
                OrderId = order.OrderId,
                CustomerId = order.CustomerId,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                OrderStatus = order.OrderStatus,
                Orderitems = order.Orderitems.Select(o => o.ToOrderitemDTO()).ToList()
            };
        }
    }
}
