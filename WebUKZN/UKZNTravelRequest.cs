using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKZNInterface.WebReference;
using EntityManager;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
namespace UKZNInterface
{

    /// <summary>
    /// Summary description for UKZNTravelRequest
    /// </summary>
    public class UKZNTravelRequest
    {
        G0securitytokentypeUser objSec = new G0securitytokentypeUser();
        TravelManagementUKZN objUKZN = new TravelManagementUKZN();
        UKZNSecurity objUKZNSecurity = new UKZNSecurity();
        /// <summary>
        /// First intiate the travel request to the UKZN
        /// </summary>
        /// <param name="objTravelRequest"></param>
        /// <returns>string Message from UKZN</returns>
        public string Validatetravelrequest(TravelRequestProcess objTravelRequest)
        {
            try
            {
                objSec = UKZNSecurity.SetUKZNSecurityt();
                //********************************validatetravelrequest*****************************************/

                Gna160Travelnewrequest1typeUser[] LstobjTravelRequ1 = new Gna160Travelnewrequest1typeUser[1];


                Gna160Travelnewrequest1typeUser objobjTravelRequ1 = new Gna160Travelnewrequest1typeUser();
                objobjTravelRequ1.supplierno = objTravelRequest.supplierno;
                objobjTravelRequ1.costcentre = objTravelRequest.costcentre;
                objobjTravelRequ1.travelrequestno = objTravelRequest.travelrequestno;
                objobjTravelRequ1.staffnorequester = objTravelRequest.staffnorequester;
                objobjTravelRequ1.inputbyagentyn = objTravelRequest.inputbyagentyn;
                objobjTravelRequ1.totamountincvat = objTravelRequest.totamountincvat;

                LstobjTravelRequ1[0] = objobjTravelRequ1;

                List<Gna160Travelnewrequest2typeUser> LstobjTravelReq = new List<Gna160Travelnewrequest2typeUser>();

                foreach (var ItemList in objTravelRequest.ListObjectTravelRequest)
                {
                    Gna160Travelnewrequest2typeUser objTravelReq = new Gna160Travelnewrequest2typeUser();
                    objTravelReq.travellersurname = ItemList.travellersurname;
                    if (ItemList.travellernationalid != null)
                        objTravelReq.travellernationalid = ItemList.travellernationalid;
                    else
                        objTravelReq.travellerpassport = ItemList.travellerpassport;
                    objTravelReq.itemamountincvat = ItemList.itemamountincvat;
                    objTravelReq.itemcategory = ItemList.itemcategory;
                    objTravelReq.accountnumber = ItemList.accountnumber;




                    LstobjTravelReq.Add(objTravelReq);
                }

                Gna160Travelnewresponse1typeUser[] objTravelRequestRsp = objUKZN.validatetravelrequest(objSec, LstobjTravelRequ1, LstobjTravelReq.ToArray());
                return objTravelRequestRsp[0].message;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// First intiate the travel request to the UKZN
        /// </summary>
        /// <param name="objTravelRequest"></param>
        /// <returns>string Message from UKZN</returns>
        public string Validateaccountantrejection(TravelRequestProcess objTravelRequest)
        {
            try
            {
                // Secuirty Parameter
                objSec = UKZNSecurity.SetUKZNSecurityt();
                // First Parameter
                List<Gna160Valaccrejrequest1typeUser> objLstGna160Valaccrejrequest1typeUser = new List<Gna160Valaccrejrequest1typeUser>();

                Gna160Valaccrejrequest1typeUser objGna160Valaccrejrequest1typeUser = new Gna160Valaccrejrequest1typeUser();

                objGna160Valaccrejrequest1typeUser.supplierno = objTravelRequest.supplierno;
                objGna160Valaccrejrequest1typeUser.costcentre = objTravelRequest.costcentre;
                objGna160Valaccrejrequest1typeUser.travelrequestno = objTravelRequest.travelrequestno;
                objGna160Valaccrejrequest1typeUser.staffnoaccountant = objTravelRequest.staffnoaccountant;
                objGna160Valaccrejrequest1typeUser.totamountincvat = objTravelRequest.totamountincvat;
                objLstGna160Valaccrejrequest1typeUser.Add(objGna160Valaccrejrequest1typeUser);

                // Second Parameter
                List<Gna160Valaccrejrequest2typeUser> objLstGna160Valaccrejrequest2typeUser = new List<Gna160Valaccrejrequest2typeUser>();
                foreach (var ItemList in objTravelRequest.ListObjectTravelRequest)
                {
                    Gna160Valaccrejrequest2typeUser objGna160Valaccrejrequest2typeUser = new Gna160Valaccrejrequest2typeUser();
                    objGna160Valaccrejrequest2typeUser.accountnumber = ItemList.accountnumber;
                    objGna160Valaccrejrequest2typeUser.itemamountincvat = ItemList.itemamountincvat;
                    objLstGna160Valaccrejrequest2typeUser.Add(objGna160Valaccrejrequest2typeUser);
                }
                Gna160Valaccrejresponse1typeUser[] objResponce = objUKZN.validateaccountantrejection(objSec, objLstGna160Valaccrejrequest1typeUser.ToArray(), objLstGna160Valaccrejrequest2typeUser.ToArray());
                return objResponce[0].message;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string Validateaccountantapproval(TravelRequestProcess objTravelRequest)
        {
            try
            {
                // Secuirty Parameter
                objSec = UKZNSecurity.SetUKZNSecurityt();
                // First Parameter
                List<Gna160Valaccapprrequest1typeUser> objLstGna160Valaccapprrequest1typeUser = new List<Gna160Valaccapprrequest1typeUser>();

                Gna160Valaccapprrequest1typeUser objGna160Valaccapprrequest1typeUser = new Gna160Valaccapprrequest1typeUser();

                objGna160Valaccapprrequest1typeUser.supplierno = objTravelRequest.supplierno;
                objGna160Valaccapprrequest1typeUser.costcentre = objTravelRequest.costcentre;
                objGna160Valaccapprrequest1typeUser.travelrequestno = objTravelRequest.travelrequestno;
                objGna160Valaccapprrequest1typeUser.staffnoaccountant = objTravelRequest.staffnoaccountant;
                objGna160Valaccapprrequest1typeUser.totamountincvat = objTravelRequest.totamountincvat;
                objLstGna160Valaccapprrequest1typeUser.Add(objGna160Valaccapprrequest1typeUser);

                // Second Parameter
                List<Gna160Valaccapprrequest2typeUser> objLstGna160Valaccapprrequest2typeUser = new List<Gna160Valaccapprrequest2typeUser>();
                foreach (var ItemList in objTravelRequest.ListObjectTravelRequest)
                {
                    Gna160Valaccapprrequest2typeUser objGna160Valaccapprrequest2typeUser = new Gna160Valaccapprrequest2typeUser();
                    objGna160Valaccapprrequest2typeUser.accountnumber = ItemList.accountnumber;
                    objGna160Valaccapprrequest2typeUser.itemamountincvat = ItemList.itemamountincvat;
                    objLstGna160Valaccapprrequest2typeUser.Add(objGna160Valaccapprrequest2typeUser);
                }
                Gna160Valaccapprresponse1typeUser[] objResponce = objUKZN.validateaccountantapproval(objSec, objLstGna160Valaccapprrequest1typeUser.ToArray(), objLstGna160Valaccapprrequest2typeUser.ToArray());
                return objResponce[0].message;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string Validatetravelrejection(TravelRequestProcess objTravelRequest)
        {
            try
            {
                // Secuirty Parameter
                objSec = UKZNSecurity.SetUKZNSecurityt();
                // First Parameter
                List<Gna160Valaprvrejrequest1typeUser> objLstGna160Valaprvrejrequest1typeUser = new List<Gna160Valaprvrejrequest1typeUser>();

                Gna160Valaprvrejrequest1typeUser objGna160Valaprvrejrequest1typeUser = new Gna160Valaprvrejrequest1typeUser();

                objGna160Valaprvrejrequest1typeUser.supplierno = objTravelRequest.supplierno;
                objGna160Valaprvrejrequest1typeUser.costcentre = objTravelRequest.costcentre;
                objGna160Valaprvrejrequest1typeUser.travelrequestno = objTravelRequest.travelrequestno;
                objGna160Valaprvrejrequest1typeUser.staffnorequester = objTravelRequest.staffnorequester;
                objGna160Valaprvrejrequest1typeUser.staffnoapprover = objTravelRequest.staffnoapprover;
                objGna160Valaprvrejrequest1typeUser.approvallevel = objTravelRequest.approvallevel;
                objGna160Valaprvrejrequest1typeUser.totamountincvat = objTravelRequest.totamountincvat;
                objLstGna160Valaprvrejrequest1typeUser.Add(objGna160Valaprvrejrequest1typeUser);

                // Second Parameter
                List<Gna160Valaprvrejrequest2typeUser> objLstGna160Valaprvrejrequest2typeUser = new List<Gna160Valaprvrejrequest2typeUser>();
                foreach (var ItemList in objTravelRequest.ListObjectTravelRequest)
                {
                    Gna160Valaprvrejrequest2typeUser objGna160Valaprvrejrequest2typeUser = new Gna160Valaprvrejrequest2typeUser();
                    objGna160Valaprvrejrequest2typeUser.travellersurname = ItemList.travellersurname;
                    objGna160Valaprvrejrequest2typeUser.travellernationalid = ItemList.travellernationalid;
                    objGna160Valaprvrejrequest2typeUser.travellerpassport = ItemList.travellerpassport;

                    objLstGna160Valaprvrejrequest2typeUser.Add(objGna160Valaprvrejrequest2typeUser);
                }
                Gna160Valaprvrejresponse1typeUser[] objResponce = objUKZN.validatetravelrejection(objSec, objLstGna160Valaprvrejrequest1typeUser.ToArray(), objLstGna160Valaprvrejrequest2typeUser.ToArray());
                return objResponce[0].message;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Gna160Valaprvappresponse1typeUser[] Validatetravelapproval(TravelRequestProcess objTravelRequest)
        {
            try
            {
                // Secuirty Parameter
                objSec = UKZNSecurity.SetUKZNSecurityt();
                // First Parameter
                List<Gna160Valaprvapprequest1typeUser> objLstGna160Valaprvapprequest1typeUser = new List<Gna160Valaprvapprequest1typeUser>();

                Gna160Valaprvapprequest1typeUser objGna160Valaprvapprequest1typeUser = new Gna160Valaprvapprequest1typeUser();

                objGna160Valaprvapprequest1typeUser.supplierno = objTravelRequest.supplierno;
                objGna160Valaprvapprequest1typeUser.costcentre = objTravelRequest.costcentre;
                objGna160Valaprvapprequest1typeUser.travelrequestno = objTravelRequest.travelrequestno;
                objGna160Valaprvapprequest1typeUser.staffnorequester = objTravelRequest.staffnorequester;
                objGna160Valaprvapprequest1typeUser.staffnoapprover = objTravelRequest.staffnoapprover;
                objGna160Valaprvapprequest1typeUser.staffnoaccountant = objTravelRequest.staffnoaccountant;
                objGna160Valaprvapprequest1typeUser.approvallevel = objTravelRequest.approvallevel;
                objGna160Valaprvapprequest1typeUser.overriddenyn = objTravelRequest.overriddenyn;
                objGna160Valaprvapprequest1typeUser.finalapprovalyn = objTravelRequest.finalapprovalyn;
                objGna160Valaprvapprequest1typeUser.totamountincvat = objTravelRequest.totamountincvat;
                objLstGna160Valaprvapprequest1typeUser.Add(objGna160Valaprvapprequest1typeUser);

                // Second Parameter
                List<Gna160Valaprvapprequest2typeUser> objLstGna160Valaprvapprequest2typeUser = new List<Gna160Valaprvapprequest2typeUser>();
                foreach (var ItemList in objTravelRequest.ListObjectTravelRequest)
                {
                    Gna160Valaprvapprequest2typeUser objGna160Valaprvapprequest2typeUser = new Gna160Valaprvapprequest2typeUser();
                    objGna160Valaprvapprequest2typeUser.uniquerequestid = ItemList.uniquerequestid;
                    objGna160Valaprvapprequest2typeUser.itemdescription = ItemList.itemdescription;
                    objGna160Valaprvapprequest2typeUser.accountnumber = ItemList.accountnumber;
                    objGna160Valaprvapprequest2typeUser.travellersurname = ItemList.travellersurname;

                    objGna160Valaprvapprequest2typeUser.travellernationalid = ItemList.travellernationalid;
                    objGna160Valaprvapprequest2typeUser.travellerpassport = ItemList.travellerpassport;
                    objGna160Valaprvapprequest2typeUser.itemvatamount = ItemList.itemvatamount;
                    objGna160Valaprvapprequest2typeUser.itemamountincvat = ItemList.itemamountincvat;

                    objLstGna160Valaprvapprequest2typeUser.Add(objGna160Valaprvapprequest2typeUser);
                }
                // Method Calling
                Gna160Valaprvappresponse1typeUser[] objResponce = objUKZN.validatetravelapproval(objSec, objLstGna160Valaprvapprequest1typeUser.ToArray(), objLstGna160Valaprvapprequest2typeUser.ToArray());
                return objResponce;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public string validateordcancellation(List<TravelRequestProcess> objTravelRequest)
        {
            try
            {
                // Secuirty Parameter
                objSec = UKZNSecurity.SetUKZNSecurityt();
                // First Parameter
                List<Gna160Valcancordrequest1typeUser> objLstGna160Valcancordrequest1typeUser = new List<Gna160Valcancordrequest1typeUser>();
                foreach (var ItemList in objTravelRequest)
                {
                    Gna160Valcancordrequest1typeUser objGna160Valcancordrequest1typeUser = new Gna160Valcancordrequest1typeUser();

                    objGna160Valcancordrequest1typeUser.supplierno = ItemList.supplierno;
                    objGna160Valcancordrequest1typeUser.ordernumber = ItemList.ordernumber;
                    objGna160Valcancordrequest1typeUser.travelrequestno = ItemList.travelrequestno;
                    objGna160Valcancordrequest1typeUser.totamountincvat = ItemList.totamountincvat;
                    objGna160Valcancordrequest1typeUser.cancellationdate = ItemList.cancellationdate;

                    objLstGna160Valcancordrequest1typeUser.Add(objGna160Valcancordrequest1typeUser);
                }
                // Method Calling
                Gna160Valcancordresponse1typeUser[] objResponce = objUKZN.validateordcancellation(objSec, objLstGna160Valcancordrequest1typeUser.ToArray());
                return objResponce[0].message;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string validateorditemcancellation(List<TravelRequestProcess> objTravelRequest)
        {
            try
            {
                // Secuirty Parameter
                objSec = UKZNSecurity.SetUKZNSecurityt();
                // First Parameter
                List<Gna160Valcancitmrequest1typeUser> objLstGna160Valcancitmrequest1typeUser = new List<Gna160Valcancitmrequest1typeUser>();
                foreach (var ItemList in objTravelRequest)
                {
                    Gna160Valcancitmrequest1typeUser objGna160Valcancitmrequest1typeUser = new Gna160Valcancitmrequest1typeUser();

                    objGna160Valcancitmrequest1typeUser.supplierno = ItemList.supplierno;
                    objGna160Valcancitmrequest1typeUser.ordernumber = ItemList.ordernumber;
                    objGna160Valcancitmrequest1typeUser.travelrequestno = ItemList.travelrequestno;
                    objGna160Valcancitmrequest1typeUser.uniquerequestid = ItemList.uniquerequestid;
                    objGna160Valcancitmrequest1typeUser.totamountincvat = ItemList.totamountincvat;
                    objGna160Valcancitmrequest1typeUser.cancellationdate = ItemList.cancellationdate;
                    objLstGna160Valcancitmrequest1typeUser.Add(objGna160Valcancitmrequest1typeUser);
                }
                // Method Calling
                Gna160Valcancitmresponse1typeUser[] objResponce = objUKZN.validateorditemcancellation(objSec, objLstGna160Valcancitmrequest1typeUser.ToArray());
                return objResponce[0].message;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string validateinvoice(TravelRequestProcess objTravelRequest)
        {
            try
            {
                // Secuirty Parameter
                objSec = UKZNSecurity.SetUKZNSecurityt();
                // First Parameter
                List<Gna160Valinvoicerequest1typeUser> objLstGna160Valinvoicerequest1typeUser = new List<Gna160Valinvoicerequest1typeUser>();

                Gna160Valinvoicerequest1typeUser objGna160Valinvoicerequest1typeUser = new Gna160Valinvoicerequest1typeUser();

                objGna160Valinvoicerequest1typeUser.supplierno = objTravelRequest.supplierno;
                objGna160Valinvoicerequest1typeUser.invoicenumber = objTravelRequest.invoicenumber;
                objGna160Valinvoicerequest1typeUser.totamountincvat = objTravelRequest.totamountincvat;
                objGna160Valinvoicerequest1typeUser.invoicedate = objTravelRequest.invoicedate;
                objGna160Valinvoicerequest1typeUser.invoicedescription = objTravelRequest.invoicedescription;
                objGna160Valinvoicerequest1typeUser.ordernumber = objTravelRequest.ordernumber;
                objGna160Valinvoicerequest1typeUser.costcentre = objTravelRequest.costcentre;

                objLstGna160Valinvoicerequest1typeUser.Add(objGna160Valinvoicerequest1typeUser);
                // Second Parameter
                List<Gna160Valinvoicerequest2typeUser> objLstGna160Valinvoicerequest2typeUser = new List<Gna160Valinvoicerequest2typeUser>();
                foreach (var ItemList in objTravelRequest.ListObjectUKZNInvoice)
                {
                    Gna160Valinvoicerequest2typeUser objGna160Valinvoicerequest2typeUser = new Gna160Valinvoicerequest2typeUser();

                    objGna160Valinvoicerequest2typeUser.accountnumber = ItemList.accountnumber;
                    objGna160Valinvoicerequest2typeUser.uniquerequestid = ItemList.uniquerequestid;
                    objGna160Valinvoicerequest2typeUser.invoiceitemdescription = ItemList.invoiceitemdescription;
                    objGna160Valinvoicerequest2typeUser.invoiceitemnote = ItemList.invoiceitemnote;
                    objGna160Valinvoicerequest2typeUser.fullyprocessedyn = ItemList.fullyprocessedyn;
                    objGna160Valinvoicerequest2typeUser.alreadypaidyn = ItemList.alreadypaidyn;
                    //objGna160Valinvoicerequest2typeUser.penaltyfeeyn=ItemList.penaltyfeeyn; not in service
                    objGna160Valinvoicerequest2typeUser.itemvatamount = ItemList.itemvatamount;
                    objGna160Valinvoicerequest2typeUser.itemamountincvat = ItemList.itemamountincvat;
                    objGna160Valinvoicerequest2typeUser.invoiceitemnumber = ItemList.invoiceitemnumber;
                    objGna160Valinvoicerequest2typeUser.commentstmc = ItemList.commentstmc;


                    objLstGna160Valinvoicerequest2typeUser.Add(objGna160Valinvoicerequest2typeUser);

                }

                // Method Calling
                Gna160Valinvoiceresponse1typeUser[] objResponce = objUKZN.validateinvoice(objSec, objLstGna160Valinvoicerequest1typeUser.ToArray(), objLstGna160Valinvoicerequest2typeUser.ToArray());
                string SecReq = GetXMLFromObject(objSec);
                string MainReq1 = GetXMLFromObject(objLstGna160Valinvoicerequest1typeUser);
                string MainReq2 = GetXMLFromObject(objLstGna160Valinvoicerequest2typeUser);
                return objResponce[0].message;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string validatecrnote(TravelRequestProcess objTravelRequest)
        {
            try
            {
                // Secuirty Parameter
                objSec = UKZNSecurity.SetUKZNSecurityt();
                // First Parameter
                List<Gna160Valcrnoterequest1typeUser> objLstGna160Valcrnoterequest1typeUser = new List<Gna160Valcrnoterequest1typeUser>();

                Gna160Valcrnoterequest1typeUser objGna160Valcrnoterequest1typeUser = new Gna160Valcrnoterequest1typeUser();

                objGna160Valcrnoterequest1typeUser.costcentre = objTravelRequest.costcentre;
                objGna160Valcrnoterequest1typeUser.creditnotedescription = objTravelRequest.creditnotedescription;
                objGna160Valcrnoterequest1typeUser.invoicenumber = objTravelRequest.invoicenumber;
                objGna160Valcrnoterequest1typeUser.creditnotenumber = objTravelRequest.creditnotenumber;
                objGna160Valcrnoterequest1typeUser.creditnotedate = objTravelRequest.creditnotedate;
                objGna160Valcrnoterequest1typeUser.totamountincvat = objTravelRequest.totamountincvat;
                objGna160Valcrnoterequest1typeUser.supplierno = objTravelRequest.supplierno;


                objLstGna160Valcrnoterequest1typeUser.Add(objGna160Valcrnoterequest1typeUser);
                // Second Parameter
                List<Gna160Valcrnoterequest2typeUser> objLstGna160Valcrnoterequest2typeUser = new List<Gna160Valcrnoterequest2typeUser>();
                foreach (var ItemList in objTravelRequest.ListObjectUKZNInvoice)
                {
                    Gna160Valcrnoterequest2typeUser objGna160Valcrnoterequest2typeUser = new Gna160Valcrnoterequest2typeUser();
                    //objGna160Valinvoicerequest2typeUser.penaltyfeeyn=ItemList.penaltyfeeyn; not in service
                    objGna160Valcrnoterequest2typeUser.invoiceitemnumber = ItemList.invoiceitemnumber;
                    objGna160Valcrnoterequest2typeUser.accountnumber = ItemList.accountnumber;
                    objGna160Valcrnoterequest2typeUser.itemvatamount = ItemList.itemvatamount;
                    objGna160Valcrnoterequest2typeUser.itemamountincvat = ItemList.itemamountincvat;
                    objGna160Valcrnoterequest2typeUser.credititemdescription = ItemList.credititemdescription;
                    objGna160Valcrnoterequest2typeUser.alreadycreditedyn = ItemList.alreadycreditedyn;

                    objLstGna160Valcrnoterequest2typeUser.Add(objGna160Valcrnoterequest2typeUser);

                }

                // Method Calling
                Gna160Valcrnoteresponse1typeUser[] objResponce = objUKZN.validatecrnote(objSec, objLstGna160Valcrnoterequest1typeUser.ToArray(), objLstGna160Valcrnoterequest2typeUser.ToArray());
                return objResponce[0].message;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Gna160Extractccresponse1typeUser[] extractcostcentres(TravelRequestProcess objTravelRequest)
        {
            try
            {
                List<Gna160Extractccrequest1typeUser> objLstGna160Extractccrequest1typeUser = new List<Gna160Extractccrequest1typeUser>();

                Gna160Extractccrequest1typeUser objGna160Extractccrequest1typeUser = new Gna160Extractccrequest1typeUser();

                objGna160Extractccrequest1typeUser.changedonlyyn = objTravelRequest.changedonlyyn;
                objGna160Extractccrequest1typeUser.supplierno = objTravelRequest.supplierno;
                objLstGna160Extractccrequest1typeUser.Add(objGna160Extractccrequest1typeUser);

                Gna160Extractccresponse1typeUser[] objResponce = objUKZN.extractcostcentres(objSec, objLstGna160Extractccrequest1typeUser.ToArray());
                return objResponce;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Gna160Extractaccresponse1typeUser[] extractaccounts(TravelRequestProcess objTravelRequest)
        {
            try
            {
                List<Gna160Extractaccrequest1typeUser> objLstGna160Extractaccrequest1typeUser = new List<Gna160Extractaccrequest1typeUser>();

                Gna160Extractaccrequest1typeUser objGna160Extractaccrequest1typeUser = new Gna160Extractaccrequest1typeUser();

                objGna160Extractaccrequest1typeUser.changedonlyyn = objTravelRequest.changedonlyyn;
                objGna160Extractaccrequest1typeUser.supplierno = objTravelRequest.supplierno;
                objLstGna160Extractaccrequest1typeUser.Add(objGna160Extractaccrequest1typeUser);

                Gna160Extractaccresponse1typeUser[] objResponce = objUKZN.extractaccounts(objSec, objLstGna160Extractaccrequest1typeUser.ToArray());
                return objResponce;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public Gna160Extractglaresponse1typeUser[] extractglas(TravelRequestProcess objTravelRequest)
        {
            try
            {
                List<Gna160Extractglarequest1typeUser> objLstGna160Extractglarequest1typeUser = new List<Gna160Extractglarequest1typeUser>();

                Gna160Extractglarequest1typeUser objGna160Extractglarequest1typeUser = new Gna160Extractglarequest1typeUser();
                objGna160Extractglarequest1typeUser.changedonlyyn = objTravelRequest.changedonlyyn;
                objGna160Extractglarequest1typeUser.supplierno = objTravelRequest.supplierno;
                objGna160Extractglarequest1typeUser.costcentrecode = objTravelRequest.costcentre;
                objGna160Extractglarequest1typeUser.accountcode = objTravelRequest.accountcode;
                objLstGna160Extractglarequest1typeUser.Add(objGna160Extractglarequest1typeUser);

                Gna160Extractglaresponse1typeUser[] objResponce = objUKZN.extractglas(objSec, objLstGna160Extractglarequest1typeUser.ToArray());
                return objResponce;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Gna160Extractempresponse1typeUser[] extractemployees(TravelRequestProcess objTravelRequest)
        {
            try
            {
                List<Gna160Extractemprequest1typeUser> objLstGna160Extractemprequest1typeUser = new List<Gna160Extractemprequest1typeUser>();

                Gna160Extractemprequest1typeUser objGna160Extractemprequest1typeUser = new Gna160Extractemprequest1typeUser();
                objGna160Extractemprequest1typeUser.empcategory = objTravelRequest.empcategory;
                objGna160Extractemprequest1typeUser.changedonlyyn = objTravelRequest.changedonlyyn;
                objGna160Extractemprequest1typeUser.costcentrecode = objTravelRequest.costcentre;
                objGna160Extractemprequest1typeUser.supplierno = objTravelRequest.supplierno;
                objLstGna160Extractemprequest1typeUser.Add(objGna160Extractemprequest1typeUser);

                Gna160Extractempresponse1typeUser[] objResponce = objUKZN.extractemployees(objSec, objLstGna160Extractemprequest1typeUser.ToArray());
                return objResponce;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Gna160Extractovrresponse1typeUser[] extractapproverrides(TravelRequestProcess objTravelRequest)
        {
            try
            {
                List<Gna160Extractovrrequest1typeUser> objLstGna160Extractovrrequest1typeUser = new List<Gna160Extractovrrequest1typeUser>();

                Gna160Extractovrrequest1typeUser objGna160Extractovrrequest1typeUser = new Gna160Extractovrrequest1typeUser();
                objGna160Extractovrrequest1typeUser.supplierno = objTravelRequest.supplierno;
                objLstGna160Extractovrrequest1typeUser.Add(objGna160Extractovrrequest1typeUser);

                Gna160Extractovrresponse1typeUser[] objResponce = objUKZN.extractapproverrides(objSec, objLstGna160Extractovrrequest1typeUser.ToArray());
                return objResponce;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Gna160Extractinvresponse1typeUser[] extractinvapprovals(TravelRequestProcess objTravelRequest)
        {
            try
            {
                List<Gna160Extractinvrequest1typeUser> objLstGna160Extractinvrequest1typeUser = new List<Gna160Extractinvrequest1typeUser>();

                Gna160Extractinvrequest1typeUser objGna160Extractovrrequest1typeUser = new Gna160Extractinvrequest1typeUser();
                objGna160Extractovrrequest1typeUser.supplierno = objTravelRequest.supplierno;
                objGna160Extractovrrequest1typeUser.frominvdate = objTravelRequest.frominvdate;
                objGna160Extractovrrequest1typeUser.status = objTravelRequest.status;
                objGna160Extractovrrequest1typeUser.toinvdate = objTravelRequest.toinvdate;
                objLstGna160Extractinvrequest1typeUser.Add(objGna160Extractovrrequest1typeUser);

                Gna160Extractinvresponse1typeUser[] objResponce = objUKZN.extractinvapprovals(objSec, objLstGna160Extractinvrequest1typeUser.ToArray());
                return objResponce;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetXMLFromObject(object o)
        {
            StringWriter sw = new StringWriter();
            XmlTextWriter tw = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(o.GetType());
                tw = new XmlTextWriter(sw);
                serializer.Serialize(tw, o);
            }
            catch (Exception ex)
            {
                //Handle Exception Code
            }
            finally
            {
                sw.Close();
                if (tw != null)
                {
                    tw.Close();
                }
            }
            return sw.ToString();
        }
    }
}