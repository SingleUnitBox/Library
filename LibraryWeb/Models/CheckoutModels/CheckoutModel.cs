﻿namespace LibraryWeb.Models.CheckoutModels
{
    public class CheckoutModel
    {
        public string LibraryCardId { get; set; }
        public string Title { get; set; }
        public int LibraryAssetId { get; set; }
        public string ImageUrl { get; set; }
        public int HoldCount { get; set; }
        public bool IsCheckedOut { get; set; }
    }
}
