using FinalProject.oConnection;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Models
{
    public class Warehouse
    {
        dbAccess con1;
        OracleCommand cmd;
        OracleConnection aOracleConnection;

        public DataTable QueryReader(string QUERY)
        {
            //Open Connection
            Open();
            OracleTransaction CmdTrans = aOracleConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                // Set the command
                cmd = new OracleCommand(QUERY, aOracleConnection);
                cmd.Transaction = CmdTrans;
                cmd.CommandType = CommandType.Text;
                // Bind 
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                CmdTrans.Commit();
                return dt;
            }
            catch (Exception ex)
            {
                CmdTrans.Rollback();
                throw new Exception(ex.Message.ToString());
            }
            finally
            {
                Close();
            }
        }
        public DataTable QueryReader(string QUERY, OracleTransaction CmdTrans, OracleConnection aaOracleConnection)
        {
            try
            {
                // Set the command
                cmd = new OracleCommand(QUERY, aaOracleConnection);
                cmd.Transaction = CmdTrans;
                cmd.CommandType = CommandType.Text;
                // Bind 
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }
        public string Add(string USERNAME, string country,string state,string city , string type, string addre, string tele, string virt)
        {
            //Open Connection
            Open();
            OracleTransaction CmdTrans = aOracleConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {

                var USERS_SEQ = QueryReader("SELECT STD_ID_SEQ.NEXTVAL AS Student_ID FROM Student", CmdTrans, aOracleConnection);
                // var USERS_ID = USERS_SEQ.Rows[0]["USERS_ID"].ToString();

                var cmdText = "INSERT INTO WAREHOUSE ( " +
                                    "WAREHOUSE_NAME, " +
                                     
                                    
                                    "COUNTRY_ID,"+
                                    "STATE_ID,"+
                                    "CITY_ID,"+
                                    "WAREHOUSE_TYPE,"+
                                    "WAREHOUSE_ADDRESS,"+
                                    "TELEPHONE,"+
                                    "VIRTUAL_WAREHOUSE"+
                                ") VALUES( " +
                
                                    ":USERNAME," +
                                    
                                    ":COUNT_ID,"+
                                    ":ST_ID,"+
                                    ":CIT_ID,"+
                                    ":TYPE,"+
                                    ":ADDR,"+
                                    ":TELEP,"+
                                    ":VIRT"+
                                ") ";

                // create command and set properties  
                OracleCommand cmd = aOracleConnection.CreateCommand();
                cmd.Transaction = CmdTrans;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = cmdText;
               // cmd.Parameters.Add(":USER_ID", OracleDbType.NVarchar2).Value = ID;
                cmd.Parameters.Add(":USERNAME", OracleDbType.NVarchar2).Value = USERNAME;
              
                cmd.Parameters.Add(":COUNT_ID", OracleDbType.NVarchar2).Value = country;
                cmd.Parameters.Add(":ST_ID", OracleDbType.Int16).Value = state;
                cmd.Parameters.Add(":CIT_ID", OracleDbType.NVarchar2).Value = city;
                 cmd.Parameters.Add(":TYPE", OracleDbType.NVarchar2).Value = type;
                 cmd.Parameters.Add(":ADDR", OracleDbType.NVarchar2).Value = addre;
                cmd.Parameters.Add(":TELEP", OracleDbType.Int16).Value = tele;
                //cmd.Parameters.Add(":DEFAULT", OracleDbType.NVarchar2).Value = defau;
                cmd.Parameters.Add(":VIRT", OracleDbType.Int16).Value = virt;



                cmd.ExecuteNonQuery();


                CmdTrans.Commit();
                return USERNAME;

            }

            catch (Exception ex)
            {
                CmdTrans.Rollback();
                throw new Exception(ex.Message.ToString());

            }
            finally
            {
                Close();
            }
        }
        public string DELETE_USER(string USER_ID)
        {
            //Open Connection
            Open();
            OracleTransaction CmdTrans = aOracleConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {


                var cmdText = "DELETE from WAREHOUSE where WAREHOUSE_ID=  " +
                                    USER_ID +

                                "";

                // create command and set properties  
                OracleCommand cmd = aOracleConnection.CreateCommand();
                cmd.Transaction = CmdTrans;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = cmdText;



                cmd.ExecuteNonQuery();


                CmdTrans.Commit();
                return "1";

            }

            catch (Exception ex)
            {
                CmdTrans.Rollback();
                throw new Exception(ex.Message.ToString());

            }
            finally
            {
                Close();
            }
        }
        public string DELETE_PURCHASE(string ID)
        {
            //Open Connection
            Open();
            OracleTransaction CmdTrans = aOracleConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {


                var cmdText = "DELETE from PURCHASE where PURCHASE_ID=  " +
                                    ID +

                                "";

                // create command and set properties  
                OracleCommand cmd = aOracleConnection.CreateCommand();
                cmd.Transaction = CmdTrans;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = cmdText;



                cmd.ExecuteNonQuery();


                CmdTrans.Commit();
                return "1";

            }

            catch (Exception ex)
            {
                CmdTrans.Rollback();
                throw new Exception(ex.Message.ToString());

            }
            finally
            {
                Close();
            }
        }
        public string Add_Purchase(int ID , int price, int total, string name , int quantity)
        {
            //Open Connection
            Open();
            OracleTransaction CmdTrans = aOracleConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {

                var USERS_SEQ = QueryReader("SELECT STD_ID_SEQ.NEXTVAL AS Student_ID FROM Student", CmdTrans, aOracleConnection);
                // var USERS_ID = USERS_SEQ.Rows[0]["USERS_ID"].ToString();

                var cmdText = "INSERT INTO PURCHASE ( " +
                                    "CATEGORY_ID, " +


                                    "PRICE," +
                                    "TOTALPRICE," +
                                    "ITEM_NAME," +
                                    "QUANTITY" +
                                    
                                ") VALUES( " +

                                    ":ID," +

                                    ":PRICE," +
                                    ":TOTALPRICE," +
                                    ":NAME," +
                                    ":QUANTITY" +
                                    
                                ") ";

                // create command and set properties  
                OracleCommand cmd = aOracleConnection.CreateCommand();
                cmd.Transaction = CmdTrans;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = cmdText;
                // cmd.Parameters.Add(":USER_ID", OracleDbType.NVarchar2).Value = ID;
                cmd.Parameters.Add(":ID", OracleDbType.NVarchar2).Value = ID;

                cmd.Parameters.Add(":PRICE", OracleDbType.NVarchar2).Value = price;
                cmd.Parameters.Add(":TOTALPRICE", OracleDbType.NVarchar2).Value = total;
                cmd.Parameters.Add(":NAME", OracleDbType.NVarchar2).Value = name;
                cmd.Parameters.Add(":QUANTITY", OracleDbType.NVarchar2).Value = quantity;
               
                //cmd.Parameters.Add(":DEFAULT", OracleDbType.NVarchar2).Value = defau;
                //cmd.Parameters.Add(":VIRT", OracleDbType.Int16).Value = virt;



                cmd.ExecuteNonQuery();


                CmdTrans.Commit();
                return name;

            }

            catch (Exception ex)
            {
                CmdTrans.Rollback();
                throw new Exception(ex.Message.ToString());

            }
            finally
            {
                Close();
            }
        }
        public string Add_Item(string name,  int type,  int ware_id,int price)
        {
            //Open Connection
            Open();
            OracleTransaction CmdTrans = aOracleConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {

                var USERS_SEQ = QueryReader("SELECT STD_ID_SEQ.NEXTVAL AS Student_ID FROM Student", CmdTrans, aOracleConnection);
                // var USERS_ID = USERS_SEQ.Rows[0]["USERS_ID"].ToString();

                var cmdText = "INSERT INTO CATEGORY_TB ( " +
                                    "CATEGORY_NAME, " +

                                    "CATEGORY_TYPE," +
                                    "WAREHOUSE_ID," +
                                    "PRICE" +

                                ") VALUES( " +

                                    ":NAME," +

                                    
                                    ":TYPE," +
                                    ":ID," +
                                    ":PRICE" +

                                ") ";

                // create command and set properties  
                OracleCommand cmd = aOracleConnection.CreateCommand();
                cmd.Transaction = CmdTrans;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = cmdText;
                // cmd.Parameters.Add(":USER_ID", OracleDbType.NVarchar2).Value = ID;
                cmd.Parameters.Add(":NAME", OracleDbType.NVarchar2).Value = name;

                cmd.Parameters.Add(":TYPE", OracleDbType.NVarchar2).Value = type;
                cmd.Parameters.Add(":ID", OracleDbType.NVarchar2).Value = ware_id;
                cmd.Parameters.Add(":PRICE", OracleDbType.NVarchar2).Value = price;
             //   cmd.Parameters.Add(":QUANTITY", OracleDbType.NVarchar2).Value = quantity;

                //cmd.Parameters.Add(":DEFAULT", OracleDbType.NVarchar2).Value = defau;
                //cmd.Parameters.Add(":VIRT", OracleDbType.Int16).Value = virt;



                cmd.ExecuteNonQuery();


                CmdTrans.Commit();
                return name;

            }

            catch (Exception ex)
            {
                CmdTrans.Rollback();
                throw new Exception(ex.Message.ToString());

            }
            finally
            {
                Close();
            }
        }
        public string Update( string ID,string Name,string Type, string Address,string Country,string City, string State, string Telephone, string Virtual)
        {
            //Open Connection
            Open();
            OracleTransaction CmdTrans = aOracleConnection.BeginTransaction(IsolationLevel.ReadCommitted);
            Console.WriteLine(Name, Telephone);
            try
            {


                var cmdText = "UPDATE WAREHOUSE SET WAREHOUSE_NAME= :USERNAME, WAREHOUSE_TYPE= :TYPE, WAREHOUSE_ADDRESS= :ADDRESS, COUNTRY_ID= :CID, STATE_ID= :SID, CITY_ID= :CITID, TELEPHONE= :TELE, VIRTUAL_WAREHOUSE= :VIRT where WAREHOUSE_ID= :USER_ID";



                // create command and set properties  
                OracleCommand cmd = aOracleConnection.CreateCommand();
                cmd.Transaction = CmdTrans;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = cmdText;

                
               
                cmd.Parameters.Add(":USERNAME", OracleDbType.NVarchar2).Value = Name;
                cmd.Parameters.Add(":TYPE", OracleDbType.NVarchar2).Value = Type;
                cmd.Parameters.Add(":ADDRESS", OracleDbType.NVarchar2).Value = Address;
                cmd.Parameters.Add(":CID", OracleDbType.Int32).Value = Country;
                cmd.Parameters.Add(":CITID", OracleDbType.Int32).Value = City;
                cmd.Parameters.Add(":SID", OracleDbType.Int32).Value = Int32.Parse(State);
                cmd.Parameters.Add(":TELE", OracleDbType.Int32).Value = Int32.Parse(Telephone);
                cmd.Parameters.Add(":VIRT", OracleDbType.Int32).Value = Virtual;
                cmd.Parameters.Add(":USER_ID", OracleDbType.Int32).Value = Int32.Parse(ID);

                cmd.ExecuteNonQuery();


                CmdTrans.Commit();
                return "1";

            }

            catch (Exception ex)
            {
                CmdTrans.Rollback();
                throw new Exception(ex.Message.ToString());

            }
            finally
            {
                Close();
            }
        }
        void Open()
        {
            con1 = new dbAccess();
            aOracleConnection = con1.get_con();
        }

        void Close()
        {
            con1.Close(aOracleConnection);
        }
    }
}
