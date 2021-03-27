using FoodOrderApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace FoodOrderApp.Helpers
{
    class CreateCartTable
    {
        public bool CreateTable()
        {
            try
            {

                var cn = DependencyService.Get<ISQLite>().GetConnection();
                //cn.DropTable<CartItem>();
                var info = cn.GetTableInfo("CartItem");
                if (!info.Any())
                {
                    cn.CreateTable<CartItem>();
                    cn.Close();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
