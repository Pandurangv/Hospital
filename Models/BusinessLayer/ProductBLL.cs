﻿using Hospital.Models.Models;
using Hospital.Models.DataLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Hospital.Models.BusinessLayer
{
    public class ProductBLL
    {
        clsDataAccess mobjDataAcces = new clsDataAccess();

        public ProductBLL()
        {
            objData = new CriticareHospitalDataContext();
        }

        public CriticareHospitalDataContext objData { get; set; }

        public int GetNewProductId()
        {
            EntityProduct ldt = new EntityProduct();
            try
            {
                ldt = (from tbl in objData.tblProductMasters
                       where tbl.IsDelete == false
                       orderby tbl.ProductId descending
                       select new EntityProduct { ProductId = tbl.ProductId }).FirstOrDefault();
                if (ldt == null)
                {
                    ldt = new EntityProduct();
                    ldt.ProductId = 1;
                }
                else
                {
                    ldt.ProductId++;
                }
            }
            catch (Exception ex)
            {

                Commons.FileLog("ProductBLL - GetNewProductId()", ex);
            }
            return ldt.ProductId;
        }


        public List<sp_GetAllProductResult> GetAllProduct()
        {
            return objData.sp_GetAllProduct().ToList();
        }

        public int InsertProduct(EntityProduct entProduct)
        {
            try
            {
                tblProductMaster obj = new tblProductMaster()
                {
                    ProductName = entProduct.ProductName,
                    UOM = entProduct.UOM,
                    SubUOM = entProduct.SubUOM,
                    Price = Convert.ToDecimal(entProduct.Price),
                    ProductContent=entProduct.Content,
                    ProductTypeId=entProduct.ProductTypeId,
                    IsDelete=false,
                    Category=entProduct.Category
                };
                objData.tblProductMasters.InsertOnSubmit(obj);
                objData.SubmitChanges();
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public int InsertProduct(EntityProduct entProduct)
        //{
        //    int cnt = 0;
        //    try
        //    {
        //        List<SqlParameter> lstParam = new List<SqlParameter>();
        //        //Commons.ADDParameter(ref lstParam, "@ProductId", DbType.String, entProduct.ProductId);
        //        Commons.ADDParameter(ref lstParam, "@ProductName", DbType.String, entProduct.ProductName);
        //        Commons.ADDParameter(ref lstParam, "@UOM", DbType.String, entProduct.UOM);
        //        Commons.ADDParameter(ref lstParam, "@SubUOM", DbType.String, entProduct.SubUOM);
        //        Commons.ADDParameter(ref lstParam, "@Price", DbType.String, entProduct.Price);
        //        cnt = mobjDataAcces.ExecuteQuery("sp_InsertProduct ", lstParam);
        //    }
        //    catch (Exception ex)
        //    {

        //        Commons.FileLog("ProductBLL - InsertProduct(EntityProduct entProduct)", ex);
        //    }
        //    return cnt;
        //}

        public DataTable GetProductForEdit(string pstrProductId)
        {
            DataTable ldt = new DataTable();
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@ProductId", DbType.String, pstrProductId);
                ldt = mobjDataAcces.GetDataTable("sp_GetProductForEdit", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("ProductBLL - GetProductForEdit(string pstrProductId)", ex);
            }
            return ldt;
        }

        public int UpdateProduct(EntityProduct entProduct)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@ProductId", DbType.String, entProduct.ProductId);
                Commons.ADDParameter(ref lstParam, "@ProductName", DbType.String, entProduct.ProductName);
                Commons.ADDParameter(ref lstParam, "@UOM", DbType.String, entProduct.UOM);
                Commons.ADDParameter(ref lstParam, "@SubUOM", DbType.String, entProduct.SubUOM);
                Commons.ADDParameter(ref lstParam, "@Price", DbType.String, entProduct.Price);
                Commons.ADDParameter(ref lstParam, "@ProductTypeId", DbType.Int32, entProduct.ProductTypeId);
                Commons.ADDParameter(ref lstParam, "@ProductContent", DbType.String, !string.IsNullOrEmpty(entProduct.Content)?entProduct.Content:"");

                cnt = mobjDataAcces.ExecuteQuery("sp_UpdateProduct", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("ProductBLL -  UpdateProductEntityProduct entProduct)", ex);
            }

            return cnt;
        }

        public int DeleteProduct(EntityProduct entProduct)
        {
            int cnt = 0;
            try
            {
                List<SqlParameter> lstParam = new List<SqlParameter>();
                Commons.ADDParameter(ref lstParam, "@ProductId", DbType.String, entProduct.ProductId);
                cnt = mobjDataAcces.ExecuteQuery("sp_DeleteProduct", lstParam);
            }
            catch (Exception ex)
            {
                Commons.FileLog("ProductBLL - DeleteProduct(EntityProduct entProduct)", ex);
            }
            return cnt;
        }

        public List<EntityProduct> GetAllProductBatchList(int ProductId=0)
        {
            List<EntityProduct> lst = (from tbl in objData.STP_GetProductBatch()
                                       select new EntityProduct { 
                                            ProductId=tbl.ProductId,
                                            BatchNo=tbl.BatchNo,
                                            ExpiryDate=tbl.ExpiryDate,
                                       }).ToList();
            if (ProductId>0)
            {
                lst = lst.Where(p => p.ProductId == ProductId).ToList();
            }
            return lst;
        }

        public List<EntityProduct> GetAllProducts()
        {
            List<EntityProduct> lst = null;
            try
            {
                lst = (from tbl in objData.tblProductMasters
                       where tbl.IsDelete == false
                       select new EntityProduct
                       {
                           ProductId = tbl.ProductId,
                           ProductName = tbl.ProductName,
                           Category=tbl.Category,
                           Content=tbl.ProductContent,
                           Price=tbl.Price,
                           ProductTypeId=tbl.ProductTypeId,
                           UOM=tbl.UOM,
                           SubUOM=tbl.SubUOM,
                       }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lst;
        }

        public List<StockInfo> GetRawItemsForReport()
        {
            return (from tbl in objData.STP_Select_ItemWiseStockDetails()
                    select new StockInfo
                    {
                        ProductName = tbl.ProductName,
                        ProductId = tbl.ProductId,
                        InwardQty = tbl.InwardQty == null ? 0 : tbl.InwardQty,
                        OutwardQty = tbl.OutwardQty == null ? 0 : tbl.OutwardQty,
                        ClosingStock = tbl.ClosingStock == null ? 0 : tbl.ClosingStock
                    }).ToList();
        }
    }

    public class ProductTypeBLL
    {
        public ProductTypeBLL()
        {
            objData = new CriticareHospitalDataContext();
        }

        public CriticareHospitalDataContext objData { get; set; }

        public List<EntityProductType> GetProductTypes()
        {
            var lst = (from tbl in objData.tblProductTypes
                       where tbl.IsDelete == false
                       select new EntityProductType { 
                            Description=tbl.Description,
                            IsDelete=tbl.IsDelete,
                            ProcutTypeId=tbl.ProductTypeId,
                            ProductType=tbl.ProductTyepe
                       }).ToList();
            return lst;
        }

        public int Save(EntityProductType model)
        {
            var tbl = new tblProductType() {
                Description = model.Description,
                IsDelete = false,
                ProductTyepe = model.ProductType
            };

            objData.tblProductTypes.InsertOnSubmit(tbl);
            objData.SubmitChanges();
            return tbl.ProductTypeId;
        }

        public int Update(EntityProductType model)
        {
            var tbl = objData.tblProductTypes.Where(p => p.ProductTypeId == model.ProcutTypeId).FirstOrDefault();
            if (tbl!=null)
            {
                tbl.ProductTyepe=model.ProductType;
                tbl.Description = model.Description;
            }
            objData.SubmitChanges();
            return tbl.ProductTypeId;
        }

    }
}