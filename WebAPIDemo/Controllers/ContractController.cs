using Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIDemo.Models;

namespace WebAPIDemo.Controllers
{
    public class ContractController : ApiController
    {
        [HttpDelete]
        public void DeleteContract(Contracts contracts )
        {
            string DBConnection = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(DBConnection);
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "DELTE FROM Contracts WHERE ContractId = " + contracts.ContractId + "";
            sqlCmd.Connection = sqlConn;
            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
            sqlDa.Fill(ds);
        }

        [HttpGet]
        public List<Contracts> GetContractList()
        {
            string DBConnection = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(DBConnection);
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "SELECT * FROM Contracts";
            sqlCmd.Connection = sqlConn;
            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
            sqlDa.Fill(ds);

            List<Contracts> lstContracts= new List<Contracts>();
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Contracts contracts = new Contracts();
                    contracts.ContractId = Convert.ToInt32(dr["ContractId"]);
                    contracts.Name = Convert.ToString(dr["Name"]);
                    contracts.Address = Convert.ToString(dr["Address"]);
                    contracts.Gender = Convert.ToString(dr["Gender"]);
                    contracts.Country = Convert.ToString(dr["Country"]);
                    contracts.DOB = Convert.ToDateTime(dr["DOB"]);
                    contracts.SaleDate = Convert.ToDateTime(dr["SaleDate"]);
                    contracts.CoveragePlan = Convert.ToString(dr["CoveragePlan"]);
                    contracts.Price = Convert.ToDecimal(dr["Price"]);

                    lstContracts.Add(contracts);
                }
            }
            return lstContracts;
        }

        [HttpPost]
        public Contracts UpdateContract(Contracts contracts)
        {
            string DBConnection = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(DBConnection);
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "UPDATE Contracts SET CoveragePlan = '"+contracts.CoveragePlan+"',Price = '"+contracts.Price+"' WHERE ContarctId="+contracts.ContractId;
            sqlCmd.Connection = sqlConn;
            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
            sqlDa.Fill(ds);

            List<Contracts> lstContracts = new List<Contracts>();
            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                string status = Convert.ToString(ds.Tables[0].Rows[0]["RESULT"]);
                if (status == "SUCCESS")
                {
                    return new Contracts
                    {
                        Message = status
                    };
                }
                else
                {
                    return new Contracts
                    {
                        Message = "ERROR"
                    };
                }
            }
            else
            {
                return new Contracts
                {
                    Message = "ERROR"
                };
            }
        }

        [HttpPost]
        public Contracts AddContract(Contracts contracts)
        {
            string DBConnection = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(DBConnection);
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "INSERT INTO Contracts values('" + contracts.Name + "','" + contracts.Address + "','" + contracts.Gender + "','" + contracts.Country + "','" + contracts.DOB + "','" + contracts.SaleDate + "','" + contracts.CoveragePlan + "','"+contracts.Price+"')";
            sqlCmd.Connection = sqlConn;
            SqlDataAdapter sqlDa = new SqlDataAdapter(sqlCmd);
            sqlDa.Fill(ds);

            if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                string status = Convert.ToString(ds.Tables[0].Rows[0]["RESULT"]);
                if (status == "SUCCESS")
                {
                    return new Contracts
                    {
                        Message = status
                    };
                }
                else
                {
                    return new Contracts
                    {
                        Message = "ERROR"
                    };
                }
            }
            else
            {
                return new Contracts
                {
                    Message = "ERROR"
                };
            }
        }
    }
}
