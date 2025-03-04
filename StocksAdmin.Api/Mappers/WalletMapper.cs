﻿using ProductClientHub.Api.Entities;
using ProjectClientHub.Communication.Responses.Wallets;

namespace ProductClientHub.Api.Mappers
{
    public class WalletMapper
    {
        public static WalletResponse ToResponse(Wallet wallet)
        {
            return new WalletResponse
            {
                Id = wallet.Id,
                Name = wallet.Name,
            };
        }

        public static List<WalletResponse> ToResponseList(List<Wallet> wallets) => wallets.Select(w => ToResponse(w)).ToList();
    }
}
